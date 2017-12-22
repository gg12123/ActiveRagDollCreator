using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class BoxColliderParams
{
   public Vector3 Centre;
   public Vector3 Size;

   public BoxColliderParams(Vector3 c, Vector3 s)
   {
      Centre = c;
      Size = s;
   }
}

public static class Utils
{
   // ###########################
   public static int GetDirectionIndex(Transform transform, Vector3 dir)
   {
      int dirIndex = 0;
      float max = 0.0f;
      float val;

      Vector3[] directionsByIndex = new Vector3[] { transform.right, transform.up, transform.forward };

      for (int i = 0; i < directionsByIndex.Length; i++)
      {
         val = Mathf.Abs(Vector3.Dot(directionsByIndex[i], dir));

         if (val > max)
         {
            max = val;
            dirIndex = i;
         }
      }

      return dirIndex;
   }

   // ############################
   public static BoxColliderParams CalculateBoxColliderParams(Vector3 lowerBoundPos,
                                                              Vector3 upperBoundPos,
                                                              Transform subject,
                                                              TransformHierarchy targetDoll)
   {
      Vector3 headPos = targetDoll.Head.position;
      Vector3 armPos1 = targetDoll.UpperArm1.position;
      Vector3 armPos2 = targetDoll.UpperArm2.position;
      Vector3 spinePos = targetDoll.Spine.position;
      Vector3 legPos = targetDoll.UpperLeg1.position;

      int upDownDirIndex = Utils.GetDirectionIndex(subject, (headPos - spinePos).normalized);
      int leftRightDirIndex = Utils.GetDirectionIndex(subject, (armPos1 - armPos2).normalized);
      int forwardDirIndex = Enumerable.Range(0, 3).Where(x => (x != upDownDirIndex) && (x != leftRightDirIndex)).First();

      Vector3[] directionsByIndex = new Vector3[] { subject.right, subject.up, subject.forward };

      float upperBoundLocalHeight = Vector3.Dot(upperBoundPos - subject.position, directionsByIndex[upDownDirIndex]) / subject.lossyScale[upDownDirIndex];
      float lowerBoundLocalHeight = Vector3.Dot(lowerBoundPos - subject.position, directionsByIndex[upDownDirIndex]) / subject.lossyScale[upDownDirIndex];
      float requiredHeight = upperBoundLocalHeight - lowerBoundLocalHeight;
      float requiredUpDownPos = (upperBoundLocalHeight + lowerBoundLocalHeight) / 2.0f;

      float halfArmSpan = Mathf.Abs(Vector3.Dot(armPos1 - subject.position, directionsByIndex[leftRightDirIndex]) / subject.lossyScale[leftRightDirIndex]);
      float requiredWidth = 2.0f * halfArmSpan;

      float armsLocalforward = Vector3.Dot(armPos1 - subject.position, directionsByIndex[forwardDirIndex]) / subject.lossyScale[forwardDirIndex];
      float legsLocalForward = Vector3.Dot(legPos - subject.position, directionsByIndex[forwardDirIndex]) / subject.lossyScale[forwardDirIndex];
      float requiredForwardPos = (armsLocalforward + legsLocalForward) / 2.0f;

      Vector3 centre = Vector3.zero;
      centre[upDownDirIndex] = requiredUpDownPos;
      centre[forwardDirIndex] = requiredForwardPos;

      Vector3 size = Vector3.one;
      size[upDownDirIndex] = requiredHeight;
      size[leftRightDirIndex] = requiredWidth;
      size[forwardDirIndex] = requiredWidth;

      return (new BoxColliderParams(centre, size));
   }

   // ##############################
   public static void SerializeProperty(Object owner, string path, Object propertyValue)
   {
      SerializedObject so = new SerializedObject(owner);
      SerializedProperty sp = so.FindProperty(path);
      sp.objectReferenceValue = propertyValue;
      so.ApplyModifiedProperties();
   }
}

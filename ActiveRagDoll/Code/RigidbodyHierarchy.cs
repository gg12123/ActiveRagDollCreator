using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RigidbodyHierarchy : MonoBehaviour
{
   public Rigidbody Hips;
   public Rigidbody Spine;
   public Rigidbody Head;
   public Rigidbody UpperArm1;
   public Rigidbody UpperArm2;
   public Rigidbody LowerArm1;
   public Rigidbody LowerArm2;
   public Rigidbody UpperLeg1;
   public Rigidbody UpperLeg2;
   public Rigidbody LowerLeg1;
   public Rigidbody LowerLeg2;

   // ############################
   public void SerializeBodies(Dictionary<Transform, Transform> targetToDoll, TransformHierarchy targetsHierachy)
   {
      SerializedObject so = new SerializedObject(this);

      so.FindProperty("Hips").objectReferenceValue = targetToDoll[targetsHierachy.Hips].GetComponent<Rigidbody>();
      so.FindProperty("Spine").objectReferenceValue = targetToDoll[targetsHierachy.Spine].GetComponent<Rigidbody>();
      so.FindProperty("Head").objectReferenceValue = targetToDoll[targetsHierachy.Head].GetComponent<Rigidbody>();
      so.FindProperty("UpperArm1").objectReferenceValue = targetToDoll[targetsHierachy.UpperArm1].GetComponent<Rigidbody>();
      so.FindProperty("UpperArm2").objectReferenceValue = targetToDoll[targetsHierachy.UpperArm2].GetComponent<Rigidbody>();
      so.FindProperty("LowerArm1").objectReferenceValue = targetToDoll[targetsHierachy.LowerArm1].GetComponent<Rigidbody>();
      so.FindProperty("LowerArm2").objectReferenceValue = targetToDoll[targetsHierachy.LowerArm2].GetComponent<Rigidbody>();
      so.FindProperty("UpperLeg1").objectReferenceValue = targetToDoll[targetsHierachy.UpperLeg1].GetComponent<Rigidbody>();
      so.FindProperty("UpperLeg2").objectReferenceValue = targetToDoll[targetsHierachy.UpperLeg2].GetComponent<Rigidbody>();
      so.FindProperty("LowerLeg1").objectReferenceValue = targetToDoll[targetsHierachy.LowerLeg1].GetComponent<Rigidbody>();
      so.FindProperty("LowerLeg2").objectReferenceValue = targetToDoll[targetsHierachy.LowerLeg2].GetComponent<Rigidbody>();

      so.ApplyModifiedProperties();
   }
}

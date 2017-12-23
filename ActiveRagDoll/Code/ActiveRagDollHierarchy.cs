using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ActiveRagDollHierarchy : MonoBehaviour
{
   public ActiveRagDollBone Hips;
   public ActiveRagDollBone Spine;
   public ActiveRagDollBone Head;
   public ActiveRagDollBone UpperArm1;
   public ActiveRagDollBone UpperArm2;
   public ActiveRagDollBone LowerArm1;
   public ActiveRagDollBone LowerArm2;
   public ActiveRagDollBone UpperLeg1;
   public ActiveRagDollBone UpperLeg2;
   public ActiveRagDollBone LowerLeg1;
   public ActiveRagDollBone LowerLeg2;

   // ############################
   public void SerializeBodies(Dictionary<Transform, Transform> targetToDoll, TransformHierarchy targetsHierachy)
   {
      SerializedObject so = new SerializedObject(this);

      so.FindProperty("Hips").objectReferenceValue = targetToDoll[targetsHierachy.Hips].GetComponent<ActiveRagDollBone>();
      so.FindProperty("Spine").objectReferenceValue = targetToDoll[targetsHierachy.Spine].GetComponent<ActiveRagDollBone>();
      so.FindProperty("Head").objectReferenceValue = targetToDoll[targetsHierachy.Head].GetComponent<ActiveRagDollBone>();
      so.FindProperty("UpperArm1").objectReferenceValue = targetToDoll[targetsHierachy.UpperArm1].GetComponent<ActiveRagDollBone>();
      so.FindProperty("UpperArm2").objectReferenceValue = targetToDoll[targetsHierachy.UpperArm2].GetComponent<ActiveRagDollBone>();
      so.FindProperty("LowerArm1").objectReferenceValue = targetToDoll[targetsHierachy.LowerArm1].GetComponent<ActiveRagDollBone>();
      so.FindProperty("LowerArm2").objectReferenceValue = targetToDoll[targetsHierachy.LowerArm2].GetComponent<ActiveRagDollBone>();
      so.FindProperty("UpperLeg1").objectReferenceValue = targetToDoll[targetsHierachy.UpperLeg1].GetComponent<ActiveRagDollBone>();
      so.FindProperty("UpperLeg2").objectReferenceValue = targetToDoll[targetsHierachy.UpperLeg2].GetComponent<ActiveRagDollBone>();
      so.FindProperty("LowerLeg1").objectReferenceValue = targetToDoll[targetsHierachy.LowerLeg1].GetComponent<ActiveRagDollBone>();
      so.FindProperty("LowerLeg2").objectReferenceValue = targetToDoll[targetsHierachy.LowerLeg2].GetComponent<ActiveRagDollBone>();

      so.ApplyModifiedProperties();
   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class ParamsTuner : MonoBehaviour
{
   private ActiveRagDollHierarchy m_MyJoints;

   [SerializeField]
   private float m_Hips;
   [SerializeField]
   private float m_Spine;
   [SerializeField]
   private float m_Head;
   [SerializeField]
   private float m_UpperArms;
   [SerializeField]
   private float m_LowerArms;
   [SerializeField]
   private float m_UpperLegs;
   [SerializeField]
   private float m_LowerLegs;

   // Use this for initialization
   void Start ()
   {
      m_MyJoints = GetComponent<ActiveRagDollHierarchy>();
      Apply();
   }

   // ###########################
   public void SerializeDefaults(float hips, float spine, float head, float upperArms, float lowerArms, float upperLegs, float lowerLegs)
   {
      SerializedObject so = new SerializedObject(this);

      so.FindProperty("m_Hips").floatValue = hips;
      so.FindProperty("m_Spine").floatValue = spine;
      so.FindProperty("m_Head").floatValue = head;
      so.FindProperty("m_UpperArms").floatValue = upperArms;
      so.FindProperty("m_LowerArms").floatValue = lowerArms;
      so.FindProperty("m_UpperLegs").floatValue = upperLegs;
      so.FindProperty("m_LowerLegs").floatValue = lowerLegs;

      so.ApplyModifiedProperties();
   }

   // ############################
   public void Apply()
   {
      if (m_MyJoints != null)
      {
         ApplyValue(m_Head, m_MyJoints.Head);
         ApplyValue(m_Hips, m_MyJoints.Hips);
         ApplyValue(m_Spine, m_MyJoints.Spine);

         ApplyValue(m_UpperArms, m_MyJoints.UpperArm1);
         ApplyValue(m_UpperArms, m_MyJoints.UpperArm2);
         ApplyValue(m_LowerArms, m_MyJoints.LowerArm1);
         ApplyValue(m_LowerArms, m_MyJoints.LowerArm2);

         ApplyValue(m_UpperLegs, m_MyJoints.UpperLeg1);
         ApplyValue(m_UpperLegs, m_MyJoints.UpperLeg2);
         ApplyValue(m_LowerLegs, m_MyJoints.LowerLeg1);
         ApplyValue(m_LowerLegs, m_MyJoints.LowerLeg2);
      }
   }

   // ###############################
   protected abstract void ApplyValue(float value, ActiveRagDollBone bone);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassTuner : MonoBehaviour
{
   private RigidbodyHierarchy m_MyJoints;

   [SerializeField]
   private float m_HipsMass = 5.0f;
   [SerializeField]
   private float m_SpineMass = 5.0f;
   [SerializeField]
   private float m_HeadMass = 1.0f;
   [SerializeField]
   private float m_UpperArmsMass = 1.0f;
   [SerializeField]
   private float m_LowerArmsMass = 1.0f;
   [SerializeField]
   private float m_UpperLegsMass = 1.0f;
   [SerializeField]
   private float m_LowerLegsMass = 1.0f;

   // Use this for initialization
   void Start ()
   {
      m_MyJoints = GetComponent<RigidbodyHierarchy>();
      Apply();
   }

   // ############################
   public void Apply()
   {
      if (m_MyJoints != null)
      {
         m_MyJoints.Head.mass = m_HeadMass;
         m_MyJoints.Hips.mass = m_HipsMass;
         m_MyJoints.Spine.mass = m_SpineMass;

         m_MyJoints.UpperArm1.mass = m_UpperArmsMass;
         m_MyJoints.UpperArm2.mass = m_UpperArmsMass;
         m_MyJoints.LowerArm1.mass = m_LowerArmsMass;
         m_MyJoints.LowerArm2.mass = m_LowerArmsMass;

         m_MyJoints.UpperLeg1.mass = m_UpperLegsMass;
         m_MyJoints.UpperLeg2.mass = m_UpperLegsMass;
         m_MyJoints.LowerLeg1.mass = m_LowerLegsMass;
         m_MyJoints.LowerLeg2.mass = m_LowerLegsMass;
      }
   }
}

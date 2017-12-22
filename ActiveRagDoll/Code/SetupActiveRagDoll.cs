using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SetupActiveRagDoll : MonoBehaviour
{
   [SerializeField]
   private TransformHierarchy m_TargetDoll;
   [SerializeField]
   private SkinnedMeshRenderer m_Mesh;

   private List<Transform> m_TargetsRotJoints;
   private List<InitJoint> m_RotJointInitializers;

   // ##########################
   private bool TransformIsRotJoint(Transform targetsTransform)
   {
      bool isRot = false;
 
      foreach (Transform rot in m_TargetsRotJoints)
      {
         if (targetsTransform == rot)
         {
            isRot = true;
            break;
         }
      }
 
      return isRot;
   }
 
   // #########################
   private void ConstructFullJointArray()
   {
      m_TargetsRotJoints = new List<Transform>();
     
      m_TargetsRotJoints.Add(m_TargetDoll.Head);
      m_TargetsRotJoints.Add(m_TargetDoll.Hips);
      m_TargetsRotJoints.Add(m_TargetDoll.Spine);
      m_TargetsRotJoints.Add(m_TargetDoll.UpperArm1);
      m_TargetsRotJoints.Add(m_TargetDoll.UpperArm2);
      m_TargetsRotJoints.Add(m_TargetDoll.LowerArm1);
      m_TargetsRotJoints.Add(m_TargetDoll.LowerArm2);
      m_TargetsRotJoints.Add(m_TargetDoll.UpperLeg1);
      m_TargetsRotJoints.Add(m_TargetDoll.UpperLeg2);
      m_TargetsRotJoints.Add(m_TargetDoll.LowerLeg1);
      m_TargetsRotJoints.Add(m_TargetDoll.LowerLeg2);
   }
 
   // ############################
   private InitJoint CreateInitJoint(Transform target, Transform myRotJoint)
   {
      InitJoint init = null;
 
      if (target == m_TargetDoll.Head)
      {
         init = new InitHeadJoint(target, myRotJoint, m_TargetDoll, m_Mesh);
      }
      else if (target == m_TargetDoll.Spine)
      {
         init = new InitSpineJoint(target, myRotJoint, m_TargetDoll);
      }
      else if (target == m_TargetDoll.Hips)
      {
         init = new InitHipsJoint(target, myRotJoint, m_TargetDoll);
      }
      else
      {
         init = new InitLimbJoint(target, myRotJoint);
      }
 
      return init;
   }
 
   // Use this for initialization
   void Awake ()
   {
      m_RotJointInitializers = new List<InitJoint>();
 
      ConstructFullJointArray();
 
      Transform[] myTrans = GetComponentsInChildren<Transform>();
      Transform[] targetsTrans = m_TargetDoll.Hips.GetComponentsInChildren<Transform>();
 
      if (myTrans.Length != targetsTrans.Length)
         Debug.LogError("Number of transforms error");
 
      for (int i = 0; i < targetsTrans.Length; i++)
      {
         if (TransformIsRotJoint(targetsTrans[i]))
         {
            m_RotJointInitializers.Add(CreateInitJoint(targetsTrans[i], myTrans[i]));
         }
      }
 
      foreach (InitJoint ij in m_RotJointInitializers)
         ij.InitStage1();
 
      foreach (InitJoint ij in m_RotJointInitializers)
         ij.InitStage2();

      //GoFullyKinematic();
   }

   // ##############################
   private void GoFullyKinematic()
   {
      foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
         rb.isKinematic = true;

      foreach (Rigidbody rb in GetComponentsInParent<Rigidbody>())
         rb.isKinematic = true;
   }
}

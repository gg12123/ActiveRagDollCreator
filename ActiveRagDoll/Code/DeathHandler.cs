using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
   private ConfigurableJoint m_Hips;
   private ConfigurableJoint[] m_Joints;
   private JointControl[] m_JointControllers;

   // Use this for initialization
   void Start ()
   {
      m_Hips = GetComponent<ActiveRagDollHierarchy>().Hips.Joint;
      m_Joints = GetComponentsInChildren<ConfigurableJoint>();
      m_JointControllers = GetComponentsInChildren<JointControl>();

      if (m_Joints.Length != m_JointControllers.Length)
         Debug.LogError("Inconsistent joints and joint controllers.");
   }

   // ###########################
   private void MakeJointFloppy(int index)
   {
      m_JointControllers[index].enabled = false;

      ConfigurableJoint j = m_Joints[index];

      JointDrive d = j.angularXDrive;
      d.positionSpring = 1.0f;
      j.angularXDrive = d;

      d = j.angularYZDrive;
      d.positionSpring = 1.0f;
      j.angularYZDrive = d;
   }
   
   // ##############################
   public void Die()
   {
      for (int i = 0; i < m_Joints.Length; i++)
         MakeJointFloppy(i);

      m_Hips.breakForce = 0.0f;
   }
}

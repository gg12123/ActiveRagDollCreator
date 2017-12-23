using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InitJoint
{
   protected Transform MyTransform;
   protected GameObject MyGameObject;
   protected Transform Target;

   private ConfigurableJoint m_JointWaitingForConnect;

   // #########################
   public InitJoint(Transform target, Transform myTransform)
   {
      Target = target;
      MyTransform = myTransform;
      MyGameObject = myTransform.gameObject;
   }

   // ######################
   private void ConfigureRotJoint()
   {
      MyGameObject.AddComponent<Rigidbody>();
      ConfigurableJoint j = MyGameObject.AddComponent<ConfigurableJoint>();

      MyGameObject.AddComponent<JointControl>();

      j.anchor = Vector3.zero;
      j.autoConfigureConnectedAnchor = true;

      j.axis = Vector3.right;
      j.secondaryAxis = Vector3.up;

      j.xMotion = ConfigurableJointMotion.Locked;
      j.yMotion = ConfigurableJointMotion.Locked;
      j.zMotion = ConfigurableJointMotion.Locked;

      SetRotationalFreedom(j);

      j.rotationDriveMode = RotationDriveMode.XYAndZ;

      JointDrive d = j.angularXDrive;
      d.positionSpring = 1000.0f;
      j.angularXDrive = d;

      d = j.angularYZDrive;
      d.positionSpring = 1000.0f;
      j.angularYZDrive = d;

      m_JointWaitingForConnect = j;
   }

   // ###########################
   public void InitStage1()
   {
      ConfigureRotJoint();
      AddCollider();
      MyGameObject.AddComponent<ActiveRagDollBone>().Serialize();
   }

   // ###########################
   public void InitStage2()
   {
      // Connect the joint to a parent rigidbody
      Transform tran = MyTransform.parent;
      Rigidbody rb = tran.GetComponent<Rigidbody>();

      while ((rb == null) && (tran != null))
      {
         tran = tran.parent;
         rb = tran.GetComponent<Rigidbody>();
      }

      if (rb == null)
         Debug.LogError("Unable to connect fixed joint");

      m_JointWaitingForConnect.connectedBody = rb;

      // Set the joint control target
      m_JointWaitingForConnect.GetComponent<JointControl>().SerializeTarget(Target);
   }

   // #########################
   protected virtual void SetRotationalFreedom(ConfigurableJoint j)
   {
      j.angularXMotion = ConfigurableJointMotion.Free;
      j.angularYMotion = ConfigurableJointMotion.Free;
      j.angularZMotion = ConfigurableJointMotion.Free;
   }

   // ###########################
   protected abstract void AddCollider();
}


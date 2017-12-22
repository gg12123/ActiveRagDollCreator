using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public abstract class InitJoint
//{
//   protected Transform MyTransform;
//   protected GameObject MyGameObject;
//   protected Transform Target;
//
//   private FixedJoint m_JointWaitingForConnect;
//   private ConfigurableJoint m_RotJoint;
//
//   // #########################
//   public InitJoint(Transform target, Transform myTransform)
//   {
//      Target = target;
//      MyTransform = myTransform;
//      MyGameObject = myTransform.gameObject;
//   }
//
//   // ######################
//   private ConfigurableJoint ConfigureRotJoint()
//   {
//      GameObject joint = new GameObject("Rot");
//      joint.AddComponent<Rigidbody>();
//      ConfigurableJoint j = joint.AddComponent<ConfigurableJoint>();
//
//      joint.AddComponent<JointControl>();
//
//      j.autoConfigureConnectedAnchor = false;
//      j.anchor = Vector3.zero;
//      j.connectedAnchor = Vector3.zero;
//
//      j.transform.position = MyTransform.position;
//      j.transform.rotation = MyTransform.rotation;
//      j.transform.parent = MyTransform.parent;
//
//      j.axis = Vector3.right;
//      j.secondaryAxis = Vector3.up;
//
//      j.xMotion = ConfigurableJointMotion.Locked;
//      j.yMotion = ConfigurableJointMotion.Locked;
//      j.zMotion = ConfigurableJointMotion.Locked;
//
//      JointDrive d = j.angularXDrive;
//      d.positionSpring = 100.0f;
//      j.angularXDrive = d;
//
//      d = j.angularYZDrive;
//      d.positionSpring = 1000.0f;
//      j.angularYZDrive = d;
//
//      return j;
//   }
//
//   // ##########################
//   private void ConfigureRoot(ConfigurableJoint rotJoint)
//   {
//      GameObject root = new GameObject("Root");
//      root.AddComponent<Rigidbody>();
//
//      m_JointWaitingForConnect = root.AddComponent<FixedJoint>();
//
//      root.transform.position = MyTransform.position;
//      root.transform.rotation = MyTransform.rotation;
//      root.transform.parent = MyTransform.parent;
//
//      rotJoint.connectedBody = root.GetComponent<Rigidbody>();
//   }
//
//   // ###########################
//   public void InitStage1()
//   {
//      m_RotJoint = ConfigureRotJoint();
//      ConfigureRoot(m_RotJoint);
//      AddCollider();
//      MyTransform.parent = m_RotJoint.transform;
//   }
//
//   // ###########################
//   public void InitStage2()
//   {
//      // Connect the root joint to a parent
//      Transform tran = MyTransform.parent.parent;
//      Rigidbody rb = tran.GetComponent<Rigidbody>();
//
//      while ((rb == null) && (tran != null))
//      {
//         tran = tran.parent;
//         rb = tran.GetComponent<Rigidbody>();
//      }
//
//      if (rb == null)
//         Debug.LogError("Unable to connect fixed joint");
//
//      m_JointWaitingForConnect.connectedBody = rb;
//
//      // Set the joint control target
//      m_RotJoint.GetComponent<JointControl>().SetTarget(Target);
//   }
//
//   // ###########################
//   protected abstract void AddCollider();
//}

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


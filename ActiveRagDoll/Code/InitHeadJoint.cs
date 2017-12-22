using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitHeadJoint : InitJoint
{
   private TransformHierarchy m_TargetDoll;
   private SkinnedMeshRenderer m_Mesh;

   public InitHeadJoint(Transform target,
                        Transform myTransform,
                        TransformHierarchy targetDoll,
                        SkinnedMeshRenderer mesh) : base(target, myTransform)
   {
      m_TargetDoll = targetDoll;
      m_Mesh = mesh;
   }

   protected override void AddCollider()
   {
      Vector3 upward = (m_TargetDoll.Head.position - m_TargetDoll.Spine.position).normalized;
      int upDownDirIndex = Utils.GetDirectionIndex(Target, upward);

      Vector3[] directionsByIndex = new Vector3[] { Target.right, Target.up, Target.forward };

      float radius = (0.5f * Vector3.Dot(m_Mesh.bounds.max - Target.position, directionsByIndex[upDownDirIndex])) / Target.lossyScale[upDownDirIndex];

      SphereCollider col = MyGameObject.AddComponent<SphereCollider>();
    
      Vector3 centre = Vector3.zero;

      if (Vector3.Dot(upward, directionsByIndex[upDownDirIndex]) > 0.0f)
      {
         centre[upDownDirIndex] = radius;
      }
      else
      {
         centre[upDownDirIndex] = -radius;
      }
    
      col.center = centre;
      col.radius = radius;
   }
}

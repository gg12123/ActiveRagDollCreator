using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSpineJoint : InitBreastJoint
{
   public InitSpineJoint(Transform target, Transform myTransform, TransformHierarchy targetDoll) : base(target, myTransform, targetDoll)
   {
   }

   protected override Vector3 GetLowerBoundPos()
   {
      return TargetDoll.Spine.position;
   }

   protected override Vector3 GetUpperBoundPos()
   {
      return TargetDoll.UpperArm1.position;
   }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitHipsJoint : InitBreastJoint
{
   public InitHipsJoint(Transform target, Transform myTransform, TransformHierarchy targetDoll) : base(target, myTransform, targetDoll)
   {
   }

   protected override Vector3 GetLowerBoundPos()
   {
      return TargetDoll.UpperLeg1.position;
   }

   protected override Vector3 GetUpperBoundPos()
   {
      return TargetDoll.Spine.position;
   }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class InitBreastJoint : InitJoint
{
   protected TransformHierarchy TargetDoll;

   // ############################
   public InitBreastJoint(Transform target,
                          Transform myTransform,
                          TransformHierarchy targetDoll) : base(target, myTransform)
   {
      TargetDoll = targetDoll;
   }

   // ############################
   protected override void AddCollider()
   {
      BoxCollider col = MyGameObject.AddComponent<BoxCollider>();

      BoxColliderParams p = Utils.CalculateBoxColliderParams(GetLowerBoundPos(),
                                                             GetUpperBoundPos(),
                                                             Target,
                                                             TargetDoll);
      
      col.center = p.Centre;
      col.size = p.Size;
   }

   // ##################################
   protected abstract Vector3 GetLowerBoundPos();
   protected abstract Vector3 GetUpperBoundPos();
}

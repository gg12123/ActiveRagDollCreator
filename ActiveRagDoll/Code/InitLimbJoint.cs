using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitLimbJoint : InitJoint
{
   public InitLimbJoint(Transform target, Transform myTransform) : base(target, myTransform)
   {
   }

   // ############################
   private Vector3 CalculateCentre(int dirIndex, float distanceToChild)
   {
      Vector3 c = Vector3.zero;
      c[dirIndex] = 0.5f * distanceToChild;
      return c;
   }

   // ###########################
   protected override void AddCollider()
   {
      Transform child = MyTransform.GetChild(0);

      if (child != null)
      {
         CapsuleCollider col = MyGameObject.AddComponent<CapsuleCollider>();

         Vector3 toChild = child.position - MyTransform.position;
         int dirIndex = Utils.GetDirectionIndex(MyTransform, toChild.normalized);
         float distanceToChild = toChild.magnitude / MyTransform.lossyScale[dirIndex];

         col.center = CalculateCentre(dirIndex, distanceToChild);
         col.direction = dirIndex;
         col.radius = distanceToChild * 0.2f;
         col.height = distanceToChild;
      }
   }
}

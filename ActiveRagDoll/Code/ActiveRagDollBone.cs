using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRagDollBone : MonoBehaviour
{
   public Rigidbody Body;
   public ConfigurableJoint Joint;

   public void Serialize()
   {
      Utils.SerializeProperty(this, "Body", GetComponent<Rigidbody>());
      Utils.SerializeProperty(this, "Joint", GetComponent<ConfigurableJoint>());
   }
}

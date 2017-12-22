using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointControl : MonoBehaviour
{
   [SerializeField]
   private Transform m_Target;

   private ConfigurableJoint m_Joint;
   private Quaternion m_q0;

   // Use this for initialization
   void Start ()
   {
      m_Joint = GetComponent<ConfigurableJoint>();
      m_q0 = m_Target.localRotation;
   }

   // #########################
   public void SerializeTarget(Transform target)
   {
      Utils.SerializeProperty(this, "m_Target", target);
   }
   
   // Update is called once per frame
   void Update ()
   {
      // deltaQ must be inverted. I think this is becasue joint space is the inverse of a normal transforms space.
      Quaternion deltaQ = Quaternion.Inverse(m_Target.localRotation) * m_q0;
      m_Joint.targetRotation = deltaQ;
   }
}

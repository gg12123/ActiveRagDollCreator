using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamperTuner : ParamsTuner
{
   protected override void ApplyValue(float value, ActiveRagDollBone bone)
   {
      JointDrive d = bone.Joint.angularXDrive;
      d.positionDamper = value;
      bone.Joint.angularXDrive = d;

      d = bone.Joint.angularYZDrive;
      d.positionDamper = value;
      bone.Joint.angularYZDrive = d;
   }
}

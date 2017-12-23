using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringForceTuner : ParamsTuner
{
   protected override void ApplyValue(float value, ActiveRagDollBone bone)
   {
      JointDrive d = bone.Joint.angularXDrive;
      d.positionSpring = value;
      bone.Joint.angularXDrive = d;

      d = bone.Joint.angularYZDrive;
      d.positionSpring = value;
      bone.Joint.angularYZDrive = d;
   }
}

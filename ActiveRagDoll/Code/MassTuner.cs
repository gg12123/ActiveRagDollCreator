using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassTuner : ParamsTuner
{
   protected override void ApplyValue(float value, ActiveRagDollBone bone)
   {
      bone.Body.mass = value;
   }
}

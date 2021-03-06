﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ParamsTuner), true)]
public class ParamsTunerEditor : Editor
{
   private ParamsTuner m_Target;

   // #######################
   void OnEnable()
   {
      m_Target = target as ParamsTuner;
   }

   // #######################
   public override void OnInspectorGUI()
   {
      base.OnInspectorGUI();
      m_Target.Apply();
   }
}

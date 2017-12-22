using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MassTuner))]
public class MassTunerEditor : Editor
{
   private MassTuner m_Target;

   // #######################
   void OnEnable()
   {
      m_Target = target as MassTuner;
   }

   // #######################
   public override void OnInspectorGUI()
   {
      base.OnInspectorGUI();
      m_Target.Apply();
   }
}

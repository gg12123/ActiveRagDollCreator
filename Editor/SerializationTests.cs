using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SerializationTests : Editor
{
   // #########################
   [MenuItem("Active rag dolls/Ser test")]
   public static void Serialize()
   {
      GameObject go = new GameObject("Test");
      go.AddComponent<Rigidbody>();


      
   }
}

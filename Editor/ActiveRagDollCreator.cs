using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ActiveRagDollCreator : EditorWindow
{
   [SerializeField]
   private TransformHierarchy m_RiggedHumanoidHierachy;
   private GameObject m_RiggedHumanoid;

   private List<Transform> m_TargetRotJoints;
   private List<InitJoint> m_RotJointInitializers;

   private SkinnedMeshRenderer[] m_RiggedHumanoidsMeshs;

   private Dictionary<Transform, Transform> m_TargetToRagDoll;

   // #########################
   [MenuItem("Active rag dolls/Creator")]
   public static void OpenWindow()
   {
      ActiveRagDollCreator window = GetWindow(typeof(ActiveRagDollCreator)) as ActiveRagDollCreator;
      window.Show();
   }

   // ##########################
   private bool TransformIsTargetJoint(Transform targetsTransform)
   {
      bool isRot = false;

      foreach (Transform rot in m_TargetRotJoints)
      {
         if (targetsTransform == rot)
         {
            isRot = true;
            break;
         }
      }

      return isRot;
   }

   // #########################
   private void ConstructTargetJointsList()
   {
      m_TargetRotJoints = new List<Transform>();

      m_TargetRotJoints.Add(m_RiggedHumanoidHierachy.Head);
      m_TargetRotJoints.Add(m_RiggedHumanoidHierachy.Hips);
      m_TargetRotJoints.Add(m_RiggedHumanoidHierachy.Spine);
      m_TargetRotJoints.Add(m_RiggedHumanoidHierachy.UpperArm1);
      m_TargetRotJoints.Add(m_RiggedHumanoidHierachy.UpperArm2);
      m_TargetRotJoints.Add(m_RiggedHumanoidHierachy.LowerArm1);
      m_TargetRotJoints.Add(m_RiggedHumanoidHierachy.LowerArm2);
      m_TargetRotJoints.Add(m_RiggedHumanoidHierachy.UpperLeg1);
      m_TargetRotJoints.Add(m_RiggedHumanoidHierachy.UpperLeg2);
      m_TargetRotJoints.Add(m_RiggedHumanoidHierachy.LowerLeg1);
      m_TargetRotJoints.Add(m_RiggedHumanoidHierachy.LowerLeg2);
   }

   // ###########################
   private SkinnedMeshRenderer FindHeadMesh()
   {
      Vector3 upward = (m_RiggedHumanoidHierachy.Head.position - m_RiggedHumanoidHierachy.Spine.position).normalized;

      float heighest = Vector3.Dot(m_RiggedHumanoidsMeshs[0].bounds.max, upward);
      SkinnedMeshRenderer head = m_RiggedHumanoidsMeshs[0];

      foreach (SkinnedMeshRenderer smr in m_RiggedHumanoidsMeshs)
      {
         float height = Vector3.Dot(smr.bounds.max, upward);

         if (height > heighest)
         {
            heighest = height;
            head = smr;
         }
      }

      return head;
   }

   // ############################
   private InitJoint CreateInitJoint(Transform target, Transform ragDollJoint)
   {
      InitJoint init = null;

      if (target == m_RiggedHumanoidHierachy.Head)
      {
         init = new InitHeadJoint(target, ragDollJoint, m_RiggedHumanoidHierachy, FindHeadMesh());
      }
      else if (target == m_RiggedHumanoidHierachy.Spine)
      {
         init = new InitSpineJoint(target, ragDollJoint, m_RiggedHumanoidHierachy);
      }
      else if (target == m_RiggedHumanoidHierachy.Hips)
      {
         init = new InitHipsJoint(target, ragDollJoint, m_RiggedHumanoidHierachy);
      }
      else
      {
         init = new InitLimbJoint(target, ragDollJoint);
      }

      return init;
   }

   // ##########################
   private void MapTargetToRagDoll(GameObject activeRagdoll)
   {
      m_RotJointInitializers = new List<InitJoint>();
      m_TargetToRagDoll = new Dictionary<Transform, Transform>();

      Transform[] ragDollTrans = activeRagdoll.GetComponentsInChildren<Transform>();
      Transform[] targetsTrans = m_RiggedHumanoid.GetComponentsInChildren<Transform>();

      if (ragDollTrans.Length != targetsTrans.Length)
         Debug.LogError("Number of transforms error");

      for (int i = 0; i < targetsTrans.Length; i++)
      {
         if (TransformIsTargetJoint(targetsTrans[i]))
         {
            m_TargetToRagDoll[targetsTrans[i]] = ragDollTrans[i];
            m_RotJointInitializers.Add(CreateInitJoint(targetsTrans[i], ragDollTrans[i]));
         }
      }
   }

   // ###########################
   private void Create()
   {
      PrefabUtility.DisconnectPrefabInstance(m_RiggedHumanoid);
      GameObject activeRagdoll = Instantiate(m_RiggedHumanoid) as GameObject;

      activeRagdoll.AddComponent<Rigidbody>().isKinematic = true;

      m_RiggedHumanoidsMeshs = m_RiggedHumanoid.GetComponentsInChildren<SkinnedMeshRenderer>();

      ConstructTargetJointsList();

      MapTargetToRagDoll(activeRagdoll);

      foreach (InitJoint ij in m_RotJointInitializers)
         ij.InitStage1();

      foreach (InitJoint ij in m_RotJointInitializers)
         ij.InitStage2();

      SetupNewGameObject(activeRagdoll);
   }

   // ########################
   private void SetupNewGameObject(GameObject activeRagdoll)
   {
      GameObject newRoot = new GameObject("ActiveRagDoll");
      newRoot.transform.position = m_RiggedHumanoid.transform.position;
      m_RiggedHumanoid.transform.parent = newRoot.transform;
      activeRagdoll.transform.parent = newRoot.transform;

      activeRagdoll.name = "RagDollHierarchy";
      m_RiggedHumanoid.name = "AnimatedHierarchy";

      activeRagdoll.GetComponentInChildren<Animator>().enabled = false;

      foreach (SkinnedMeshRenderer smr in m_RiggedHumanoidsMeshs)
         smr.sharedMesh = null;

      activeRagdoll.AddComponent<MassTuner>();
      activeRagdoll.AddComponent<DeathHandler>();
      activeRagdoll.AddComponent<RigidbodyHierarchy>().SerializeBodies(m_TargetToRagDoll, m_RiggedHumanoidHierachy);
   }

   // ########################
   void OnGUI()
   {
      m_RiggedHumanoid = EditorGUILayout.ObjectField("Rigged humanoid root game object", m_RiggedHumanoid, typeof(GameObject), true) as GameObject;

      SerializedObject s = new SerializedObject(this);
      EditorGUILayout.PropertyField(s.FindProperty("m_RiggedHumanoidHierachy"), true);
      s.ApplyModifiedProperties();

      if (GUILayout.Button("Create"))
      {
         Create();
      }
   }
}

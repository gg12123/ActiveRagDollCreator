using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
   [SerializeField]
   private GameObject m_ProjectilePrefab;
   [SerializeField]
   private float m_Speed = 25.0f;

   // Use this for initialization
   void Start ()
   {
      
   }
   
   // Update is called once per frame
   void Update ()
   {
      if (Input.GetMouseButtonDown(0))
      {
         Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

         GameObject proj = Instantiate(m_ProjectilePrefab) as GameObject;

         proj.transform.position = r.origin;
         proj.GetComponent<Rigidbody>().velocity = m_Speed * r.direction;

         Destroy(proj, 5.0f);
      }
   }
}

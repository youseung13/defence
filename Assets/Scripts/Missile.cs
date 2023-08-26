using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
   public Transform target;
   public Transform oritarget;
   public CapsuleCollider2D cr;

   public float speed = 0.7f;
   public int damage;

   public Vector3 dir;
   public Vector3 targetPos;

   private void Awake() {
      cr= GetComponent<CapsuleCollider2D>();
      oritarget = target;
   }

   void Update()
    {
      if (target != null)
        {
            targetPos = target.position;
        }
        
        dir = targetPos - this.transform.position;
        dir = dir.normalized * speed * Time.deltaTime;
            
        this.transform.position += dir;


        float angle = Quaternion.FromToRotation
            (Vector3.right, dir).eulerAngles.z;

        this.transform.rotation = Quaternion.Euler(0,0,
            angle);
      
      
        if (Vector3.Distance(this.transform.position,
                targetPos) < 0.5f)
        {
            if (target != null)
            {
                target.GetComponent<Enemy>().TakeDamage(damage);
            }
            DestroyImmediate(this.gameObject);
        }

    }

    private void Attack(int _damage)
    {

    }

/*
    private void OnTriggerEnter2D(Collider2D hit) 
    {
      if(hit.GetComponent<Enemy>() != null)
      {
         if(hit.gameObject.transform == target)
         {
            hit.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
         }
      }
    }
*/
}

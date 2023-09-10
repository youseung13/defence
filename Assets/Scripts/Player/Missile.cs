using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public PlayerStats pl;
   public GameObject target;
   public Transform oritarget;
   public CapsuleCollider2D cr;

   public float speed = 0.7f;
 
   public Stat damage;

   public Vector3 dir;
   public Vector3 targetPos;
   public Vector3 lastKnownPosition;

   private void Awake() {
      cr= GetComponent<CapsuleCollider2D>();
    //  oritarget = target;
   }

/*
   void Update()
    {
      if (target != null)
        {
            targetPos = target.position;
        }

        if(!target.gameObject.activeSelf)
        {
            oritarget =target;
          
        }
        
        
        dir = targetPos - this.transform.position;
        dir = dir.normalized * speed * Time.deltaTime;
            
        this.transform.position += dir;


        float angle = Quaternion.FromToRotation
            (Vector3.right, dir).eulerAngles.z;

        this.transform.rotation = Quaternion.Euler(0,0,
            angle);

        if(oritarget != null && oritarget != target)
        Destroy(gameObject);

    }
*/
    private void Update()
    {
        if (target != null)
        {
            Vector3 moveDirection = (lastKnownPosition - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;

              float angle = Quaternion.FromToRotation
            (Vector3.right, dir).eulerAngles.z;

        this.transform.rotation = Quaternion.Euler(0,0,
            angle);
        }

        if(Vector2.Distance(transform.position,lastKnownPosition) < 0.2f)
        Destroy(gameObject);
    }
     public void SetTarget(Transform newTarget)
    {
     
         //this.target.transform= newTarget;
        lastKnownPosition = newTarget.position;
     
       
    }

    private void Attack(int _damage)
    {

    }


    private void OnTriggerEnter2D(Collider2D hit) 
    {
      if(hit.GetComponent<EnemyStats>() != null)
      {
            EnemyStats targetstat = hit.GetComponent<EnemyStats>();

            pl.DoDamage(targetstat);
            //hit.GetComponent<Enemy>().Die();
            Destroy(gameObject);
      }
    }

}

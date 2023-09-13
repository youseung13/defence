using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public PlayerStats pl;
   public GameObject target;
   public Transform oritarget;
   public CapsuleCollider2D cr;
   public Hero hero;


   public float speed = 0.7f;
 
   public Stat damage;

   public Vector3 dir;
   public Vector3 targetPos;
   public Vector3 lastKnownPosition;

   private bool ishit = false;

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

        if(Vector2.Distance(transform.position,lastKnownPosition) < 0.01f)
        Destroy(gameObject);
    }
*/
            private void Update()
        {
            if(target == null || Vector2.Distance(transform.position,lastKnownPosition) < 0.01f)
            {
                Destroy(gameObject);
            }

            if (target != null)
            {

                // 현재 위치에서 목표 위치까지 직선으로 이동
                transform.position = Vector3.MoveTowards(transform.position, lastKnownPosition, speed * Time.deltaTime);

                // 미사일의 방향을 조절
                Vector3 direction = (lastKnownPosition - transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);

                // 미사일이 목표 지점에 도달하면 파괴

               
            }
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
      if(hit.GetComponent<EnemyStats>() != null && !ishit)
      {
            EnemyStats targetstat = hit.GetComponent<EnemyStats>();
            pl.DoDamage(targetstat);
            ishit = true;
            //hit.GetComponent<Enemy>().Die();
            Destroy(gameObject);
      }
    }

}

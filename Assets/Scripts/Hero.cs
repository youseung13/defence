using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
  public float delayTimer;

  public float delayMax;

  public float range ;

  public int cost;

  public Animator ani;

  public GameManager gm;

  public Transform target;

  public float minDistance = 9999;

  public GameObject bullet;



    public GameObject targetEnemy;
    public LayerMask enemyLayer;
   public float detectInterval =0;
    private bool isdetect;
    private bool canAttack;


bool isRunning = true;
  private void Update()
  {
    if(!canAttack)
    {
        delayTimer += Time.deltaTime;
        if(delayTimer >= delayMax)
        {
            canAttack = true;
            delayTimer = 0;
        }
    }
    /*
    
    delayTimer -= Time.deltaTime;

    if(delayTimer<0 && target != null)
    {
     // Debug.Log("공격!");
      delayTimer = delayMax;
      ani.SetTrigger("attack");

      GameObject temp = Instantiate(bullet);
      temp.transform.position = transform.position;
      temp.GetComponent<Missile>().target = target;
    }

       if (target == null)
        {
            minDistance = 999999;
            for (int i = 0; i < gm.enemys.childCount; i++)
            {
               // if (gm.enemys.GetChild(i).GetComponent<Enemy>().is)
              //  {
                    if (Vector3.Distance(gm.enemys.GetChild(i).position,
                            this.transform.position) < range)
                    {
                        if (Vector3.Distance(gm.enemys.GetChild(i).position,
                                this.transform.position) < minDistance)
                        {
                            minDistance = Vector3.Distance(gm.enemys.GetChild(i).position,
                                this.transform.position);
                            target = gm.enemys.GetChild(i);
                        }
                    }
                //}
            }
        }
        else
        {
           // if (Vector3.Distance(target.position,
                 //   this.transform.position) > range || !target.GetComponent<Enemy>().isAlive)
            //{
            //    target = null;
           // }
        }
        //가까이 있는 적을 타겟으로 설정한다
        //타겟이 사정거리 밖으로 나가면 다시 타겟을 설정한다

        */

         if (!isdetect)
        {
            StartCoroutine(DetectEnemies());
            isdetect = true;
        }

        if(targetEnemy != null && canAttack && Vector2.Distance(transform.position,targetEnemy.transform.position) <= range)
        {
            Attack();
        }
      
    }

    private void Attack()
    {
        canAttack=false;
      ani.SetTrigger("attack");
     GameObject temp = Instantiate(bullet);
      temp.transform.position = transform.position;
      temp.GetComponent<Missile>().target = targetEnemy;
      temp.GetComponent<Missile>().pl = GetComponent<PlayerStats>();
      temp.GetComponent<Missile>().SetTarget(targetEnemy.transform);
     
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    //    Gizmos.DrawWireCube(transform.position, new Vector3(range,range,0));
    }

    IEnumerator DetectEnemies()
    {
        while (isRunning)
        {
            // Find all enemies within detection radius
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range*2, enemyLayer);
           
            if (hitColliders.Length > 0)
            {
                // Assign closest enemy to target enemy
                targetEnemy = hitColliders[0].gameObject;
                float closestDistance = Vector3.Distance(transform.position, targetEnemy.transform.position);
                for (int i = 1; i < hitColliders.Length; i++)
                {
                    float distance = Vector3.Distance(transform.position, hitColliders[i].transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        targetEnemy = hitColliders[i].gameObject;
                    }
                }
            }
            else
            {
                targetEnemy = null;
            }
            // Wait for detectInterval seconds before checking for new enemies again
            yield return new WaitForSeconds(detectInterval);
        }
    }
}

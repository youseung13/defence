using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
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

  private void Update()
  {
    
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
                if (gm.enemys.GetChild(i).GetComponent<Enemy>().isAlive)
                {
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
                }
            }
        }
        else
        {
            if (Vector3.Distance(target.position,
                    this.transform.position) > range || !target.GetComponent<Enemy>().isAlive)
            {
                target = null;
            }
        }
        //가까이 있는 적을 타겟으로 설정한다
        //타겟이 사정거리 밖으로 나가면 다시 타겟을 설정한다
    }
}

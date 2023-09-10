using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeArrow : MonoBehaviour
{
    private CircleCollider2D cd;
    public Hero hero;
    private void Awake() {
        cd = GetComponent<CircleCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
    
    }


      private void OnTriggerEnter2D(Collider2D hit) 
    {
       Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(this.transform.position, 1f);

            foreach(Collider2D enemy in hitEnemies)
        {
                    if(enemy.GetComponent<EnemyStats>() != null)
            {
                    EnemyStats targetstat = enemy.GetComponent<EnemyStats>();
                    
                    targetstat.TakeDamage((int)hero.skills[0].baseDamage);
                //  pl.DoDamage(targetstat);
                Debug.Log("basedamage is" + (int)hero.skills[0].baseDamage);
                    //hit.GetComponent<Enemy>().Die();
                  
            }
        }
    }

      private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 1f);
    }

    public void destory()
    {
        Destroy(gameObject);
    }
 
}

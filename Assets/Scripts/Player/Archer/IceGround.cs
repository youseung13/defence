using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceGround : MonoBehaviour
{
    public PolygonCollider2D cd;
    private float time;
    public float damagetick = 0.3f;
    public int damage = 3;


    public float radius  = 1f;
    // Start is called before the first frame update

    private void Awake() {
        cd = GetComponent<PolygonCollider2D>();
    }

    private void Start() {
        InvokeRepeating("ApplyDamageToEnemies", 0f, damagetick);
    }
 
    private void Update() 
    {
        time += Time.deltaTime;

    }

        //make enemy slow
    private void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.GetComponent<Enemy>() != null)
        {
            Enemy Enemy = hit.GetComponent<Enemy>();
            Enemy.SlowEntityBy(0.5f, 3f);
           
            EntityFX fx = hit.GetComponent<EntityFX>();
            fx.ChillFxFor(3f);

            
        }
    }
    private void OnTriggerExit2D(Collider2D hit)
    {
        if(hit.GetComponent<Enemy>() != null)
        {
            Enemy targetstat = hit.GetComponent<Enemy>();
            
            Debug.Log("not chilled");
        }
    }

    private void ApplyDamageToEnemies()
    {
        // 원형 영역 내의 모든 Collider2D를 검출
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D collider in colliders)
        {
            // Collider가 Enemy 태그를 가진 오브젝트에 속하는지 확인
            if (collider.GetComponent<EnemyStats>() != null)
            {
                EnemyStats enemy = collider.GetComponent<EnemyStats>();
                if (enemy != null)
                {
                    // 적에게 데미지를 입히거나 다른 동작 수행
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
   void OnDrawGizmosSelected()
    {
       Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }


}


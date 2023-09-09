using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Hero_type
{
    Archer,
    Magician,
    Ninja
}

public class Hero : MonoBehaviour
{
    public Hero_type type;
    public bool canuse;
    public int unlockcost;

    public HeroData herodata;
  public float delayTimer;

    public Sprite sprite;
  public Animator ani;

  public GameManager gm;

  public GameObject bullet;

  public PlayerStats hero_stat;

    public GameObject targetEnemy;
    public LayerMask enemyLayer;
   public float detectInterval =0;
    private bool isdetect;
    private bool canAttack;


bool isRunning = true;

private void Awake() 
{
    ani = GetComponentInChildren<Animator>();
    enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
    hero_stat = GetComponent<PlayerStats>();
    InitializeHeroStats(herodata);
}
private void Start()
{

            // Initialize hero stats with the data from the Scriptable Object


}


  private void Update()
    {
        BattleLogic();

    }

    private void BattleLogic()
    {
        if (!canAttack)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer >= hero_stat.attackDelay.GetflaotValue())
            {
                canAttack = true;
                delayTimer = 0;
            }
        }


        if (!isdetect)
        {
            StartCoroutine(DetectEnemies());
            isdetect = true;
        }

        if (targetEnemy != null && canAttack && Vector2.Distance(transform.position, targetEnemy.transform.position) <= hero_stat.attackRange.GetflaotValue())
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
        Gizmos.DrawWireSphere(transform.position, hero_stat.attackRange.GetflaotValue());
    //    Gizmos.DrawWireCube(transform.position, new Vector3(range,range,0));
    }

    IEnumerator DetectEnemies()
    {
        while (isRunning)
        {
            // Find all enemies within detection radius
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, hero_stat.attackRange.GetflaotValue()*2, enemyLayer);
           
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


   public void InitializeHeroStats(HeroData data)
{
    if (data != null)
    {
        hero_stat.level.SetDefaultValue(data.d_level);
        hero_stat.damage.SetDefaultValue(data.d_damage);
        hero_stat.critChance.SetDefaultFloatValue(data.d_critChance);
        hero_stat.critPower.SetDefaultFloatValue(data.d_critPower);
        hero_stat.attackDelay.SetDefaultFloatValue(data.d_attackDelay);
        hero_stat.attackRange.SetDefaultFloatValue(data.d_attackRange);
    }
    else
    {
        Debug.LogError("InitializeHeroStats: HeroData is not assigned.");
    }
}
}

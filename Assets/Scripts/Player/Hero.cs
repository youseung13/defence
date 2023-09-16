using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Hero_type
{
    Archer,
    Magician,
    Ninja,
    Magic
}

public class Hero : MonoBehaviour
{
    
   public GameManager gm;
    public HeroData herodata;
    public SkillData[] skills;
    // 레벨에 따른 레벨업에 필요한 골드,재화 데이터를 관리하는 스크립트
    public int level;
    public int power;
    public LevelManager levelManager;
    public Hero_type type;
    public bool canuse;
    public int unlockcost;
     public bool skillshot;




    public Sprite sprite;
  public Animator ani;
  public GameObject bullet;
  public PlayerStats hero_stat;
  public int batchindex;

    public GameObject targetEnemy;
    public LayerMask enemyLayer;
      public float delayTimer;
   public float detectInterval =0.5f;
    public bool isdetect;
    public bool canAttack;
    bool isRunning = true;
    private bool hasDetectedEnemy = false;

    #region State
    public HeroStateMachine stateMachine {get; private set;}
    public HeroIdleState idleState  {get; private set;}
    public HeroReadyState readyState { get; private set;}
    public HeroSkillState skillState { get; private set;}
    public HeroAttackState attackState { get; private set;}
    #endregion


private void Awake() 
{
    ani = GetComponentInChildren<Animator>();
    enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
    hero_stat = GetComponent<PlayerStats>();
    InitializeHeroStats(herodata);

    stateMachine = new HeroStateMachine();
    idleState = new HeroIdleState(this, stateMachine, "Idle");
    readyState = new HeroReadyState(this, stateMachine, "Ready");
    skillState = new HeroSkillState(this, stateMachine, "Skill");
    attackState = new HeroAttackState(this, stateMachine, "Attack");
    batchindex = -1;
   
}
private void Start()
{

    stateMachine.Initialize(idleState);

}


  private void Update()
    {
      //  BattleLogic();
      stateMachine.currentState.Update();

    }

    public void Init()
    {
        InitializeHeroStats(herodata);
    }

    public void BattleLogic()
    {
        if(GameManager.instance.game_State == GameManager.Game_State.Battle)
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


        
            if (targetEnemy == null && canAttack)
            {
                hasDetectedEnemy = false;
                StartCoroutine(DetectEnemies());
            }

    
    

            if (targetEnemy != null && canAttack && Vector2.Distance(transform.position, targetEnemy.transform.position) <= hero_stat.attackRange.GetflaotValue())
            {
                stateMachine.ChangeState(attackState);
            }

            if(targetEnemy != null)
            {
                if(targetEnemy.activeSelf == false)
                {
                    targetEnemy = null;
            
                }
            }

        }
    }

    public void Attack()
    {
       
      //ani.SetTrigger("attack");
     GameObject temp = Instantiate(bullet);
      temp.transform.position = transform.position;
      temp.GetComponent<Missile>().target = targetEnemy;
      temp.GetComponent<Missile>().pl = hero_stat;
      temp.GetComponent<Missile>().hero = this;

        if(targetEnemy != null)
      temp.GetComponent<Missile>().SetTarget(targetEnemy.transform);
     
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
/*
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hero_stat.attackRange.GetflaotValue());
    //    Gizmos.DrawWireCube(transform.position, new Vector3(range,range,0));
    }
*/
    IEnumerator DetectEnemies()
    {
        while (!hasDetectedEnemy &&isRunning)
        
        {
          //  Debug.Log("Detecting Enemies");
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
            hasDetectedEnemy = true;
            // Wait for detectInterval seconds before checking for new enemies again
            yield return new WaitForSeconds(detectInterval);


            // 감지 플래그 초기화
        }
    }


   public void InitializeHeroStats(HeroData data)
{
    if (data != null)
    {
        level = 1;
        power = 0;
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

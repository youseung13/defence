/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Slider HPbar;
    private int lap;

    public Transform[] target;

    private Vector3 dir;

    private int targetIndex;

    private Animator ani;

    public float speed ;
    public int hp;
    public int maxhp;
    public int givecoin;
    private bool isDead = false;
    public bool isAlive = true;

    private SpriteRenderer sprite;
     private CapsuleCollider2D cr;

     private Coroutine hpBarCoroutine;
    // Start is called before the first frame update
    private void Awake() {
              cr= GetComponent<CapsuleCollider2D>();
              ani = GetComponent<Animator>();
              sprite = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        if(HPbar !=null)
        {
        HPbar.value = (float)hp/ (float)maxhp;
        HPbar.gameObject.SetActive(false);
        }
       
        targetIndex = 1;
        maxhp = hp;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (GameManager.instance.isPause)
            return;
        if (isDead)
            return;

        Move();

        if(lap==3 && targetIndex==2)
        Gohome();

    }

    private void Gohome()
    {
          
    this.transform.position = Vector2.MoveTowards(this.transform.position, GameManager.instance.startPos.position, Time.deltaTime * speed * 5);
     if (Vector2.Distance(this.transform.position, GameManager.instance.startPos.position) < 0.05f)
    {
         
         GameManager.instance.life--;
         GameManager.instance.aliveenemy.Remove(gameObject);
        GameManager.instance.countText.text = string.Format("{0:F0}", GameManager.instance.aliveenemy.Count);

        GameManager.instance.LifeText.text = string.Format("{0:F0}", GameManager.instance.life);
         Destroy(gameObject);
    }

    }

    private void Move()
    {
        if (targetIndex < 6)
        {
            dir = target[targetIndex].position - this.transform.position;
            dir = dir.normalized * 0.03f;
            if (dir.x > 0)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }

            // speed = dir.magnitude;

            //  this.transform.position += dir;
            this.transform.position = Vector2.MoveTowards(this.transform.position, target[targetIndex].position, Time.deltaTime * speed);

            // Debug.LogFormat("Distance : {0}",Vector2.Distance(this.transform.position,target[targetIndex].position));


            if (Vector2.Distance(this.transform.position, target[targetIndex].position) < 0.02f)
            {
                targetIndex += 1;
                if (targetIndex == 6)
                {
                    lap ++;  
                    targetIndex = 1;
                }
            }
        }
        ani.SetFloat("Speed", speed);
    }

    public void TakeDamage(int _damage)
   {
        hp -= _damage;
         if(HPbar ==null)
         return;


         if (HPbar.gameObject.activeSelf)
        {
            if (hpBarCoroutine != null)
            {
                StopCoroutine(hpBarCoroutine);
            }
            hpBarCoroutine = StartCoroutine(ShowAndHideHPBar());
        }
        else // HP 바가 비활성화된 경우 바로 코루틴 시작
        {
            hpBarCoroutine = StartCoroutine(ShowAndHideHPBar());
        }

        UpdateHPBar();
       

   // GetComponent<Entity>().DamageImpact();
    //fx.StartCoroutine("FlashFX");

     if(hp <0  && !isDead)
     {
        isDead =true;
        ani.SetTrigger("Dead");
     }
   }

    private void UpdateHPBar()
    {
       HPbar.value = (float)hp/ (float)maxhp;
    }

    private void Die()
    {

       
        GameManager.instance.GetCoin(givecoin);
        Destroy(gameObject);
        GameManager.instance.aliveenemy.Remove(this.gameObject);
         GameManager.instance.countText.text = string.Format("{0:F0}", GameManager.instance.aliveenemy.Count);
    }

    IEnumerator ShowAndHideHPBar()
    {
        HPbar.gameObject.SetActive(true); // HP 바 활성화
        yield return new WaitForSeconds(0.8f); // 일정 시간 대기
        HPbar.gameObject.SetActive(false); // HP 바 비활성화
    }
}

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : Entity
{
      public CapsuleCollider2D cd {get; private set;}
   // [SerializeField]protected LayerMask whatIsPlayer;

  
    [Header("Stunned info")]
    public float stunDuration;
   // public Vector2 stunDirction;
    protected bool canBeStunned;
   // [SerializeField] protected GameObject counterImage;
   // public float detectionRadius = 5f;
    public float baseRadius;
   //  public float patrolRange;

    //public Vector2 homePos;
    [Header("Move Info")]
    public float moveSpeed;
    public float baseSpeed;
    public float idleTime;
    public float defaulMoveSpeed;

    

    [Header("Attack Info")]
   public GameObject target;
    public float attackdistance;
    public float attackCooldown;
   [HideInInspector] public float lastTimeAttacked;
   public EnemyStateMachine stateMachine {get; private set;}
   public string lastAnimBoolName {get; private set;}
   private Enemy_Melee enemy;

    public Vector3 dis;
    public Vector3 dis2;
    public float distanceToPlayer;

   protected virtual void OnEnable() {
    // homePos = transform.position;

   }

   public virtual void AssignLastAnimName(string _animBoolName)
   {
        lastAnimBoolName = _animBoolName;
   }

    public override void SlowEntityBy(float _slowPercentage, float _slowDuration)
    {
      moveSpeed = moveSpeed * (1 - _slowPercentage);
      anim.speed = anim.speed * ( 1 - _slowPercentage);

      Invoke("ReturnDefaultSpeed", _slowDuration);


    }



    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();

        moveSpeed = defaulMoveSpeed;
    }


   protected override void Awake() 
   {

      cd = GetComponent<CapsuleCollider2D>();
     target = GameManager.instance.target;
        base.Awake();
        stateMachine = new EnemyStateMachine();
        defaulMoveSpeed = moveSpeed;
     //   baseRadius = detectionRadius;
        baseSpeed = moveSpeed;
        enemy = GetComponent<Enemy_Melee>();
   
        
   }

 
  

   protected override void Update() 
   {

        base.Update();
         dis = new Vector3(enemy.target.transform.position.x,0,0);
        dis2 = new Vector3(enemy.transform.position.x,0,0);
        distanceToPlayer =  Vector3.Distance(dis, dis2);

        stateMachine.currentState.Update();
   }

    #region  Counter Attack Window
   public virtual void OpenCounterAttackWindow()
   {
    canBeStunned = true;
   // counterImage.SetActive(true);
   }

   public virtual void CloseCounterAttackWindow()
   {
    canBeStunned=false;
 //   counterImage.SetActive(false);
   }
   #endregion

   
   public virtual void FreezeTime(bool _timeFrozen)
   {
        if(_timeFrozen)
        {
            moveSpeed = 0;
            anim.speed = 0;
        }
        else
        {
            moveSpeed = defaulMoveSpeed;
            anim.speed= 1;
        }
   }

    public virtual void FreezeTimeFor(float _duration) => StartCoroutine(FreezeTimeCoroutine(_duration));

   protected virtual IEnumerator FreezeTimeCoroutine(float _seconds)
   {
    FreezeTime(true);
   
    yield return new WaitForSeconds(_seconds);
    
    FreezeTime(false);

   }

   
   public virtual bool CanBeStunned()
   {
    if(canBeStunned)
    {
        CloseCounterAttackWindow();
        return true;
    }
    return false;

   }

   public virtual bool CanAttack()
    {
        if(Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
      //  Debug.Log("Attack is on cooldown");

        return false;
    }


    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    private void OnDisable() {
        gameObject.GetComponent<CapsuleCollider2D>().enabled=false;
    }

 
/*
   public virtual Collider2D IsPlayerDetected()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, whatIsPlayer);
        if (colliders.Length > 0)
        {
            Debug.Log("player is detected");
            // At least one collider (player) is detected within the detectionRadius.
            // You can decide what to do here, such as prioritizing the nearest player or just returning the first one found.
            return colliders[0];
        }

        // No player is detected within the detectionRadius.
        return null;
    }
*/
    
     //private void OnDrawGizmosSelected()
   // {
        // Draw the detection range circle in the Scene view.
   //      Gizmos.color = new Color(1f, 0f, 0f, 0.3f); 
    //    Gizmos.DrawWireSphere(transform.position, detectionRadius);
   // }

   // protected override void OnDrawGizmos() 
   // {
    //    base.OnDrawGizmos();
    //    Gizmos.color = Color.yellow;
       
    //    Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackdistance * facingDir, transform.position.y));          
        
   // }

    private void OnTriggerEnter2D(Collider2D other) 
    {
      //  if(Vector2.Distance(player.transform.position, transform.position) > detectionRadius )
     //   {
          //  StartCoroutine("hitAggro", 4f);
         //   Debug.Log("aggroget");
       // }
    }

    // =>
}


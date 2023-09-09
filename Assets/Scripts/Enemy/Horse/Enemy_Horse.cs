using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Horse : Enemy
{
    public GameObject Original;
    #region  States

    public Horse_IdleState idleState {get; private set;}
    public Horse_MoveState moveState {get; private set;}
   // public SkeletonPatrolState patrolState {get; private set;}
   // public SkeletonBattleState battleState {get; private set;}

     public Horse_AttackState attackState {get; private set;}

    // public SkeletonStunnedState stunnedstate { get; private set;}
     public Horse_DeathState deadstate { get; private set;}
    #endregion
    protected override void Awake()
    {
        base.Awake();

        idleState = new Horse_IdleState(this, stateMachine, "Idle", this);
        moveState = new Horse_MoveState(this, stateMachine, "Move", this);
       // patrolState = new SkeletonPatrolState(this, stateMachine, "Patrol", this);
       // battleState = new SkeletonBattleState(this, stateMachine, "Move", this);

        attackState = new Horse_AttackState(this, stateMachine, "Attack", this);
       // stunnedstate = new SkeletonStunnedState(this, stateMachine, "Stunned", this);
        deadstate = new Horse_DeathState(this, stateMachine, "Die",this);
    }

    protected override void OnEnable() 
    {
        base.OnEnable();
        gameObject.GetComponent<CapsuleCollider2D>().enabled=true;
        gameObject.GetComponent<EnemyStats>().Initialize();
        gameObject.GetComponent<EnemyStats>().SetHPbar();

        if(stateMachine.currentState != null)
         stateMachine.ChangeState(idleState);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);

    
    }

    protected override void Update()
    {
        base.Update();

      

      
    }

  

/*
    public override bool CanBeStunned()
    {
       if(base.CanBeStunned())
       {
        stateMachine.ChangeState(stunnedstate);
        return true;
       }
       return false;
    }
*/
    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadstate);
    }

    public override void Deactive()
      {
        gameObject.SetActive(false);
    }

   
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Melee : Enemy
{
    #region  States

    public Enemy_IdleState idleState {get; private set;}
    public Enemy_MoveState moveState {get; private set;}
   // public SkeletonPatrolState patrolState {get; private set;}
   // public SkeletonBattleState battleState {get; private set;}

     public Enemy_AttackState attackState {get; private set;}

    // public SkeletonStunnedState stunnedstate { get; private set;}
     public Enemy_DeathState deadstate { get; private set;}
    #endregion
    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(this, stateMachine, "Idle", this);
        moveState = new Enemy_MoveState(this, stateMachine, "Move", this);
       // patrolState = new SkeletonPatrolState(this, stateMachine, "Patrol", this);
       // battleState = new SkeletonBattleState(this, stateMachine, "Move", this);

        attackState = new Enemy_AttackState(this, stateMachine, "Attack", this);
       // stunnedstate = new SkeletonStunnedState(this, stateMachine, "Stunned", this);
        deadstate = new Enemy_DeathState(this, stateMachine, "Die",this);
    }

    protected override void OnEnable() 
    {
        gameObject.GetComponent<CapsuleCollider2D>().enabled=true;
        gameObject.GetComponent<EnemyStats>().Initialize();

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
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Melee : Enemy
{
    #region  States

    public Melee_IdleState idleState {get; private set;}
    public Melee_MoveState moveState {get; private set;}
   // public SkeletonPatrolState patrolState {get; private set;}
   // public SkeletonBattleState battleState {get; private set;}

     public Melee_AttackState attackState {get; private set;}

    // public SkeletonStunnedState stunnedstate { get; private set;}
     public Melee_DeathState deadstate { get; private set;}
     public Melee_StandState standstate { get; private set;}
    #endregion
    protected override void Awake()
    {
        base.Awake();

        idleState = new Melee_IdleState(this, stateMachine, "Idle", this);
        moveState = new Melee_MoveState(this, stateMachine, "Move", this);
       // patrolState = new SkeletonPatrolState(this, stateMachine, "Patrol", this);
       // battleState = new SkeletonBattleState(this, stateMachine, "Move", this);

        attackState = new Melee_AttackState(this, stateMachine, "Attack", this);
       // stunnedstate = new SkeletonStunnedState(this, stateMachine, "Stunned", this);
        deadstate = new Melee_DeathState(this, stateMachine, "Die",this);
        standstate = new Melee_StandState(this, stateMachine, "Stand", this);
    }

    protected override void OnEnable() 
    {
        base.OnEnable();
      //   Debug.Log("Onenable");
        gameObject.GetComponent<CapsuleCollider2D>().enabled=true;
        gameObject.GetComponent<EnemyStats>().Initialize();
        gameObject.GetComponent<EnemyStats>().SetHPbar();

         if(stateMachine.currentState != null && standHorse)
             stateMachine.ChangeState(standstate); 

        if(stateMachine.currentState != null && !standHorse)
        {
            stateMachine.ChangeState(idleState); 
            Debug.Log("idle from onable");
        }
        

      
        
    }

    protected override void Start()
    {
        base.Start();

            if(standHorse)
        stateMachine.Initialize(standstate);
        else
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
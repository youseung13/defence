using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_StandState : EnemyState
{
    private Enemy_Melee enemy;

    public Melee_StandState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Melee _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    { // Debug.Log("Stand state is start");
        base.Enter();
        //enemy.SetZeroVelocity();
    stateTimer = 0.7f;
       
    }

    public override void Exit()
    {
        
        base.Exit();
       
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            // Change the state to the idle state (or another appropriate state)
            stateMachine.ChangeState(enemy.idleState);
        }

    

    }
}
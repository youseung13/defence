using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse_IdleState : EnemyState
{
    private Enemy_Horse enemy;

    public Horse_IdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Horse _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    { //  Debug.Log("idle state is start");
        base.Enter();
        //enemy.SetZeroVelocity();

       
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

       

      if(enemy.moveSpeed > 0.1 && enemy.distanceToPlayer > enemy.attackdistance) // if(enemy.moveSpeed > 0.1)
        stateMachine.ChangeState(enemy.moveState);
        
        if (enemy.distanceToPlayer <= enemy.attackdistance && enemy.CanAttack())
        {
            stateMachine.ChangeState(enemy.attackState);
        }

    }
}
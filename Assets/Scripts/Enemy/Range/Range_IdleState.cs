using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range_IdleState : EnemyState
{
    private Enemy_Range enemy;

    public Range_IdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Range _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {   //Debug.Log("idle state is start");
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

       

      if(enemy.moveSpeed > 0.1 && Vector2.Distance(enemy.target.transform.position, enemy.transform.position) > enemy.attackdistance) // if(enemy.moveSpeed > 0.1)
        stateMachine.ChangeState(enemy.moveState);
        
        if (Vector2.Distance(enemy.target.transform.position, enemy.transform.position) < enemy.attackdistance && enemy.CanAttack())
        {
            stateMachine.ChangeState(enemy.attackState);
        }

    }
}
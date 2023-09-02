using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_AttackState : EnemyState
{
    private Enemy_Melee enemy;
    public Melee_AttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Melee _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        Debug.Log("attack state is start");
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
       // enemy.SetZeroVelocity();

        if(triggerCalled)
        {
            if (enemy.distanceToPlayer <= enemy.attackdistance != enemy.CanAttack())
            {
                 stateMachine.ChangeState(enemy.idleState);
            }
            else
                 stateMachine.ChangeState(enemy.moveState);
        }
   
    }

       public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttacked = Time.time;

    }
}

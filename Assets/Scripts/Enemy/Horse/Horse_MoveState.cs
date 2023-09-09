using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse_MoveState : EnemyState
{
    private Enemy_Horse enemy;

    public Horse_MoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Horse _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
       // Debug.Log("move state is start");
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
  
        //enemyBase.FlipController(player.transform.position.x -enemyBase.transform.position.x);

        enemy.transform.Translate(Vector3.left * enemy.moveSpeed * Time.deltaTime);



        if (enemy.distanceToPlayer <= enemy.attackdistance && enemy.CanAttack())
        {
            stateMachine.ChangeState(enemy.attackState);
        }

        //enemy.SetVelocity(enemy.moveSpeed*enemy.facingDir, enemy.moveSpeed*enemy.facingDir);


     
    }


    
}

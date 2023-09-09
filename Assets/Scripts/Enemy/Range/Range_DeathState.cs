using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range_DeathState : EnemyState
{
    private Enemy_Range enemy;
    public Range_DeathState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,Enemy_Range _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        Debug.Log("death state is start");
        base.Enter();

    //    enemy.anim.SetBool(enemy.lastAnimBoolName,true);
    //    enemy.anim.speed=0;
        enemy.cd.enabled =false;

     //   stateTimer = .5f;
        Debug.Log("enter dead state");
        
    }

    public override void Exit()
    {
        
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}

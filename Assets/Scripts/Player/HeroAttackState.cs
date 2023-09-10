using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackState : HeroState
{
    public HeroAttackState(Hero _player, HeroStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
       // Debug.Log("공격");
        base.Enter();
       // player.SetZeroVelocity();//공격중일떄 못움직이게
       hero.canAttack = false;
       
    }

    public override void Exit()
    {
       // Debug.Log("공격끝");
        base.Exit();
        

        
    }

    public override void Update()
    {
       base.Update();

       if(triggerCalled)
        stateMachine.ChangeState(hero.idleState);  
       
    }

   
}

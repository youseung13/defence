using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroIdleState : HeroState
{
    public HeroIdleState(Hero _player, HeroStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {   
       // Debug.Log("idle");
        base.Enter();
       // player.SetZeroVelocity();//공격중일떄 못움직이게
       
    }

    public override void Exit()
    {
     //   Debug.Log("idle끝");
        base.Exit();
    }

    public override void Update()
    {
       base.Update();

       if(Input.GetKeyDown(KeyCode.Space))
       {
           stateMachine.ChangeState(hero.skillState);
       }

       hero.BattleLogic();
       
    }
}

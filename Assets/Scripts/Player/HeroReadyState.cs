using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroReadyState : HeroState
{
    public HeroReadyState(Hero _player, HeroStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
       // Debug.Log("공격");
        base.Enter();
       // player.SetZeroVelocity();//공격중일떄 못움직이게
       
       
    }

    public override void Exit()
    {
       // Debug.Log("공격끝");
        base.Exit();
        

        
    }

    public override void Update()
    {
       base.Update();

       if(hero.skillshot == true)
       {
         stateMachine.ChangeState(hero.skillState);  
         Debug.Log("스킬");
       }
       
       
    }

   
}

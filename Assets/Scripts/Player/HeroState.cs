using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroState 
{
    protected HeroStateMachine stateMachine;
   protected Hero hero;

    private string animBoolName;
    protected float stateTimer;
   protected bool triggerCalled;
    // Start is called before the first frame update
     public HeroState(Hero _player,HeroStateMachine _stateMachine, string _animBoolName)
   {
    this.hero = _player;
    this.stateMachine = _stateMachine;
    this.animBoolName = _animBoolName;
   }


   public virtual void Enter()
   {
    hero.ani.SetBool(animBoolName, true);
    triggerCalled = false;
   }


    public virtual void Update()
   {
    stateTimer -= Time.deltaTime;
   }

   public virtual void Exit()
   {
    hero.ani.SetBool(animBoolName, false);
   }

   public virtual void AnimationFinishTrigger()
   {
    triggerCalled=true;
   }


}

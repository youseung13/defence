using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSkillState : HeroState
{
    public HeroSkillState(Hero _player, HeroStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
       // Debug.Log("스킬");
   //     stateTimer = 2.0f;
            ShotSkill();
            hero.skillshot = false;
       // player.SetZeroVelocity();//공격중일떄 못움직이게
       
       
    }

    public override void Exit()
    {   
     //    Debug.Log("스킬끝");
        base.Exit();
     
       
    }

    public override void Update()
    {
       base.Update();

       if(triggerCalled && stateTimer<=0.0f)
        stateMachine.ChangeState(hero.idleState);
       
    }

    public void ShotSkill()
    {
        Debug.Log("스킬발동");

        if(hero.type == Hero_type.Archer)
        {
            if(hero.GetComponent<SkillController>().skillIndex == 0)
            {
                hero.GetComponent<SkillController>().UseMultiShot(hero.GetComponent<SkillController>().skills[0].targetPosition);
            }
        }
        else if(hero.type == Hero_type.Magician)
        {
            if(hero.GetComponent<SkillController>().skillIndex == 0)
            {
                hero.GetComponent<SkillController>().skills[0].projectilePrefab.gameObject.GetComponent<BllizardSkill>().UseBllizardSkill(hero.GetComponent<SkillController>().skills[0].targetPosition);
            }
        }
    }


}

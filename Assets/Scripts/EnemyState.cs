using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
  public Transform castle;
  protected EnemyStateMachine stateMachine;
  protected Enemy enemyBase;
  protected Rigidbody2D rb;


  protected bool triggerCalled;
  private string animBoolName;
  protected float stateTimer;
  protected float stateTimer2;


  public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName)
  {
    this.enemyBase = _enemyBase;
    this.stateMachine = _stateMachine;
    this.animBoolName = _animBoolName;
  }

  public virtual void Update()
  {
    
    stateTimer -= Time.deltaTime;
     //stateTimer2 -= Time.deltaTime;
  }

  public virtual void Enter()
  {
    triggerCalled =false;//트리거 이용해서 애니메이션 끝낼떄 필요
    rb = enemyBase.rb;
 //     castle = GameManager.instance.castle.transform;
    enemyBase.anim.SetBool(animBoolName, true);

  
  }

  public virtual void Exit()
  {
     enemyBase.anim.SetBool(animBoolName, false);
     enemyBase.AssignLastAnimName(animBoolName);
  }

  public virtual void AnimationFinishTrigger()
  {
    triggerCalled = true;
  }

  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AnimationTriggers : MonoBehaviour
{
     private Enemy enemy => GetComponentInParent<Enemy>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
       Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

       foreach(var hit in colliders)
       {
           if(hit.GetComponent<Castle>() != null)
           {
//              Debug.Log("enemy attack");
               Castle target = hit.GetComponent<Castle>();
               enemy.stats.DoDamage(target);

            }

            
       
       }
    }

     private void Dietrigger()
    {
        enemy.AnimationFinishTrigger();
        enemy.anim.Rebind();
        enemy.Deactive();
    }

    private void Stand()
    {
        enemy.StandFromHorse();
    }

    private void RangeAttack()
    {
        enemy.FireEnemyBullet();
        
    }

    private void OpenCounterAttackWindow() => enemy.OpenCounterAttackWindow();
    private void CloseCounterAttackWindow() => enemy.CloseCounterAttackWindow();

}

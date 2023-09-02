using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AnimationTriggers : MonoBehaviour
{
    private Enemy_Melee enemy => GetComponentInParent<Enemy_Melee>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach(var hit in colliders)
        {
           // if(hit.GetComponent<Player2>() != null)
            {
               // PlayerStats target = hit.GetComponent<PlayerStats>();
                //enemy.stats.DoDamage(target);

            }
       
        }
    }

     private void Dietrigger()
    {
        enemy.Deactive();
    }

    private void OpenCounterAttackWindow() => enemy.OpenCounterAttackWindow();
    private void CloseCounterAttackWindow() => enemy.CloseCounterAttackWindow();

}

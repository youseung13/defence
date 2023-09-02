﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Hero hero;
 
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        hero = GetComponent<Hero>();
    }


    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

    }

    protected override void Die()
    {
        base.Die();

     //   hero.Die();
    }

    protected override void DecreaseHealthBy(int _damage)
    {
        base.DecreaseHealthBy(_damage);

        //ItemData_Equipment currentArmor = Inventory.instance.GetEquipment(EquipmentType.Armor);//아머 착용여부 가져오고

      //  if(currentArmor != null)//비어있지않고 입고있으면
        {


      //      currentArmor.Effect(hero.transform);//가지고있는 효과 실행
        }

    }

    public override void OnEvasion()
    {
     //   hero.skill.dodge.CreateMirageDodge();
       Debug.Log("Player avoided");
    }

    public void CloneDoDamage(CharacterStats _targetStats, float _multiplier)
    {   
        if (TargetCanAvoidAttack(_targetStats))
            return;

        int totalDamage = damage.GetValue() + strength.GetValue();    

        if(_multiplier >0)
            totalDamage = Mathf.RoundToInt(totalDamage * _multiplier);



        if(CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
        }

        totalDamage = CheckTargetArmor(_targetStats, totalDamage);
        _targetStats.TakeDamage(totalDamage);

        //if inventory current weapon has fire effect
        DoMagicalDamage(_targetStats);//평타에 속성데미지 추가 ailemnt on primary attack

    }
}

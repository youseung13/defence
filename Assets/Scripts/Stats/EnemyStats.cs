﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;
   // private ItemDrop myDropSystem;

    [Header("Level details")]
    [SerializeField] private int level = 1;

    [Range(0f,1f)]
    [SerializeField] private float percentageModifier = .15f;
   protected override void Start()
    {
        ApplyLevelModifiers();

        base.Start();

        enemy = GetComponent<Enemy>();
       // myDropSystem = GetComponent<ItemDrop>();

    }

    protected void ApplyLevelModifiers()
    {
        Modify(strength);
        Modify(agility);
        Modify(intelligence);
        Modify(vitality);

        Modify(damage);
        Modify(critChance);
        Modify(critPower);

        Modify(maxHealth);
        Modify(health);
        Modify(armor);
        Modify(evasion);
        Modify(magicResistance);

        Modify(fireDamage);
        Modify(iceDamage);
        Modify(lightingDamage);
    }

    private void Modify(Stat _stat)
    {
        for (int i = 1; i < level; i++)
        {
            float modifer = _stat.GetValue() * percentageModifier; //

            _stat.AddModifier(Mathf.RoundToInt(modifer));
        }
    }

    public override void TakeDamage(int _damage)
        {
            base.TakeDamage(_damage);

        }

    protected override void Die()
    {
        base.Die();

        enemy.Die();

       // myDropSystem.GenerateDrop();
    }

      public override void Initialize()
      {
        base.Initialize();
      }
        
}


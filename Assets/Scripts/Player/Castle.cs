﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : CharacterStats
{
    
    public int hp;
    public int maxhp;

    [SerializeField]
    public Slider HPbar;
    [SerializeField]
    public Text HPbartext;

    public BoxCollider2D cd {get; private set;}

    private void OnEnable() {
        init();
        Debug.Log("캐슬 초기화");
    }

    private void Awake()
    {     
        cd = GetComponent<BoxCollider2D>();  
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

    }

    public void init()
    {
        maxhp = Player.instance.castlehp;
        currentHealth = maxhp;

    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        UI_Manager.instance.UpdateHPBar();

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


}

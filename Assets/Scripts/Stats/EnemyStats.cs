using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : CharacterStats
{
      [SerializeField]
    private Slider HPbar;
     private Coroutine hpBarCoroutine;

    private Enemy enemy;
   // private ItemDrop myDropSystem;

    [Header("Level details")]
    [SerializeField] private int enemylevel = 1;

    [Range(0f,1f)]
    [SerializeField] private float percentageModifier = .15f;
   protected override void Start()
    {
        ApplyLevelModifiers();

        base.Start();

        enemy = GetComponent<Enemy>();

        SetHPbar();
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
        for (int i = 1; i < enemylevel; i++)
        {
            float modifer = _stat.GetValue() * percentageModifier; //

            _stat.AddModifier(Mathf.RoundToInt(modifer));
        }
    }

    public override void TakeDamage(int _damage)
        {
            base.TakeDamage(_damage);

            
         if (HPbar.gameObject.activeSelf)
        {
            if (hpBarCoroutine != null)
            {
                StopCoroutine(hpBarCoroutine);
            }
            hpBarCoroutine = StartCoroutine(ShowAndHideHPBar());
        }
        else // HP 바가 비활성화된 경우 바로 코루틴 시작
        {
            hpBarCoroutine = StartCoroutine(ShowAndHideHPBar());
        }

        UpdateHPBar();

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

    public void SetHPbar()
    {
        if(HPbar !=null)
        {
        HPbar.value = (float)currentHealth/ (float)GetMaxHealthValue();
        HPbar.gameObject.SetActive(false);
        }
    }

      
    private void UpdateHPBar()
    {
       HPbar.value = (float)currentHealth/ (float)GetMaxHealthValue();
    }

  

    IEnumerator ShowAndHideHPBar()
    {
        HPbar.gameObject.SetActive(true); // HP 바 활성화
        yield return new WaitForSeconds(0.8f); // 일정 시간 대기
        HPbar.gameObject.SetActive(false); // HP 바 비활성화
    }
        
}


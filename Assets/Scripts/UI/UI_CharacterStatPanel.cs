using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterStatPanel : MonoBehaviour
{
    public GameObject heroUnit;
    public Text heroName;
    public Text heroLevel;
    public Text heroDamage;
    public Text heroAttackSpeed;
    public Text heroCriticalchance;
    public Text heroCriticalDamage;

    // Start is called before the first frame update
    void Start()
    {
  
        
        //initail function for clear panel
        ClearPanel();
    }

    public void ClearPanel()
    {
        heroUnit.GetComponent<Image>().sprite = null;
        heroUnit.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        heroName.text = "";
        heroLevel.text = "";
        heroDamage.text = "";
        heroAttackSpeed.text = "";
        heroCriticalchance.text = "";
        heroCriticalDamage.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCharacterInfo(int _slotindex)
    {

       
   
        Hero hero = Player.instance.heroprefab[_slotindex].GetComponent<Hero>();
        //GameObject  heroImage = Instantiate(Resources.Load<GameObject>("SPUM/SPUM_Units/" + hero.type.ToString()));
        //heroImage.transform.SetParent(heroUnit.transform);
        //heroImage.transform.localScale = new Vector3(100, 100, 100);  
        //heroImage.transform.localPosition = new Vector3(0, 0, 0);
        heroUnit.GetComponent<Image>().sprite = hero.sprite;
        heroUnit.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        heroName.text = hero.type.ToString();
        heroLevel.text = "Level : " + hero.level.ToString();
        heroDamage.text = "Damage : " + hero.hero_stat.damage.GetValue().ToString();
        heroAttackSpeed.text = "Attack Speed : " + hero.hero_stat.attackDelay.GetflaotValue().ToString();
        heroCriticalchance.text = "Critical : " + hero.hero_stat.critChance.GetflaotValue().ToString();
        heroCriticalDamage.text = "Critical Damage : " + hero.hero_stat.critPower.GetflaotValue().ToString();



    }


}








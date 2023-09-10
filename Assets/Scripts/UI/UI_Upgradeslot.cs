using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Upgradeslot : MonoBehaviour
{
    public GameObject statrpanel;
    public Text goldcost;
     public Text goldcost2;
    public Text gemcost;
    public Text gemcost2;
    public Button levelup;
    public int _index;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame


        public void SetneedLevelUP(int index)
        {
            if (goldcost != null && gemcost != null)
            {
            goldcost.text = GameManager.instance.LevelManager.GetComponent<LevelManager>().
            Setrequiredgold(Player.instance.transform.GetChild(index).GetComponent<Hero>().level).ToString();

            gemcost.text = GameManager.instance.LevelManager.GetComponent<LevelManager>().
            Setrequiredgem(Player.instance.transform.GetChild(index).GetComponent<Hero>().level).ToString();
        
            }

        }

         public void SetneedUpgrade(int index)
        {
            if (goldcost2 != null && gemcost2 != null)
            {
            goldcost2.text = GameManager.instance.LevelManager.GetComponent<LevelManager>().
            Setrequiredgold2(Player.instance.transform.GetChild(index).GetComponent<Hero>().level).ToString();

            gemcost2.text = GameManager.instance.LevelManager.GetComponent<LevelManager>().
            Setrequiredgem2(Player.instance.transform.GetChild(index).GetComponent<Hero>().level).ToString();

            }

        }

        public void CheckUpgrade(int index)
        {
            index = _index;
            if(Player.instance.gold >= int.Parse(goldcost.text) && Player.instance.diamond >= int.Parse(gemcost.text))
            {
                Player.instance.gold -= int.Parse(goldcost.text);
                Player.instance.diamond -= int.Parse(gemcost.text);
               Player.instance.HeroLevelup(index);
            }
            else
            {
              
            }
            SetneedLevelUP(index);
            statrpanel.GetComponent<UI_CharacterStatPanel>().SetCharacterInfo(index);
        }

         public void CheckUpgrade2(int index)
        {
            index = _index;
            if(Player.instance.gold >= int.Parse(goldcost2.text) && Player.instance.diamond >= int.Parse(gemcost2.text))
            {
             
                Player.instance.gold -= int.Parse(goldcost2.text);
                Player.instance.diamond -= int.Parse(gemcost2.text);
                Player.instance.HeroPowerUp(index);
            }
            else
            {
              
            }
            SetneedUpgrade(index);
            statrpanel.GetComponent<UI_CharacterStatPanel>().SetCharacterInfo(index);
            
        }
}


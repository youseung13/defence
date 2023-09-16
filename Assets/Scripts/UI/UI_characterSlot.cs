using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_characterSlot : MonoBehaviour
{
    public Image icon;
    public Text nameText;
    public Text costtext;
    public Image slotImage; // 슬롯 이미지
    public int slotindex;

    public bool isSelected = false;
    public bool canbuy;
    public bool isbuy;
    public Hero hero;


    private void Awake()
{
   // for (int i = 0; i < Player.instance.heroprefab.Length; i++)
   // {
   //     hero[i] =  Player.instance.heroprefab[i].GetComponent<Hero>();
  //  }

  
//    icon = GetComponentInChildren<Image>(); // 자식 오브젝트에서 Image 컴포넌트 가져오기
 //   nameText = GetComponentInChildren<Text>(); // 자식 오브젝트에서 Text 컴포넌트 가져오기
}

private void Update() {

}

    public void SetCharacterInfo(Hero hero)
{
    if (icon != null && nameText != null)
    {
        icon.sprite = hero.sprite;
        nameText.text = hero.type.ToString();
        costtext.text = hero.unlockcost.ToString();
        CheckBuy();

    }
    else
    {
        Debug.LogError("icon 또는 nameText가 없습니다.");
    }
}



        public void CheckBuy()
        {
            if (Player.instance.gold >= int.Parse(costtext.text))
            {
                canbuy = true;
            }
            else
            {
                canbuy = false;
            }
        }


    private void OnDisable() {
        isSelected = false;
        slotImage.color = Color.white;
        UI_Manager.instance.batchbuttonoff();
    }
   
}

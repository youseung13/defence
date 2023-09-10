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


    private void Awake()
{
//    icon = GetComponentInChildren<Image>(); // 자식 오브젝트에서 Image 컴포넌트 가져오기
 //   nameText = GetComponentInChildren<Text>(); // 자식 오브젝트에서 Text 컴포넌트 가져오기
}

    public void SetCharacterInfo(Hero hero)
{
    if (icon != null && nameText != null)
    {
        icon.sprite = hero.sprite;
        nameText.text = hero.type.ToString();
        costtext.text = hero.unlockcost.ToString();
        CheckBuy();

/*
        // canuse 여부에 따라 슬롯의 색상을 설정
        if ((Player.instance.herounlock[slotindex]))
        {
            // 사용 가능한 경우, 색상을 원래대로 설정
            icon.color = Color.white;
            nameText.color = Color.white;
        }
        else
        {
            // 사용 불가능한 경우, 회색으로 설정
            icon.color = Color.gray;
            nameText.color = Color.gray;
        }
*/ 
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

   
}

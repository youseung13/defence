using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterPnael : MonoBehaviour
{
    public GameObject[] characterSlot;
    private Player playerInstance;
    public Button buyButton;
    private UI_characterSlot selectedSlot;

   private void OnEnable()
{
    playerInstance = Player.instance; // 전역 Player 인스턴스에 접근

    // characterSlot에 히어로 정보 표시
    for (int i = 0; i < characterSlot.Length; i++)
    {
        UI_characterSlot slot = characterSlot[i].GetComponent<UI_characterSlot>();
        Hero hero = playerInstance.heroprefab[i].GetComponent<Hero>();

        // 슬롯 정보 설정 및 히어로 정보 전달
        slot.SetCharacterInfo(hero);
        slot.slotindex = i;
    }
}
    public void OnClickSlot(UI_characterSlot slot)
    {


        if(selectedSlot == slot && slot.isSelected)
        {
            slot.isSelected = false;
            slot.slotImage.color = Color.white;
            buyButton.gameObject.SetActive(false);
            return;
        }

        if (selectedSlot != null)
        {
            selectedSlot.isSelected = false;
            selectedSlot.slotImage.color = Color.white;
        }

        selectedSlot = slot;
        selectedSlot.isSelected = true;
        selectedSlot.slotImage.color = Color.green;
        buyButton.gameObject.SetActive(true);
        ColorButton(slot);

        if(slot.isbuy)
        {
            buyButton.gameObject.SetActive(false);
        }

    }

    public void ColorButton(UI_characterSlot slot)
    {
        if(slot.canbuy)
        buyButton.image.color = Color.red;
        else
        buyButton.image.color = Color.gray;
    }

    public void BuyHero()
    {
        if(selectedSlot.canbuy && !selectedSlot.isbuy)
        {
            selectedSlot.canbuy = false;
            selectedSlot.isbuy = true;
            Player.instance.gold -= int.Parse(selectedSlot.costtext.text);
            Player.instance.herounlock[selectedSlot.slotindex] = true;
            Player.instance.CheckHaveHero();
            selectedSlot.isSelected = false;
            selectedSlot.slotImage.color = Color.white;
            buyButton.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("돈이 부족합니다.");
        }
        selectedSlot.SetCharacterInfo(Player.instance.heroprefab[selectedSlot.slotindex].GetComponent<Hero>());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterPanel : MonoBehaviour
{
    public GameObject[] characterSlot;

    public UI_CharacterStatPanel characterStatPanel;
    private Player playerInstance;
    public Button buyButton;
    public GameObject Levelup;
    public GameObject Upgrade;
    public UI_characterSlot selectedSlot;

     Hero hero;

   private void OnEnable()
{
    playerInstance = Player.instance; // 전역 Player 인스턴스에 접근

    // characterSlot에 히어로 정보 표시
    for (int i = 0; i < characterSlot.Length; i++)
    {
        UI_characterSlot slot = characterSlot[i].GetComponent<UI_characterSlot>();
        slot.hero = playerInstance.heroprefab[i].GetComponent<Hero>();

        // 슬롯 정보 설정 및 히어로 정보 전달
        slot.SetCharacterInfo(slot.hero);
        slot.slotindex = i;
    }
}

/*
    public void OnClickSlot(UI_characterSlot slot)
    {

        if(selectedSlot == slot && slot.isSelected)
        {
            slot.isSelected = false;
            slot.slotImage.color = Color.white;
        //    buyButton.gameObject.SetActive(false);
            characterStatPanel.ClearPanel();
            //해제
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
       // buyButton.gameObject.SetActive(true);
   
        slot.CheckBuy();
     //        ColorButton(slot);
        //method that shows character stat info panel when certain slot is selected creating from here cause i need hero's values
        characterStatPanel.SetCharacterInfo(slot.slotindex);


    
        if(slot.isbuy || !slot.canbuy)
        {
            buyButton.gameObject.SetActive(false);
        }
    

    }
*/
public void OnClickSlot(UI_characterSlot slot)
    {
        if(UI_Manager.instance.currentState == UI_Manager.UIState.CharacterScene)
        {
                if(selectedSlot == slot && slot.isSelected)
            {
                slot.isSelected = false;
                slot.slotImage.color = Color.white;
                characterStatPanel.ClearPanel();
                Levelup.SetActive(false);
                Upgrade.SetActive(false);   
                //해제
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
            Levelup.SetActive(true);
            Upgrade.SetActive(true);
            Levelup.GetComponent<UI_Upgradeslot>()._index = slot.slotindex;
            Upgrade.GetComponent<UI_Upgradeslot>()._index = slot.slotindex;
            slot.CheckBuy();
            //method that shows character stat info panel when certain slot is selected creating from here cause i need hero's values
            characterStatPanel.SetCharacterInfo(slot.slotindex);
            Levelup.GetComponent<UI_Upgradeslot>().SetneedLevelUP(slot.slotindex);
            Upgrade.GetComponent<UI_Upgradeslot>().SetneedUpgrade(slot.slotindex);
        }

         if(UI_Manager.instance.currentState == UI_Manager.UIState.HeroBatchScene)
         {
                if(selectedSlot == slot && slot.isSelected)
                {
                    slot.isSelected = false;
                    slot.slotImage.color = Color.white;
                    GameManager.instance.selectedhero = false;
                    DeselecteSlot();
                    //해제
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
               GameManager.instance.selectedhero  = true;
                GameManager.instance.selectedSlot = slot;
               UI_Manager.instance.batchbuttonon();
                //method that shows character stat info panel when certain slot is selected creating from here cause i need hero's values
                //여기가 슬롯이 선택됐을때 로직인데, 이 상태에서 게임매니저의 heropoints 중에 한 곳을 선택하는 선택모드가 실행됨 
         }

        
      //  Levelup.GetComponent<UI_Upgradeslot>().CheckUpgrade(slot.slotindex);
     //   Upgrade.GetComponent<UI_Upgradeslot>().CheckUpgrade2(slot.slotindex);



    

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
         //   buyButton.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("돈이 부족합니다.");
        }
        selectedSlot.SetCharacterInfo(Player.instance.heroprefab[selectedSlot.slotindex].GetComponent<Hero>());
    }

  
    public void DeselecteSlot()
    {
        for(int i = 0; i < characterSlot.Length; i++)
        {
            UI_characterSlot slot = characterSlot[i].GetComponent<UI_characterSlot>();
            slot.isSelected = false;
            slot.slotImage.color = Color.white;
        }
        UI_Manager.instance.batchbuttonoff();
    }
    

    // new function for showing character stat info panel when certain slot is selected

}

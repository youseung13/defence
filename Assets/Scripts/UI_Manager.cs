using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;
    public Player pl;
    public Castle castle;
    public Button StageButton;
    public GameObject MainUI;
    public GameObject InGameUI;
    public GameObject CharacterPnael;
    public GameObject CurrencyPanel;

    
    bool isActived = false;

    public Text CoinText;

    public Text DiaText;

    public Text lvText;

    public Text countText;

    public Text LifeText;

    public Text timeText;
    
       private void Awake() 
    {
        
          // 이미 인스턴스가 존재하면 현재 인스턴스를 파괴하고 새로 생성하지 않습니다.
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this; // 현재 인스턴스를 설정합니다.
        DontDestroyOnLoad(gameObject); // 씬 전환 시에도 인스턴스가 유지되도록 설정합니다.
        
    }
    // Start is called before the first frame update
    void Start()
    {
        pl = GameManager.instance.player;
        UpdateUI();
        MainUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
         timeText.text = string.Format("Time : {0:N1}", GameManager.instance.timer);
    }

    public void FinishStageUI()
    {     
       
        MainUI.SetActive(true);
        InGameUI.SetActive(false);
        CurrencyPanel.SetActive(true);
        UpdateUI();
    }

    public void StageStartUI()
    {     
        MainUI.SetActive(false);
        InGameUI.SetActive(true);
        CurrencyPanel.SetActive(false);
        UpdateUI();
    }

    public void ShowCharacterPnael()
    {     
         isActived = !isActived;
        CharacterPnael.SetActive(isActived);
        MainUI.SetActive(!isActived);
    }


    public void UpdateUI()
    {
        CoinText.text = string.Format("{0:F0}", pl.gold);
        DiaText.text = string.Format("{0:F0}", pl.diamond);
        lvText.text = string.Format("Lv : {0:F0}", pl.level);

    }

    public void UpdateHPBar()
    {
       castle.HPbar.value = (float)castle.currentHealth/ (float)castle.maxhp;
       castle.HPbartext.text = string.Format("{0:F0}/{1:F0}",(float)castle.currentHealth, (float)castle.maxhp);
    }
}

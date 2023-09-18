using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public enum UIState
    {
        MainScene,
        BattleScene,
        CharacterScene,
        HeroBatchScene,
        
    }
    public UIState currentState;
    public static UI_Manager instance;
    public Player pl;
    public Button StageButton;
    public GameObject[] MainSceneUI;
    public GameObject[] BattleSceneUI;
    public GameObject[] CharaterUI;
    public Button batchButton;
    public Button batchButton2;

    public GameObject VictoryPanel;
    public GameObject DefeatPanel;

    public GameObject StartButton;

    public FadeScreen fadeScreen;

    bool isActived = false;

    public Text CoinText;

    public Text DiaText;

    public Text lvText;

    public Text countText;

    public Text LifeText;

    public Text timeText;
    public bool isTransitioning;


    
       private void Awake() 
    {
        
          // 이미 인스턴스가 존재하면 현재 인스턴스를 파괴하고 새로 생성하지 않습니다.
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this; // 현재 인스턴스를 설정합니다.
      //  DontDestroyOnLoad(gameObject); // 씬 전환 시에도 인스턴스가 유지되도록 설정합니다.

        SetUIState(0);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        pl = GameManager.instance.player;
        UpdateUI();
       
    }

    // Update is called once per frame
    void Update()
    {
        
         timeText.text = string.Format("Time : {0:N1}", GameManager.instance.stageTimer);
         UpdateUI();
    }

    public void SetallDisable()
    {     
       for(int i = 0; i < MainSceneUI.Length; i++)
       {
           MainSceneUI[i].SetActive(false);
       }

         for(int i = 0; i < BattleSceneUI.Length; i++)
         {
              BattleSceneUI[i].SetActive(false);
         }

        for(int i = 0; i < CharaterUI.Length; i++)
        {
                CharaterUI[i].SetActive(false);
        }

        VictoryPanel.SetActive(false);
        DefeatPanel.SetActive(false);
        StartButton.SetActive(false);
     
    }

    public void SetUIState(int stateindex)
    {
        SetallDisable();
        currentState = (UIState)stateindex;
   
        // UI state에 따른 동작 수행
        switch ((UIState)stateindex)
        {
            case UIState.MainScene:
                fadeScreen.Fade();
                for(int i = 0; i < MainSceneUI.Length; i++)
                {
                    if(MainSceneUI[i].activeSelf == false)
                    MainSceneUI[i].SetActive(true);
                }
                GameManager.instance.game_State = GameManager.Game_State.None;
                GameManager.instance.HideHero();
                break;
            case UIState.BattleScene:
                for(int i = 0; i < BattleSceneUI.Length; i++)
                {
                    if(BattleSceneUI[i].activeSelf == false)
                    BattleSceneUI[i].SetActive(true);
                }
                GameManager.instance.SetBatch();
                break;
            case UIState.CharacterScene:
                
                for(int i = 0; i < CharaterUI.Length; i++)
                {
                    if(CharaterUI[i].activeSelf == false)
                    CharaterUI[i].SetActive(true);
                }
                MainSceneUI[1].SetActive(true);
                 GameManager.instance.game_State = GameManager.Game_State.None;
                break;
            case UIState.HeroBatchScene:
                    fadeScreen.Fade();
                    MainSceneUI[1].SetActive(true);
                   CharaterUI[1].SetActive(true);
                   BattleSceneUI[4].SetActive(true);
                    BattleSceneUI[5].SetActive(true);
                    StartButton.SetActive(true);
                    GameManager.instance.game_State = GameManager.Game_State.Ready;
                    GameManager.instance.SetBatch();
                break;
            default:
                break;
        }
    }

    public void ShowCharacterPanel()
    {     

         Debug.Log(isActived.ToString() + "2");
        
    }


    public void UpdateUI()
    {
        CoinText.text = string.Format("{0:F0}", pl.gold);
        DiaText.text = string.Format("{0:F0}", pl.diamond);
        lvText.text = string.Format("Lv : {0:F0}", pl.level);

    }

    public void UpdateHPBar()
    {
       GameManager.instance.castle.HPbar.value = (float)GameManager.instance.castle.currentHealth/ (float)GameManager.instance.castle.maxhp;
       GameManager.instance.castle.HPbartext.text = string.Format("{0:F0}/{1:F0}",(float)GameManager.instance.castle.currentHealth, (float)GameManager.instance.castle.maxhp);
    }


    public void batchbuttonon()
    {
        batchButton.gameObject.SetActive(true);
        batchButton2.gameObject.SetActive(true);

    }

    public void batchbuttonoff()
    {
        batchButton.gameObject.SetActive(false);
        batchButton2.gameObject.SetActive(false);
            
    }

    public void showVictoryPanel()
    {
        VictoryPanel.SetActive(true);
        VictoryPanel.gameObject.GetComponent<ResultPanel>().resultText.text = "'Stage " + Player.instance.stage + "' 클리어 했습니다.";

       
    }

    public void showDefeatPanel()
    {
        DefeatPanel.SetActive(true);
        DefeatPanel.gameObject.GetComponent<ResultPanel>().resultText.text = "'Stage " + Player.instance.stage + "' 패배 했습니다.";
    }

      public void OnClickStageStart()
    {
        GameManager.instance.StageStart(GameManager.instance.player.world,GameManager.instance.player.stage);   
        StartButton.SetActive(false);
    }


}

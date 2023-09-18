using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public enum Game_State
    {
        None,
        Ready,
        Battle,
        Finish
    }
    public Game_State game_State;
    public static GameManager instance;
    public GameObject LevelManager;

    public GameObject StageManager;
    public UI_CharacterPanel UI_characterPanel;
    public GameObject[] heropoints;
    public bool[] batchedhero;
    public GameObject[] heroprefab;
    public bool selectedhero;
    public UI_characterSlot selectedSlot;

    public GameObject castleprefab;

    public InputField idInputField; // 유니티 인스펙터에서 텍스트 필드를 연결할 변수

    private string playerID; 
    
    public int  stageNumber;
    public int worldNumber;

  


   // [Header("Info")]
    public List<GameObject> aliveenemy;
    public int count;
    public PoolManager pool;
    public WaveManager waveM;
    public UI_Manager ui;
    public Player player;
    public Castle castle;
    public GameObject target;




    [SerializeField] private SpawnData[] data;

  //  public Transform[] target;



    public float time ;
     public float stageTimer ;
    public bool spawnstart;
    public bool initspawn = false;
    public bool readToBattle;
    private bool isFunctionRunning;
    public bool Automode;
    private bool isFunctionRunning2;


    // Start is called before the first frame update

    private void Awake() 
    {

          // 이미 인스턴스가 존재하면 현재 인스턴스를 파괴하고 새로 생성하지 않습니다.
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this; // 현재 인스턴스를 설정합니다.
//        DontDestroyOnLoad(gameObject); // 씬 전환 시에도 인스턴스가 유지되도록 설정합니다.
        
    }
    void Start()
    {
        player.init();
       //  countText.text = string.Format("{0:F0}", aliveenemy.Count);
      
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
           SceneManager.LoadScene("Lobby");
        }

        if(Input.GetKeyDown(KeyCode.O))
        Time.timeScale = Time.timeScale*2;

        if(Input.GetKeyDown(KeyCode.P))
        Time.timeScale = 1;

        if(Input.GetKeyDown(KeyCode.A))
        {
            if(Automode)
            {
                Automode = false;
            }
            else
            {
                Automode = true;
            }
            Debug.Log("Auto mode is " + Automode);
        }


      //  if(readToBattle)
     //  // HeroBatch();

if(game_State == GameManager.Game_State.Battle)
{
     if(spawnstart)
        {
           
            stageTimer += Time.deltaTime;
   
        }

        if(stageTimer >= 20f)
        {
            spawnstart = false;
              stageTimer = 0;
        }


        if(aliveenemy.Count == 0 && !spawnstart || Input.GetKeyDown(KeyCode.C))
        {
           CallFunctionWithDelay(StageClear);
        //  StageClear();
        }

        if(castle.currentHealth <= 0 || Input.GetKeyDown(KeyCode.F))
        {
            castle.currentHealth = 0;
            spawnstart = false;
             stageTimer = 0;
              CallFunctionWithDelay(StageFail);
         //  StageFail();
        
        }

}
       
       // if(GameManager.instance.aliveenemy.Count == 0 && !spawnstart)
        //  StageFInsih();
      
      
    }

    public void BatchHero()
    {
   
        int index = selectedSlot.slotindex;
        GameObject hero = player.transform.GetChild(index).gameObject;

        if(hero.GetComponent<Hero>().batchindex != -1)
        {
            heroreturn();


        }

        for (int i = 0; i < heropoints.Length; i++)
        {
            if(heropoints[i].GetComponent<TowerPoints>().isbuild == false)
            {
               
                hero.transform.localScale = new Vector3(-1.2f, 1.2f, 1.2f);
                hero.transform.position = heropoints[i].transform.position;
                hero.gameObject.SetActive(true);
                heropoints[i].GetComponent<TowerPoints>().isbuild = true;
                 heropoints[i].GetComponent<TowerPoints>().pointindex = index;
                hero.GetComponent<Hero>().batchindex = i;
                
                break;
               
            }
        }

        UI_characterPanel.DeselecteSlot();
    }

    public void heroreturn()
    {
        
        int index = selectedSlot.slotindex;
        GameObject hero = player.transform.GetChild(index).gameObject;

        if(hero.GetComponent<Hero>().batchindex == -1)
        {
            return;
        }

        heropoints[hero.GetComponent<Hero>().batchindex].GetComponent<TowerPoints>().isbuild = false;
        
        hero.GetComponent<Hero>().batchindex = -1;
        hero.SetActive(false);


    }




    public void HideHero()
    {
        
        for (int i = 0; i < player.transform.childCount; i++)
        {
         
            GameObject hero = player.transform.GetChild(i).gameObject;
            hero.gameObject.SetActive(false);
        }
   
    }

    public void SetBatch()
    {
        Debug.Log("SetBatch");
         for (int i = 0; i < player.transform.childCount; i++)
        {
            
            GameObject hero = player.transform.GetChild(i).gameObject;

            if(hero.GetComponent<Hero>().batchindex != -1)
            {
                Debug.Log("SetBatch2");
                  hero.gameObject.SetActive(true);
            }
          
        }
    }


    
    private void StageClear()
    {
        GameManager.instance.game_State = GameManager.Game_State.Ready;
       ui.showVictoryPanel();
       CalculateStage(player.world,player.stage);
       player.clearedmaxWorld = player.world;
       player.clearedmaxstage = player.stage;

       if(Automode)
       CallFunctionWithDelay2(TryButton);
       //보상
       //스테이지
    }


    private void StageFail()
    {
    GameManager.instance.game_State = GameManager.Game_State.Ready;
       ui.showDefeatPanel();
       //보상
       //스테이지
    }

    public void CheckButton()
    {
           if(ui.VictoryPanel.activeSelf != false || ui.DefeatPanel.activeSelf != false)
        {
            ui.VictoryPanel.SetActive(false);
            ui.DefeatPanel.SetActive(false);
        }
        RemoveAllEnemy();
          CallFunctionWithDelay(Setui3);
       // ui.SetUIState(3);
     
    }

    public void TryButton()
    {
        if(ui.VictoryPanel.activeSelf != false || ui.DefeatPanel.activeSelf != false)
        {
            ui.VictoryPanel.SetActive(false);
            ui.DefeatPanel.SetActive(false);
        }
        RemoveAllEnemy();
         CallFunctionWithDelay(Stagedelay);
      //  StageStart(player.stage);
    }

 public void MoveToNextStage()
    {
        stageNumber++;

        if (stageNumber > 10)
        {
            worldNumber++;
            stageNumber = 1;
        }
    }



    public void StageStart(int worldNumber,int stageNumber)
    {
        ui.SetUIState(1);
        game_State = GameManager.Game_State.Battle;
        spawnstart = true;
        //Player.instance.castle.currentHealth = Player.instance.castle.maxhp;
        UI_Manager.instance.UpdateHPBar();
        Debug.Log(worldNumber+"-" + stageNumber + "is start");

    }

    public void SetUI(int index)
    {
        UI_Manager.instance.SetUIState(index);
       // ui.FinishStageUI();
      //  HideHero();
    }

    private IEnumerator DelayedFunction(System.Action action)
    {
        yield return new WaitForSeconds(1f);
        action?.Invoke();
        isFunctionRunning = false;
    }

    private IEnumerator DelayedFunction2(System.Action action)
    {
        yield return new WaitForSeconds(2f);
        action?.Invoke();
        isFunctionRunning2 = false;
    }
    public void CallFunctionWithDelay(System.Action action)
    {
        if (!isFunctionRunning)
        {
            isFunctionRunning = true;
            StartCoroutine(DelayedFunction(action));
        }
    }

    public void CallFunctionWithDelay2(System.Action action)
    {
        if (!isFunctionRunning2)
        {
            isFunctionRunning2 = true;
            StartCoroutine(DelayedFunction2(action));
        }
    }

    public void RemoveAllEnemy()
    {
        for (int i = 0; i < aliveenemy.Count; i++)
        {
            aliveenemy[i].SetActive(false);
        }
    }

    public void Setui3()
    {
        ui.SetUIState(3);
    }
    public void Stagedelay()
    {
           StageStart(player.world,player.stage);
    }

    public int[] CalculateStage(int world, int stage)
{
    Player.instance.stage++;

    if (Player.instance.stage > 20)
    {
        Player.instance.world++;
        Player.instance.stage = 1;
    }

    int[] result = new int[] { Player.instance.stage, Player.instance.world };
    return result;
}
    
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    public GameObject LevelManager;
    public GameObject[] heropoints;

    public GameObject castleprefab;

    public InputField idInputField; // 유니티 인스펙터에서 텍스트 필드를 연결할 변수

    private string playerID; 
    
    private int spawnPointIndex = 0; 


   // [Header("Info")]
    public List<GameObject> aliveenemy;
    public int count;
    public PoolManager pool;
    public WaveManager waveM;
    public UI_Manager ui;
    public Player player;
    public GameObject target;



    [SerializeField] private SpawnData[] data;

  //  public Transform[] target;



    public float time ;
     public float timer ;
    public bool spawnstart;
    public bool initspawn = false;
    public float stageTime = 0;
    public float stageTimeMax = 5;


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
        DontDestroyOnLoad(gameObject); // 씬 전환 시에도 인스턴스가 유지되도록 설정합니다.
        
    }
    void Start()
    {
        player.init();
       //  countText.text = string.Format("{0:F0}", aliveenemy.Count);
      
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        Time.timeScale = Time.timeScale*2;

        if(Input.GetKeyDown(KeyCode.P))
        Time.timeScale = 1;



        if(spawnstart)
        {
            timer += Time.deltaTime;
            
            time += Time.deltaTime;

            if(time >= waveM.spawntimer)
            {
                waveM.Spawn();
                time = 0;
            }
        }

        if(timer >= 90f)
        {
            spawnstart = false;
              timer = 0;
        }

        if(GameManager.instance.aliveenemy.Count == 0 && !spawnstart)
          StageFInsih();
      
      
    }
    
    //function that hero batch to the heropoints when stagestart. check canuse value in heroof player's clihd object by array and if it is true. instantiate hero prefab to the heropoints.
    public void HeroBatch()
    {
        
        for (int i = 0; i < heropoints.Length; i++)
        {
         
            GameObject hero = player.transform.GetChild(i).gameObject;
            hero.transform.localScale = new Vector3(-1.2f, 1.2f, 1.2f); // Adjust the scale as needed
           // hero.transform.parent = heropoints[i].transform;
            hero.transform.position = heropoints[i].transform.position;
            hero.gameObject.SetActive(true);
        }
   
    }

    public void HideHero()
    {
        
        for (int i = 0; i < heropoints.Length; i++)
        {
         
            GameObject hero = player.transform.GetChild(i).gameObject;
            hero.gameObject.SetActive(false);
        }
   
    }

/*
   private void SpawnHero(Hero_type heroType)
{
    if (spawnPointIndex >= heropoints.Length)
    {
        Debug.LogWarning("No more spawn points available.");
        return;
    }

    // Instantiate the selected hero prefab from the GameManager's array
    GameObject heroPrefab = heroprefab[(int)heroType];
    GameObject hero = Instantiate(heroPrefab, heropoints[spawnPointIndex].transform.position, Quaternion.identity);

    hero.transform.localScale = new Vector3(-1.2f, 1.2f, 1.2f); // Adjust the scale as needed

    // Move to the next spawn point index (cycling back to the first if all have been used)
    spawnPointIndex = (spawnPointIndex + 1) % heropoints.Length;
}
*/

 

    public void StageStart()
    {
        spawnstart = true;
        HeroBatch();
        ui.StageStartUI();
        //Player.instance.castle.currentHealth = Player.instance.castle.maxhp;
        UI_Manager.instance.UpdateHPBar();
        Debug.Log("StageStart");

    }

    public void StageFInsih()
    {
        ui.FinishStageUI();
        HideHero();
    }
      

    /*
    private void BossSpawn()
    {
        GameObject boss = Instantiate(enemyprefab[5]);
        boss.transform.position = startPos.position;
      //  boss.GetComponent<Enemy>().target = target;
        boss.transform.parent = enemys;
        count ++;


        aliveenemy.Add(boss);
        countText.text = string.Format("{0:F0}", aliveenemy.Count);
    }

    public void DoSpawn()
    {
       
     
      
        GameObject temp = Instantiate(enemyprefab[Random.Range(0,data[level-1].MaxIndex+1)]);
        temp.transform.position = startPos.position;
     //   temp.GetComponent<Enemy>().target = target;
        temp.transform.parent = enemys;

        count++;
        aliveenemy.Add(temp);
                countText.text = string.Format("{0:F0}", aliveenemy.Count);
       // Debug.Log("Count ! til to max = " + (data[level-1].MaxEnemy - count));
       

    }    


    public void OnClickGround(Transform tr)
    {
        ground = tr;
        buttons.SetActive(true);

    }

    public void CreateTower(int index)
    {
        if(ground.childCount == 0 && coin >= towers[index].GetComponent<Tower>().cost)
        {
                coin = coin - towers[index].GetComponent<Tower>().cost;
                CoinText.text = string.Format("{0:F0}", coin);
              GameObject temp = Instantiate(towers[index]);
                temp.transform.parent = ground;
                temp.transform.localPosition = Vector3.zero;
                temp.GetComponent<Tower>().gm = this;

        }
     

        buttons.SetActive(false);
        
    }


    public void PauseGame()
    {
        Time.timeScale=1;
        isPause = false;
        Pausebutton.gameObject.SetActive(false);

    }

    public void GetCoin(int _coin)
    {
        coin += _coin;
        CoinText.text = string.Format("{0:F0}", coin);
    }

    */
    
}
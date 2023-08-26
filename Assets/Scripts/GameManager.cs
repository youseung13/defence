using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
public class GameManager : MonoBehaviour
{
    public static int[,] enemyData = 
    {
        {0,0,2,0,3,0,4,0},
        {0,0,0,0,1,1,1,1},
        {1,1,1,1,1,1,1,1}
    };
    //생성할 프리팹
    public GameObject[] enemy01;
    public GameObject[] towers;

    //생성될 위치
    public Transform startPos;

    public Transform[] target;

    public GameObject buttons;
    public Transform ground;

    public Transform enemys;


    public float time =0;
    public float timeMax = 2f;

    public int count = 0;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if(count <8)
        {
            time -= Time.deltaTime;
            if(time <0)
            {
                GameObject temp = Instantiate(enemy01[enemyData[1,count]]);
                temp.transform.position = startPos.position;
                temp.GetComponent<Enemy>().target = target;
                temp.transform.parent = enemys;

                time = timeMax;
                count++;
            }
        }

    }

    public void OnClickGround(Transform tr)
    {
        ground = tr;
        buttons.SetActive(true);

    }

    public void CreateTower(int index)
    {
        if(ground.childCount == 0)
        {
              GameObject temp = Instantiate(towers[index]);
                temp.transform.parent = ground;
                temp.transform.localPosition = Vector3.zero;
                temp.GetComponent<Tower>().gm = this;

        }
     

        buttons.SetActive(false);
        

    }
}
*/

public class GameManager : MonoBehaviour
{
    [Header("Info")]
    public List<GameObject> aliveenemy;
    public int count;
    public int coin;
    public int life;



    public static GameManager instance;

    public bool isPause;

    private int level ;
    private int Maxlevel;
    private int nowwave ;



    [SerializeField] private SpawnData[] data;

    //생성할 프리팹
    public GameObject[] enemyprefab;
    public GameObject[] towers;

    //생성될 위치
    public Transform startPos;

    public Transform[] target;

    public GameObject buttons;
    public GameObject Pausebutton;
    public Transform ground;

    public Transform enemys;


    public float time ;
    public float timeBetweenSpawn ;
    public bool initspawn = false;

    public float stageTime = 0;

    public float stageTimeMax = 5;


    public Text CoinText;
    public Text lvText;
    public Text countText;
    public Text LifeText;
    public Text timeText;
    // Start is called before the first frame update

    private void Awake() 
    {
       timeBetweenSpawn = 0.35f;
        
    }
    void Start()
    {
        stageTime = stageTimeMax;
        instance = this;
        level = 1;
        nowwave = 0;
        coin = 30;
        life = 5;

        CoinText.text = string.Format("{0:F0}", coin);

        lvText.text = string.Format("lv : {0:F0}", level);
        
        countText.text = string.Format("{0:F0}", aliveenemy.Count);

        LifeText.text = string.Format("{0:F0}", life);


        Debug.Log("data  1 = " + data[0].level + "/" + data[0].MaxEnemy + "/" + data[0].MaxIndex  +"/" + data[0].wave );
        Debug.Log("data  2 = " + data[1].level + "/" + data[1].MaxEnemy + "/" + data[1].MaxIndex  +"/" + data[1].wave );
        Debug.Log("data  = " + level + "/" + count  +"/" + nowwave + "/" + Random.Range(0,data[level-1].MaxIndex));


    }

    // Update is called once per frame
    void Update()
    {
        if(life <=0)
        {
            Debug.Log("game over!");
            Pausebutton.gameObject.SetActive(true);
            Time.timeScale=0;
        }



        time += Time.deltaTime;


      if(time >= timeBetweenSpawn && count < data[level-1].MaxEnemy && nowwave <data[level-1].wave && !isPause) 
            {
                time =0;

                if(level == 5)
                BossSpawn();
                else
                DoSpawn();
            }



        if(stageTime < 0 && nowwave < data[level-1].wave )//(count == data[level-1].MaxEnemy)
        {
            
            nowwave++;
            count = 0;
            stageTime=stageTimeMax;
            Debug.Log("Next Wave ! Wave is = " + nowwave+"/"+data[level-1].wave);
        }


        if(nowwave ==data[level-1].wave && aliveenemy.Count == 0)
        {
            //stageTime = stageTimeMax;
            isPause=true;
            level++;
            lvText.text = string.Format("lv : {0:F0}", level);
            nowwave = 0;
            Debug.Log("level up ! level is = " + level);
            Pausebutton.gameObject.SetActive(true);
            Time.timeScale=0;
        }

        if(stageTime >= 0)
        stageTime -= Time.deltaTime;


        timeText.text = string.Format("Time : {0:N2}", stageTime);


   
    }

    private void BossSpawn()
    {
        GameObject boss = Instantiate(enemyprefab[5]);
        boss.transform.position = startPos.position;
        boss.GetComponent<Enemy>().target = target;
        boss.transform.parent = enemys;
        count ++;


        aliveenemy.Add(boss);
        countText.text = string.Format("{0:F0}", aliveenemy.Count);
    }

    public void DoSpawn()
    {
       
     
      
        GameObject temp = Instantiate(enemyprefab[Random.Range(0,data[level-1].MaxIndex+1)]);
        temp.transform.position = startPos.position;
        temp.GetComponent<Enemy>().target = target;
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
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//handle these spawn function with object pooling
public class WaveManager : MonoBehaviour
{   
    public enum SpawnType
    {
        None, //그냥 가운데서 등장할 보스용
        Normal, // 주기마다 랜덤한 위치에서 한 마리씩
        Virtical, // n마리까지 세로로 살짝 거리두고 나타남
        Rect, // n마리까지 사각형 모양으로 나타남


    }

    private bool isInitialRandomSpawn = true;
    private bool isInitialRectSpawn = true;
    private bool isInitialVerticalSpawn = true;

    public SpawnData[] spawnDataArray;

    public Transform[] spawnPoint;
     public Vector2  startY;
    public Vector2  endY;

    public float timer1;
    public float timer2;
    public float timer3;

    public bool spawndone;

    // implement this with object pooling, from data i can get what to objectpool and how many. write a script for objectpooling
    private void Awake()
     {
          spawnPoint = GetComponentsInChildren<Transform>();    
          startY = new Vector2(0, spawnPoint[1].transform.position.y);
          endY = new Vector2(0, spawnPoint[2].transform.position.y);
          
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update() 
    {
        if(GameManager.instance.game_State != GameManager.Game_State.Battle && spawndone == true)
        {
            spawndone = false;
            ResetSpawnState();
        }

         if(GameManager.instance.spawnstart == true)
        {
            //Debug.Log("스폰 시작");
          
            SpawnData data = GetSpawnData(Player.instance.world,Player.instance.stage);

            if(data!=null)
            Spawn(data);

                    
            timer1 += Time.deltaTime;
            timer2 += Time.deltaTime;
            timer3 += Time.deltaTime;


                
        }
    }

    // Update is called once per frame

       public SpawnData GetSpawnData(int world, int stage)
    {
        foreach (SpawnData spawnData in spawnDataArray)
        {
            if (spawnData.world == world && spawnData.stage == stage)
            {
                return spawnData;
            }
        }
        return null; // 해당 월드와 스테이지에 대한 스폰 데이터를 찾을 수 없을 경우 null 반환
    }
    public void Spawn(SpawnData data)
    {
        spawndone = true;

        if (data == null)
        {
            Debug.LogError("스폰 데이터가 없습니다.");
            return;
        }

           // 초기 스폰 (딜레이 없이 1번 호출)
    if (isInitialRandomSpawn)
    {
        SpawnRandomly(data);
        isInitialRandomSpawn = false;
        return;
    }

    if (isInitialRectSpawn)
    {
        SpawnShapeRect(data);
        isInitialRectSpawn = false;
        return;
    }

    if (isInitialVerticalSpawn)
    {
        SpawnShapeVirtical(data);
        isInitialVerticalSpawn = false;
        return;
    }



        if(timer1 >= data.enemyspawnrate[0])
        {
            SpawnRandomly(data);
            timer1 = 0;
        }

        if(timer2 >= data.enemyspawnrate[1])
        {
            SpawnShapeRect(data);
            timer2 = 0;
        }

        if(timer3 >= data.enemyspawnrate[2])
        {
            SpawnShapeVirtical(data);
            timer3 = 0;
        }
     
    



       
      //  GameObject enemy = GameManager.instance.pool.Get2(Random.Range(0,3));
     //  enemy.transform.position = Vector2.zero;
       // enemy.transform.position = new Vector2 (spawnPoint[1].transform.position.x, Random.Range(endY.y,startY.y));
      //  GameManager.instance.count++;
    ////    GameManager.instance.aliveenemy.Add(enemy);
   //      GameManager.instance.ui.countText.text = string.Format("{0:F0}", GameManager.instance.aliveenemy.Count);

    }

        public void SpawnRandomly(SpawnData data)
    {
        Debug.Log("스폰 랜덤");

        StartCoroutine(SpawnRandomWithDelay(data.enemyprefab[0], data.enemycount[0]));
    }

    private IEnumerator SpawnRandomWithDelay(GameObject prefab, int count)
    {
        float spawnInterval = 1.2f; // 유닛 사이의 딜레이를 조절할 값

        for (int i = 0; i < count; i++)
        {
            GameObject enemy = Instantiate(prefab, transform);
            enemy.transform.position = Vector2.zero;
            enemy.transform.position = new Vector2(spawnPoint[1].transform.position.x, Random.Range(endY.y, startY.y));
            GameManager.instance.count++;
            GameManager.instance.aliveenemy.Add(enemy);
            GameManager.instance.ui.countText.text = string.Format("{0:F0}", GameManager.instance.aliveenemy.Count);

            yield return new WaitForSeconds(spawnInterval); // 유닛 사이의 딜레이 적용
        }
    }
    public void SpawnShapeRect(SpawnData data)
    {
        Debug.Log("스폰 사각형");
        int row = (int)Mathf.Sqrt(data.enemycount[1]);
        int col = data.enemycount[1] / row;
        float interval = 1f;
        float startX = -interval * (row - 1) / 2f;
        float startY = interval * (col - 1) / 2f;
        
        StartCoroutine(SpawnRectWithDelay(data.enemyprefab[1], row, col, interval));
    }

    private IEnumerator SpawnRectWithDelay(GameObject prefab, int row, int col, float interval)
    {
        float spawnInterval = 0.1f; // 유닛 사이의 딜레이를 조절할 값

        for (int i = 0; i < row * col; i++)
        {
            GameObject enemy = Instantiate(prefab, transform);
            enemy.transform.position = Vector2.zero;
            enemy.transform.position = new Vector2(spawnPoint[1].transform.position.x + interval * (i % row), spawnPoint[1].transform.position.y - interval * (i / row));
            GameManager.instance.count++;
            GameManager.instance.aliveenemy.Add(enemy);
            GameManager.instance.ui.countText.text = string.Format("{0:F0}", GameManager.instance.aliveenemy.Count);

            yield return new WaitForSeconds(spawnInterval); // 유닛 사이의 딜레이 적용
        }
    }

    public void SpawnShapeVirtical(SpawnData data)
    {
        Debug.Log("스폰 세로");
        float interval = 1f;
        float startY = interval * (data.enemycount[2] - 1) / 2f;

        StartCoroutine(SpawnVerticalWithDelay(data.enemyprefab[2], data.enemycount[2], interval, startY));
    }

    private IEnumerator SpawnVerticalWithDelay(GameObject prefab, int count, float interval, float startY)
    {
        float spawnInterval = 0.3f; // 유닛 사이의 딜레이를 조절할 값

        for (int i = 0; i < count; i++)
        {
            GameObject enemy = Instantiate(prefab, transform);
            enemy.transform.position = Vector2.zero;
            enemy.transform.position = new Vector2(spawnPoint[1].transform.position.x, startY - interval * i);
            GameManager.instance.count++;
            GameManager.instance.aliveenemy.Add(enemy);
            GameManager.instance.ui.countText.text = string.Format("{0:F0}", GameManager.instance.aliveenemy.Count);

            yield return new WaitForSeconds(spawnInterval); // 유닛 사이의 딜레이 적용
        }
    }

    public void ResetSpawnState()
    {
    // 스테이지가 끝날 때 초기화할 작업 수행
    timer1 = 0f;
    timer2 = 0f;
    timer3 = 0f;
    
    // 초기 딜레이 없는 리젠 리셋
        isInitialRandomSpawn = true;
        isInitialRectSpawn = true;
        isInitialVerticalSpawn = true;
        
        // WaveManager 초기화
    
    }

    
}

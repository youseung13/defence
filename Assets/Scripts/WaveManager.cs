using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{   
    public enum SpawnType
    {
        None, //그냥 가운데서 등장할 보스용
        Normal, // 주기마다 랜덤한 위치에서 한 마리씩
        Virtical, // n마리까지 세로로 살짝 거리두고 나타남
        Rect, // n마리까지 사각형 모양으로 나타남


    }

    public SpawnData[] spawnDataArray;


    public Transform[] spawnPoint;
     public SpawnData[] spawndata;
     public Vector2  startY;
    public Vector2  endY;

    public float timer1;
    public float timer2;
    public float timer3;

    public float[] time;

    public float spawntimer = 2f;

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

    private void Update() {
        
   
         if(GameManager.instance.spawnstart == true)
        {
            //Debug.Log("스폰 시작");
          
            SpawnData data = GetSpawnData(Player.instance.world,Player.instance.stage);
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

        if (data == null)
        {
            Debug.LogError("스폰 데이터가 없습니다.");
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

        for (int i = 0; i < data.enemycount[0]; i++)
        {
            GameObject enemy = Instantiate(data.enemyprefab[0], transform);
            enemy.transform.position = Vector2.zero;
            enemy.transform.position = new Vector2 (spawnPoint[1].transform.position.x, Random.Range(endY.y,startY.y));
            GameManager.instance.count++;
            GameManager.instance.aliveenemy.Add(enemy);
            GameManager.instance.ui.countText.text = string.Format("{0:F0}", GameManager.instance.aliveenemy.Count);
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
        for (int i = 0; i < data.enemycount[1]; i++)
        {
            GameObject enemy = Instantiate(data.enemyprefab[1], transform);
            enemy.transform.position = Vector2.zero;
            enemy.transform.position = new Vector2(spawnPoint[1].transform.position.x + interval * (i % row), spawnPoint[1].transform.position.y - interval * (i / row));
            GameManager.instance.count++;
            GameManager.instance.aliveenemy.Add(enemy);
            GameManager.instance.ui.countText.text = string.Format("{0:F0}", GameManager.instance.aliveenemy.Count);
        }

        
    }

    public void SpawnShapeVirtical(SpawnData data)//don start form endposition.y
    {
        Debug.Log("스폰 세로");

        float interval = 1f;
        float startY = interval * (data.enemycount[2] - 1) / 2f;
        for (int i = 0; i < data.enemycount[2]; i++)
        {
            GameObject enemy = Instantiate(data.enemyprefab[2], transform);
            enemy.transform.position = Vector2.zero;
            enemy.transform.position = new Vector2(spawnPoint[1].transform.position.x, startY - interval * i);
            GameManager.instance.count++;
            GameManager.instance.aliveenemy.Add(enemy);
            GameManager.instance.ui.countText.text = string.Format("{0:F0}", GameManager.instance.aliveenemy.Count);
        }
    }



    
}

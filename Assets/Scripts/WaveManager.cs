using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{   
    public Transform[] spawnPoint;
     public SpawnData[] spawndata;
     public Vector2  startY;
    public Vector2  endY;

    public float spawntimer = 1.5f;

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

    // Update is called once per frame
    void Update()
    {
      
         
    }

    public void Spawn()
    {
       
        GameObject enemy = GameManager.instance.pool.Get2(0);
        enemy.transform.position = Vector2.zero;
    //    enemy.transform.position = spawnPoint[Random.Range(1,spawnPoint.Length)].position;//생성후 위치 지정
        enemy.transform.position = new Vector2 (spawnPoint[1].transform.position.x, Random.Range(endY.y,startY.y));
    //    enemy.GetComponent<Enemy>().Init(spawndata[level]);
    //    enemy.GetComponent<Enemy>().home =  enemy.transform.position;
        
    }
}

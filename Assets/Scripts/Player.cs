using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public string id;
    public int level;
    public int exp;
    public int gold;
    public int diamond;

    public int castlelevel;
    public int castlehp;
    public List<Hero> herolist;
    public GameObject[] heroprefab;
    public bool[] herounlock;


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
        CheckHaveHero();
    }

    

    public void CheckHaveHero()
    {
        for (int i = 0; i < heroprefab.Length; i++)
        {
            if (herounlock[i])
            {
                heroprefab[i].GetComponent<Hero>().canuse = true;
            }
        

        }
    }

    public void init()
    {
        id = "test";
        level = 1;
        exp = 0;
        gold = 1000;
        diamond = 100;
    }
   
    // Start is called before the first frame update
    private void Start()
    {
         heroprefab = new GameObject[this.transform.childCount];
        for (int i = 0; i < this.transform.childCount; i++)
        {
            heroprefab[i] = this.transform.GetChild(i).gameObject;
        }

        // 모든 hero 오브젝트를 활성화하고 초기화
        foreach (GameObject hero in heroprefab)
        {
            hero.SetActive(true);
            hero.GetComponent<Hero>().Init();

            // 여기에서 초기화 작업을 수행합니다.
            // 예를 들어, 초기 위치, 상태, 속성 설정 등을 수행할 수 있습니다.
        }

        // 모든 hero 오브젝트를 다시 비활성화
        foreach (GameObject hero in heroprefab)
        {
            hero.SetActive(false);
        }
    }
}


    


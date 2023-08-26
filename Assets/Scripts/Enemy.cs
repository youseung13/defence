using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Slider HPbar;
    private int lap;

    public Transform[] target;

    private Vector3 dir;

    private int targetIndex;

    private Animator ani;

    public float speed ;
    public int hp;
    public int maxhp;
    public int givecoin;
    private bool isDead = false;
    public bool isAlive = true;

    private SpriteRenderer sprite;
     private CapsuleCollider2D cr;

     private Coroutine hpBarCoroutine;
    // Start is called before the first frame update
    private void Awake() {
              cr= GetComponent<CapsuleCollider2D>();
              ani = GetComponent<Animator>();
              sprite = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        if(HPbar !=null)
        {
        HPbar.value = (float)hp/ (float)maxhp;
        HPbar.gameObject.SetActive(false);
        }
       
        targetIndex = 1;
        maxhp = hp;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (GameManager.instance.isPause)
            return;
        if (isDead)
            return;

        Move();

        if(lap==3 && targetIndex==2)
        Gohome();

    }

    private void Gohome()
    {
          
    this.transform.position = Vector2.MoveTowards(this.transform.position, GameManager.instance.startPos.position, Time.deltaTime * speed * 5);
     if (Vector2.Distance(this.transform.position, GameManager.instance.startPos.position) < 0.05f)
    {
         
         GameManager.instance.life--;
         GameManager.instance.aliveenemy.Remove(gameObject);
        GameManager.instance.countText.text = string.Format("{0:F0}", GameManager.instance.aliveenemy.Count);

        GameManager.instance.LifeText.text = string.Format("{0:F0}", GameManager.instance.life);
         Destroy(gameObject);
    }

    }

    private void Move()
    {
        if (targetIndex < 6)
        {
            dir = target[targetIndex].position - this.transform.position;
            dir = dir.normalized * 0.03f;
            if (dir.x > 0)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }

            // speed = dir.magnitude;

            //  this.transform.position += dir;
            this.transform.position = Vector2.MoveTowards(this.transform.position, target[targetIndex].position, Time.deltaTime * speed);

            // Debug.LogFormat("Distance : {0}",Vector2.Distance(this.transform.position,target[targetIndex].position));


            if (Vector2.Distance(this.transform.position, target[targetIndex].position) < 0.02f)
            {
                targetIndex += 1;
                if (targetIndex == 6)
                {
                    lap ++;  
                    targetIndex = 1;
                }
            }
        }
        ani.SetFloat("Speed", speed);
    }

    public void TakeDamage(int _damage)
   {
        hp -= _damage;
         if(HPbar ==null)
         return;


         if (HPbar.gameObject.activeSelf)
        {
            if (hpBarCoroutine != null)
            {
                StopCoroutine(hpBarCoroutine);
            }
            hpBarCoroutine = StartCoroutine(ShowAndHideHPBar());
        }
        else // HP 바가 비활성화된 경우 바로 코루틴 시작
        {
            hpBarCoroutine = StartCoroutine(ShowAndHideHPBar());
        }

        UpdateHPBar();
       

   // GetComponent<Entity>().DamageImpact();
    //fx.StartCoroutine("FlashFX");

     if(hp <0  && !isDead)
     {
        isDead =true;
        ani.SetTrigger("Dead");
     }
   }

    private void UpdateHPBar()
    {
       HPbar.value = (float)hp/ (float)maxhp;
    }

    private void Die()
    {

       
        GameManager.instance.GetCoin(givecoin);
        Destroy(gameObject);
        GameManager.instance.aliveenemy.Remove(this.gameObject);
         GameManager.instance.countText.text = string.Format("{0:F0}", GameManager.instance.aliveenemy.Count);
    }

    IEnumerator ShowAndHideHPBar()
    {
        HPbar.gameObject.SetActive(true); // HP 바 활성화
        yield return new WaitForSeconds(0.8f); // 일정 시간 대기
        HPbar.gameObject.SetActive(false); // HP 바 비활성화
    }
}

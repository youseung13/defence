using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public Skill[] skills; // 스킬 목록
    public Hero hero;
 


    [Header("MultiShot")]
    private int arrowsFired = 0;
    private int arrowsReachedTarget = 0; // 목표 지점에 도달한 화살 수
    private bool isTargetingMode = false; // 타겟팅 모드 여부 
     private Coroutine currentFireArrowsCoroutine;
    private float arrowDelay = 0.15f;
   




   
   
    private void Awake() {
        hero = GetComponent<Hero>();
     }

    private void Start()
    {

    }
     private void Update() 
    {
        if (isTargetingMode)
        {
            // 타겟팅 모드에서 터치 입력을 사용하여 목표 지점 선택

            //can input touch with mobile
            //if (Input.GetMouseButtonDown(0))
             if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                hero.skillshot = true;
                // 마우스 위치를 화면 좌표에서 월드 좌표로 변환
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = -Camera.main.transform.position.z;
                skills[0].targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                // 스킬 발동
                //UseMultiShot(skills[0].targetPosition);
               
            }
        }
        else
        {
                // 버튼을 눌러 타겟팅 모드로 전환
               // if (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.Joystick1Button2)
               if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    isTargetingMode = true;
                     hero.stateMachine.ChangeState(hero.readyState);
                    Debug.Log("Targeting Mode On");
                }
        }
    }
    

    public void UseMultiShot(Vector3 targetPosition)
    {
        Debug.Log("ActivateSkill");
        // 타겟팅 모드에서만 스킬 발동
        if (isTargetingMode)
        {
            // 화살 발사 코루틴 시작
           currentFireArrowsCoroutine = StartCoroutine(MultiArrows(targetPosition));
            // 타겟팅 모드 종료
            isTargetingMode = false;
            Debug.Log("Skill Activated");
        }
    }

    private IEnumerator MultiArrows(Vector3 targetPosition)
    {
        arrowsFired = 0;
        arrowsReachedTarget = 0;
        
        while (arrowsFired < skills[0].numArrows)
        {
            // 다중화살을 생성하고 발사합니다.
            GameObject multiArrow = Instantiate(skills[0].projectilePrefab, hero.transform.position, Quaternion.identity);
            MultiArrow arrowScript = multiArrow.GetComponent<MultiArrow>();
            arrowScript.skillController = this;
            arrowScript.target = targetPosition;
        
            // 화살 방향 설정
          //  Vector3 direction = targetPosition - firePoint.position;
          //  arrowScript.transform.forward = direction.normalized;

            //화살 목표물로 이동
            

            arrowsFired++;
            Debug.Log("FireArrows");
            yield return new WaitForSeconds(arrowDelay);
        }
    }

    public void MultiShotExplode()
    {
        arrowsReachedTarget++;
        Debug.Log("StartExplosion");
        Instantiate(skills[0].addiotnalEffectPrefab, skills[0].targetPosition, Quaternion.identity);

        if (arrowsReachedTarget >= skills[0].numArrows)
        {
            // 모든 화살이 목표 지점에 도달한 경우 폭발 이펙트 시작
            currentFireArrowsCoroutine = null;
        }

  
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public Skill[] skills; // 스킬 목록
    public Hero hero;

    public int skillIndex = -1; // 현재 스킬 인덱스
 


    [Header("MultiShot")]
    private int arrowsFired = 0;
    private int arrowsReachedTarget = 0; // 목표 지점에 도달한 화살 수
    private bool isTargetingMode = false; // 타겟팅 모드 여부 
    private bool skillPressed;
    private Coroutine currentFireArrowsCoroutine;
    private float arrowDelay = 0.15f;
    private Vector3 inputStartPosition;
    public GameObject targetArea;

    private void Awake() {
        hero = GetComponent<Hero>();
     }

    private void Start()
    {

    }
     
    
    private void Update()
    {
        if (GameManager.instance.game_State == GameManager.Game_State.Battle)
        {
            if (skillPressed)
            {
                // 터치 입력 또는 마우스 클릭을 사용하여 목표 지점 선택
                if ((Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && !isTargetingMode)
                {
                    isTargetingMode = true;
                     hero.stateMachine.ChangeState(hero.readyState);
                    Debug.Log("Targeting Mode On");
                    inputStartPosition = Input.mousePosition;
                    targetArea.SetActive(true);
                    UpdateTargetAreaPosition(inputStartPosition);
                }
                else if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
                {
                    //Debug.Log("Targeting Mode On");
                    // 입력 위치가 이동할 때마다 TargetArea 위치 업데이트
                    UpdateTargetAreaPosition(Input.mousePosition);
                }
                else if ((Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) && isTargetingMode)
                {
                       hero.skillshot = true;
                    Debug.Log("Targeting Mode Off");
                    // 입력이 끝났을 때, 스킬 발동 로직 추가
                    targetArea.SetActive(false);       
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = -Camera.main.transform.position.z;
                skills[skillIndex].targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                 //   UseMultiShot(worldPosition); // 스킬 발동 함수 호출
                 
                    isTargetingMode = false;
                    skillPressed = false;
                    Debug.Log("skillpressed false");
                }
            }
        }
    }




   private void UpdateTargetAreaPosition(Vector3 screenPosition)
    {
        // 화면 좌표를 월드 좌표로 변환하고 TargetArea 위치 설정
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;
        targetArea.transform.position = worldPosition;
    }

    public void StartTargetingMode(int index)
    {
        skillIndex = index;
        skillPressed = true;
        // 버튼을 눌러 타겟팅 모드로 전환
        hero.stateMachine.ChangeState(hero.readyState);
        Debug.Log("Targeting Mode On");
    }

    public void UseMultiShot(Vector3 targetPosition)
    {
        Debug.Log("ActivateSkill");
        // 타겟팅 모드에서만 스킬 발동

            // 화살 발사 코루틴 시작
           currentFireArrowsCoroutine = StartCoroutine(MultiArrows(targetPosition));
            // 타겟팅 모드 종료
            Debug.Log("Skill Activated");

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public GameObject multiArrowPrefab; // 다중화살 프리팹
    public Transform firePoint; // 화살 발사 위치
    public Transform explodepoint; // 화살 발사 위치
    public GameObject explosionEffectPrefab; // 폭발 이펙트 프리팹
    public float arrowDelay = 0.5f; // 화살 간 간격
    public int numArrows = 3; // 발사할 화살 개수
    private int arrowsFired = 0;
    private int arrowsReachedTarget = 0; // 목표 지점에 도달한 화살 수
    private bool isTargetingMode = false; // 타겟팅 모드 여부
    private Vector3 targetPosition;

     private Coroutine currentFireArrowsCoroutine;

    private void Update() 
    {
        if (isTargetingMode)
        {
            // 타겟팅 모드에서 터치 입력을 사용하여 목표 지점 선택

            //can input touch with mobile
            //if (Input.GetMouseButtonDown(0))
             if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // 마우스 위치를 화면 좌표에서 월드 좌표로 변환
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = -Camera.main.transform.position.z;
                targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                // 스킬 발동
                ActivateSkill(targetPosition);
            }
        }
        else
        {
                // 버튼을 눌러 타겟팅 모드로 전환
               // if (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.Joystick1Button2)
               if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    isTargetingMode = true;
                    Debug.Log("Targeting Mode On");
                }
        }
    }
    

    public void ActivateSkill(Vector3 targetPosition)
    {
        Debug.Log("ActivateSkill");
        // 타겟팅 모드에서만 스킬 발동
        if (isTargetingMode)
        {
            // 화살 발사 코루틴 시작
           currentFireArrowsCoroutine = StartCoroutine(FireArrows(targetPosition));
            // 타겟팅 모드 종료
            isTargetingMode = false;
            Debug.Log("Skill Activated");
        }
    }

    private IEnumerator FireArrows(Vector3 targetPosition)
    {
        arrowsFired = 0;
        arrowsReachedTarget = 0;
        
        while (arrowsFired < numArrows)
        {
            // 다중화살을 생성하고 발사합니다.
            GameObject multiArrow = Instantiate(multiArrowPrefab, firePoint.position, Quaternion.identity);
            MultiArrow arrowScript = multiArrow.GetComponent<MultiArrow>();
            arrowScript.target = targetPosition;
            arrowScript.skillController = this;
            // 화살 방향 설정
          //  Vector3 direction = targetPosition - firePoint.position;
          //  arrowScript.transform.forward = direction.normalized;

            //화살 목표물로 이동
            

            arrowsFired++;
            Debug.Log("FireArrows");
            yield return new WaitForSeconds(arrowDelay);
        }
    }

        public void ArrowReachedTarget()
    {
        arrowsReachedTarget++;

        if (arrowsReachedTarget >= numArrows)
        {
            // 모든 화살이 목표 지점에 도달한 경우 폭발 이펙트 시작
            StartExplosion();
            currentFireArrowsCoroutine = null;
        }
    }

    public void StartExplosion()
    {
        arrowsReachedTarget++;
        Debug.Log("StartExplosion");
        Instantiate(explosionEffectPrefab, targetPosition, Quaternion.identity);

        if (arrowsReachedTarget >= numArrows)
        {
            // 모든 화살이 목표 지점에 도달한 경우 폭발 이펙트 시작
            currentFireArrowsCoroutine = null;
        }

  
    }

}
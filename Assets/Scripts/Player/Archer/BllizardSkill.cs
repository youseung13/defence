using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BllizardSkill : MonoBehaviour
{
    public GameObject bllizardobejct;
    public ParticleSystem blizzardParticle;

    public GameObject target;
    public int damage =10;

    private void Awake() {
        blizzardParticle = bllizardobejct.GetComponentInChildren<ParticleSystem>();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.L))
        {
            UseBllizardSkill(target.transform.position);
        }

        
    }


    public void UseBllizardSkill(Vector3 targetPosition)
    {
        GameObject blizzard = Instantiate(bllizardobejct, targetPosition, Quaternion.identity);

        blizzardParticle.Play();
        Destroy(blizzard, blizzardParticle.main.duration); // 파티클 재생 시간 후에 파티클과 함께 파티클 오브젝트도 삭제
    }

     public void OnParticleSystemStopped()
    {
        // 파티클 시스템이 재생을 마치면 오브젝트 삭제
      //  Destroy(gameObject);
    }





    
}

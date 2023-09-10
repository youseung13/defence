using System.Collections;
using UnityEngine;

public class MultiArrow : MonoBehaviour
{
    public float speed;
    public Vector3 target;
    public SkillController skillController;

/*
    public void InitiateArrow(SkillController controller, GameObject explosionEffect, float delay)
    {
        skillController = controller;
        explosionEffectPrefab = explosionEffect;
        explosionDelay = delay;
        StartCoroutine(MoveArrow());
    }
*/
    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(transform.position == target)
        {
            skillController.StartExplosion();
            Destroy(gameObject);
        }
    }
   
}
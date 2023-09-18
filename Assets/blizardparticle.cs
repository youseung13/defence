using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blizardparticle : MonoBehaviour
{
    public ParticleSystem blizzardParticle;
    private float time;
    public float damagetick;

    public int damage =2;

    private void Awake() {
        blizzardParticle = GetComponent<ParticleSystem>();
    }

    private void Update() {
        time += Time.deltaTime;
        
    }

    private void OnParticleCollision(GameObject other) {
        if(other.GetComponent<EnemyStats>() != null)
        {
            EnemyStats targetstat = other.GetComponent<EnemyStats>();
             if(time > damagetick)
            {
                targetstat.TakeDamage(damage);
                time = 0;
            }
            
        }
    }

    

    
}

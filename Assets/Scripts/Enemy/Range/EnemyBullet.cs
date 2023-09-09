using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 25f; // Bullet speed
    public EnemyStats enemystat; // Damage amount when it hits the player

    public GameObject target; // Reference to the player's transform

    private void Start()
    {
      

        if (target == null)
        {
            Debug.LogWarning("Target not found in GameManager. Make sure you set the target in GameManager.");
            Destroy(gameObject); // Destroy the bullet if the target is not found.
        }
    }

    private void Update()
    {
        if (target != null)
        {
            // Calculate the direction towards the target (player)
            Vector3 direction = (target.transform.position - transform.position).normalized;

            // Move the bullet in the calculated direction
            transform.Translate(direction * speed * Time.deltaTime);

            // Destroy the bullet when it goes out of bounds or after some time (prevent memory leaks)
            Destroy(gameObject, 8f);
        }
    }



      private void OnTriggerEnter2D(Collider2D hit) 
    {
      if(hit.GetComponent<Castle>() != null)
      {
            Castle castlestat = hit.GetComponent<Castle>();


            enemystat.DoDamage(castlestat);
           
            //hit.GetComponent<Enemy>().Die();
            Destroy(gameObject);
      }
    }
}
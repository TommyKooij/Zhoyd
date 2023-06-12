using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int totalHealth = 3;
    public GameObject deathEffect;
    private Transform player;
    public float bounce;

    public void Start()
    {
        player = PlayerHealthController.instance.transform;
    }

    public void DamageEnemy(int damageAmount)
    {
        totalHealth -= damageAmount;

        if (totalHealth <= 0)
        {
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }

        if (player.position.x < transform.position.x)
        {
            transform.position = new Vector2(transform.position.x + bounce, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - bounce, transform.position.y);
        }
    }
}

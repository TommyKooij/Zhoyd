using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    #region VARIABLES
    public int totalHealth = 3;
    public GameObject deathEffect;
    private Transform player;
    public float bounce;

    public float invincibilityLength;
    private float invinceCounter;
    public float flashLength;
    private float flashCounter;
    private bool isDamaged = false;

    public SpriteRenderer[] enemySprites;
    #endregion

    public void Start()
    {
        player = PlayerHealthController.instance.transform;
    }

    public void Update()
    {
        #region INVINCIBLE LENGTH
        if (invinceCounter > 0)
        {
            isDamaged = true;
            invinceCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                foreach (SpriteRenderer sr in enemySprites)
                {
                    sr.enabled = !sr.enabled;
                }

                flashCounter = flashLength;
            }

            if (invinceCounter <= 0)
            {
                foreach (SpriteRenderer sr in enemySprites)
                {
                    sr.enabled = true;
                }
                flashCounter = 0f;
            }
        }
        else
        {
            isDamaged = false;
        }
        #endregion
    }

    public void DamageEnemy(int damageAmount)
    {
        if (isDamaged == false)
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
            else
            {
                invinceCounter = invincibilityLength;
            }
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

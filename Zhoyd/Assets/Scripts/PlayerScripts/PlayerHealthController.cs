using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    //[HideInInspector]
    #region VARIABLES
    public int currentHealth;
    public int maxHealth;

    public float invincibilityLength;
    private float invinceCounter;
    public float flashLength;
    private float flashCounter;

    public SpriteRenderer[] playerSprites;
    private PlayerController player;
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        #region INVINCIBLE LENGTH
        if (invinceCounter > 0)
        {
            invinceCounter -= Time.deltaTime;
            Physics2D.IgnoreLayerCollision(9, 14, true);

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                foreach(SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = !sr.enabled;
                }

                flashCounter = flashLength;
            }

            if (invinceCounter <= 0)
            {
                foreach (SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = true;
                }
                flashCounter = 0f;
            }
        }
        else
        {
            Physics2D.IgnoreLayerCollision(9, 14, false);
        }
        #endregion
    }

    public void DamagePlayer(int damageAmount)
    {
        if (invinceCounter <= 0)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                RespawnController.instance.Respawn();
            }
            else
            {
                invinceCounter = invincibilityLength;
            }

            UIController.instance.UpdateHealth(currentHealth, maxHealth);
        }
    }

    public void FillHealthbar()
    {
        currentHealth = maxHealth;

        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }
}

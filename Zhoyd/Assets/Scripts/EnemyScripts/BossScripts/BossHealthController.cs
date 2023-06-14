using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{

    public static BossHealthController instance;

    #region VARIABLES
    public Slider bossHealthSlider;
    public int currentHealth = 30;
    public BossBattle theBoss;

    public float invincibilityLength;
    private float invinceCounter;
    public float flashLength;
    private float flashCounter;
    private bool isDamaged = false;

    public SpriteRenderer[] bossSprites;
    #endregion

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        bossHealthSlider.maxValue = currentHealth;
        bossHealthSlider.value = currentHealth;
    }

    void Update()
    {
        #region INVINCIBLE LENGTH
        if (invinceCounter > 0)
        {
            isDamaged = true;
            invinceCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                foreach (SpriteRenderer sr in bossSprites)
                {
                    sr.enabled = !sr.enabled;
                }

                flashCounter = flashLength;
            }

            if (invinceCounter <= 0)
            {
                foreach (SpriteRenderer sr in bossSprites)
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

    public void TakeDamage(int damageAmount)
    {
        if (isDamaged == false)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                theBoss.EndBattle();
            }
            else
            {
                invinceCounter = invincibilityLength;
            }

            bossHealthSlider.value = currentHealth;
        }
    }
}

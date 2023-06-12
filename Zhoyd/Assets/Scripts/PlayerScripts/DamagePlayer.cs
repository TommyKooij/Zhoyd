using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    #region VARIABLES
    private PlayerController player;
    public int damageAmount;

    public bool destroyOnDamage;
    public GameObject destroyEffect;
    private float knockback = 15f;
    #endregion

    private void OnCollisionEnter2D(Collision2D other)
    {
        player = FindObjectOfType<PlayerController>();
        if (other.gameObject.tag == "Player")
        {
            DealDamage();
        }
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            DealDamage();
        }
    }*/

    void DealDamage()
    {
        if (this.transform.position.x < PlayerHealthController.instance.transform.position.x)
        {
            player.theRB.velocity = new Vector2(knockback, knockback);
        }
        else
        {
            player.theRB.velocity = new Vector2(-knockback, knockback);
        }

        PlayerHealthController.instance.DamagePlayer(damageAmount);

        if (destroyOnDamage)
        {
            if (destroyEffect != null)
            {
                Instantiate(destroyEffect, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}

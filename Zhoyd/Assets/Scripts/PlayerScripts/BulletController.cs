using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    #region VARIABLES
    private Transform player;

    [Header("Bullet")]
    public float bulletSpeed;
    public Rigidbody2D theRB;
    public Vector2 moveDir;

    public GameObject hitMarker;

    public int damageAmount;
    #endregion

    void Start()
    {
        #region DIRECTION
        player = PlayerHealthController.instance.transform;

        //handle direction change
        if (player.position.x < transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region MOVEMENT
        theRB.velocity = moveDir * bulletSpeed;
        #endregion
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthController>().DamageEnemy(damageAmount);
        }

        if (other.tag == "Boss")
        {
            BossHealthController.instance.TakeDamage(damageAmount);
        }

        if (hitMarker != null)
        {
            Instantiate(hitMarker, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

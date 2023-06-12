using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    #region VARIABLES
    private Transform player;

    [Header("Bullet")]
    public float bulletSpeed;
    public Rigidbody2D theRB;

    public GameObject hitMarker;

    public int damageAmount;
    #endregion

    void Start()
    {
        #region DIRECTION
        Vector3 direction = transform.position - PlayerHealthController.instance.transform.position;

        if (PlayerHealthController.instance.transform.position.x < theRB.position.x)
        {
            theRB.velocity = -transform.right * bulletSpeed;
        }
        else
        {
            theRB.velocity = transform.right * bulletSpeed;
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region MOVEMENT
        //theRB.velocity = -transform.right * bulletSpeed;
        #endregion
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer(damageAmount);
        }

        if (hitMarker != null)
        {
            Instantiate(hitMarker, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
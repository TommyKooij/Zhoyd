using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    #region VARIABLES
    public float rocketSpeed;
    public Rigidbody2D theRB;
    public Vector2 moveDir;

    public GameObject hitMarker;
    public int damageAmount;

    public float blastRange;
    public LayerMask whatIsDestructible;
    #endregion

    // Update is called once per frame
    void Update()
    {
        #region MOVEMENT
        theRB.velocity = moveDir * rocketSpeed;

        //handle direction change
        if (theRB.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (theRB.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        #endregion

        //destroy object on hit
        Collider2D[] objectsToRemove = Physics2D.OverlapCircleAll(transform.position, blastRange, whatIsDestructible);

        if (objectsToRemove.Length > 0)
        {
            foreach (Collider2D col in objectsToRemove)
            {
                Destroy(col.gameObject);
            }
        }
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

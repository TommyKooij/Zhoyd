using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingController : MonoBehaviour
{
    #region VARIABLES
    public Rigidbody2D theRB;
    public float rangeToStartChase;
    private bool isChasing;

    public float moveSpeed;

    private Transform player;

    public Animator anim;

    private EnemyHealthController health;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthController.instance.transform;

        health = GetComponent<EnemyHealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = new Vector2(theRB.velocity.x * moveSpeed, theRB.velocity.y);

        if (!isChasing)
        {
            if (health.maxHealth > health.currentHealth)
            {
                isChasing = true;
            }
            else
            {
                if (Vector3.Distance(transform.position, player.position) < rangeToStartChase)
                {
                    isChasing = true;
                }
                else
                {
                    isChasing = false;
                }
            }
        } 
        else
        {
            //handle direction change
            if (player.position.x < transform.position.x)
            {
                transform.localScale = Vector3.one;
            }
            else if (player.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            if (player.gameObject.activeSelf)
            {
                Vector3 direction = transform.position - player.position;

                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentientCrystalController : MonoBehaviour
{
    #region VARIABLES
    public Transform[] patrolPoints;
    private int currentPatrolPoint;

    public float moveSpeed, waitAtPoint;
    private float waitCounter;

    public Rigidbody2D theRB;
    private Transform player;

    public Transform shotPoint;
    public GameObject shotToFire;
    public float shotTimer;
    private float shotCounter;
    public bool isAttacking;

    public float rangeToShoot;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthController.instance.transform;

        waitCounter = waitAtPoint;

        foreach (Transform pPoint in patrolPoints)
        {
            pPoint.SetParent(null);
        }

        shotCounter = shotTimer;
    }

    // Update is called once per frame
    void Update()
    {
        #region PATROL
        if (Mathf.Abs(transform.position.x - patrolPoints[currentPatrolPoint].position.x) > 0.2f)
        {
            if (transform.position.x < patrolPoints[currentPatrolPoint].position.x)
            {
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
                transform.localScale = Vector3.one;
            }
        }
        else
        {
            theRB.velocity = new Vector2(0f, theRB.velocity.y);
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0)
            {
                waitCounter = waitAtPoint;
                currentPatrolPoint++;

                if (currentPatrolPoint >= patrolPoints.Length)
                {
                    currentPatrolPoint = 0;
                }
            }
        }
        #endregion

        #region ATTACK
        if (isAttacking)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = shotTimer;
                Instantiate(shotToFire, shotPoint.position, Quaternion.identity);
            }
        }
        #endregion
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }
}

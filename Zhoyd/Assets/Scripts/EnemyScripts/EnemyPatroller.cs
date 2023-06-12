using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    #region VARIABLES
    public Transform[] patrolPoints;
    private int currentPatrolPoint;

    public float moveSpeed, waitAtPoint;
    private float waitCounter;

    public Rigidbody2D theRB;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitAtPoint;

        foreach(Transform pPoint in patrolPoints)
        {
            pPoint.SetParent(null);
        }
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
    }
}

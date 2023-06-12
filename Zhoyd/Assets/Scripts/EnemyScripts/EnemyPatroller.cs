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

    public bool isHorizontal;
    public bool isVertical;
    public bool isFacingRight;
    public bool isFacingLeft;
    public bool isFacingUp;
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
            #region HORIZONTAL
            if (isHorizontal == true)
            {
                if (Mathf.Abs(transform.position.x - patrolPoints[currentPatrolPoint].position.x) > 0.2f)
                {
                    if (transform.position.x < patrolPoints[currentPatrolPoint].position.x)
                    {
                        theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
                        if (isFacingUp == true)
                        {
                            transform.localScale = new Vector3(1f, -1f, 1f);
                        }
                        else
                        {
                            transform.localScale = new Vector3(-1f, 1f, 1f);
                        }
                    }
                    else
                    {
                        theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
                        if (isFacingUp)
                        {
                            transform.localScale = new Vector3(-1f, -1f, 1f);
                        }
                        else
                        {
                            transform.localScale = Vector3.one;
                        }
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
            }
            #endregion

            #region VERTICAL
            if (isVertical)
            {
                if (Mathf.Abs(transform.position.y - patrolPoints[currentPatrolPoint].position.y) > 0.2f)
                {
                    if (transform.position.y < patrolPoints[currentPatrolPoint].position.y)
                    {
                        theRB.velocity = new Vector2(theRB.velocity.x, moveSpeed);
                        if (isFacingLeft == true)
                        {
                            transform.localScale = new Vector3(1f, -1f, 1f);
                        }
                        else if (isFacingRight == true)
                        {
                        transform.localScale = new Vector3(-1f, 1f, 1f);
                        }
                    }
                    else
                    {
                        theRB.velocity = new Vector2(theRB.velocity.x, -moveSpeed);
                        if (isFacingLeft == true)
                        {
                            transform.localScale = new Vector3(-1f, -1f, 1f);
                        }
                        else if (isFacingRight == true)
                        {
                        transform.localScale = Vector3.one;
                    }
                    }
                }
                else
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, 0f);
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
            }
            #endregion
        #endregion
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour
{
    public Transform[] waitPoints;
    private int currentWaitPoint;

    public float moveSpeed, waitAtPoint;
    private float waitCounter;

    public Rigidbody2D theRB;

    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitAtPoint;

        foreach (Transform wPoint in waitPoints)
        {
            wPoint.SetParent(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.y - waitPoints[currentWaitPoint].position.y) > 0.2f)
        {
            if (transform.position.y < waitPoints[currentWaitPoint].position.y)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, moveSpeed);
            }
            else
            {
                theRB.velocity = new Vector2(theRB.velocity.x, -moveSpeed);
            }
        }
        else
        {
            theRB.velocity = new Vector2(theRB.velocity.x, 0f);
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0)
            {
                waitCounter = waitAtPoint;
                currentWaitPoint++;

                if (currentWaitPoint >= waitPoints.Length)
                {
                    currentWaitPoint = 0;
                }
            }
        }
    }
}

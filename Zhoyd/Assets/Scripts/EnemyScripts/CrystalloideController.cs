using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalloideController : MonoBehaviour
{
    public Rigidbody2D theRB;

    public Transform[] patrolPoints;
    private int currentPatrolPoint;

    public float moveSpeed, turnSpeed, floatSpeed;
    public float waitAtPoint;
    private float waitCounter;

    private Transform player;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthController.instance.transform;

        foreach (Transform pPoint in patrolPoints)
        {
            pPoint.SetParent(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 direction = transform.position - player.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);*/
        if (Mathf.Abs(transform.position.x - patrolPoints[currentPatrolPoint].position.x) > 0.1f)
        {
            if (transform.position.y > patrolPoints[currentPatrolPoint].position.y && transform.position.x > patrolPoints[currentPatrolPoint].position.x)
            {
                theRB.velocity = new Vector2(-moveSpeed, -floatSpeed);
                transform.localScale = Vector3.one;
            }
        }
        else
        {
            //theRB.velocity = new Vector2(0f, theRB.velocity.y);
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
}

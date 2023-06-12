using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdamantineThresherController : MonoBehaviour
{
    #region VARIABLES
    [Header("Body")]
    public float moveSpeed;
    public Animator anim;
    public Rigidbody2D theRB;

    [Header("Jump")]
    public Transform[] jumpPoints;
    private int currentJumpPoint;
    public float jumpForce;
    private bool hasJumped;
    public float jumpCounter, timeBetweenJumps;
    public float waitBeforeJump;
    private float landedJumpCounter, waitBeforeJumpCounter;

    [Header("Ground")]
    public Transform groundPoint;
    public LayerMask isGround;
    public bool isOnGround;

    [Header("Attacks")]
    public GameObject bullet;
    public float shotCounter, timeBetweenShots;
    public Transform shotPoint;
    public float distanceAttacks;
    private bool isTongueAttacking, isShooting = false;

    private PlayerController thePlayer;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();

        waitBeforeJumpCounter = waitBeforeJump;

        foreach (Transform jPoint in jumpPoints)
        {
            jPoint.SetParent(null);
        }

        shotCounter = timeBetweenShots;

        thePlayer = PlayerHealthController.instance.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        isOnGround = Physics2D.OverlapCircle(groundPoint.position, .1f, isGround);

        //Jump if not at other point
        #region JUMP
        if (Mathf.Abs(transform.position.x - jumpPoints[currentJumpPoint].position.x) > .2f)
        {
            if (transform.position.x < jumpPoints[currentJumpPoint].position.x)
            {
                anim.SetTrigger("isJumping");
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
                hasJumped = false;
                isTongueAttacking = false;
                isShooting = false;
            }
            else
            {
                anim.SetTrigger("isJumping");
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
                transform.localScale = Vector3.one;
                hasJumped = false;
                isTongueAttacking = false;
                isShooting = false;
            }

            if (transform.position.y < jumpPoints[currentJumpPoint].position.y - 1.5f && theRB.velocity.y < .5f)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }
        }
        else
        {
            theRB.velocity = new Vector2(0f, theRB.velocity.y);

            //Change sprite dependant on jump point
            if (currentJumpPoint == 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            } else
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            //Wait before jump
            waitBeforeJumpCounter -= Time.deltaTime;
            if (waitBeforeJumpCounter <= 0)
            {
                //waitBeforeJumpCounter = waitBeforeJump;
                currentJumpPoint++;

                if (currentJumpPoint == 2 || currentJumpPoint == 4)
                {
                    waitBeforeJumpCounter = waitBeforeJump;
                }

                if (currentJumpPoint >= jumpPoints.Length)
                {
                    currentJumpPoint = 0;
                }
            }
        }

        if (hasJumped)
        {

        }
        else
        {
            if (jumpCounter <= 0)
            {
                anim.SetTrigger("isJumping");
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                hasJumped = true;
            }
        }
        #endregion

        #region ATTACKS
        //Checks if boss touches ground
        if (isOnGround)
        {
            if (Vector3.Distance(transform.position, thePlayer.transform.position) < distanceAttacks)
            {
                isTongueAttacking = true;
                isShooting = false;
                anim.SetTrigger("isTongueAttacking");
                shotCounter = timeBetweenShots;
            }
            else
            {
                isShooting = true;
                isTongueAttacking = false;
                shotCounter -= Time.deltaTime;
                jumpCounter = timeBetweenJumps;
            }
        } 
        else
        {
            shotCounter = timeBetweenShots;
            jumpCounter = timeBetweenJumps;
        }

        //TimeCounters for attacks
        if (shotCounter <= 0)
        {
            shotCounter = timeBetweenShots;
            anim.SetTrigger("isAttacking");
            Instantiate(bullet, shotPoint.position, Quaternion.identity);
            anim.SetTrigger("endsAttacking");
        }
        #endregion
    }
}

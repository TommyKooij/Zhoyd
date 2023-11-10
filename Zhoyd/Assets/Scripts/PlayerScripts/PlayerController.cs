using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region VARIABLES
    public Rigidbody2D theRB;
    public float gravity;
    public bool isUsingElevator;

    [Header("Movement")]
    public float moveSpeed;
    public float minMoveSpeed;
    public float maxMoveSpeed;
    public float timeBeforeMaxSpeed;
    public float walkDrag;
    public bool canMove;
    public float velocity;
    public float walkGravity;
    public float fallGravity;
    public float gravityTimeFall;

    [Header("Jump")]
    public float jumpThreshold;
    public float jumpForce;
    private bool canDoubleJump;
    private bool isJumping;
    private float jumpButtonPress;
    public float jumpButtonPressLimit;
    public float jumpDrag;
    public float jumpSpeed;
    public float jumpGravity;
    public float gravityTimeJump;
    private bool cantJump;

    [Header("Layers")]
    public Transform groundPoint;
    public bool isOnGround;
    public LayerMask whatIsGround;
    public bool isInWater;
    public LayerMask whatIsWater;
    public bool isInTightSpace;
    public LayerMask whatIsTightSpace;
    public bool isOnSlope;
    public LayerMask whatIsSlope;

    [Header("Animators")]
    public Animator anim;
    public Animator animCrouch;
    public Animator animCrawl;

    [Header("Abilities")]
    private PlayerAbilityTracker abilities;

    [Header("Sprites")]
    public SpriteRenderer theSR;
    public SpriteRenderer afterImage;
    public GameObject standing;
    public GameObject crouching;
    public GameObject crawling;
    public GameObject staticStance;
    public bool isStanding;
    public bool isCrouching;
    public bool isCrawling;
    private float crouchButtonPress;
    public float crouchButtonPressLimit;

    [Header("Attack")]
/*    public BulletController shotToFire;
    public Transform shotPoint;
    public Transform shotPointCrouch;*/
    public float attackDelay;
    private float attackCounter;
    public float attackLunge;
    private int attackCount = 0;
    public GameObject attackHitbox;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();

        abilities = GetComponent<PlayerAbilityTracker>();

        canMove = true;

        isStanding = true;
        isCrouching = false;
        isCrawling = false;

        attackCounter = attackDelay;

        theRB.gravityScale = walkGravity;
    }

    // Update is called once per frame
    void Update()
    {
        gravity = theRB.gravityScale;

        if (isUsingElevator == true)
        {
            standing.SetActive(false);
            crouching.SetActive(false);
            crawling.SetActive(false);
            isStanding = false;
            isCrouching = false;
            isCrawling = false;
            staticStance.SetActive(true);
            crouchButtonPress = 0f;
        }
        else if (isCrouching == false && isCrawling == false)
        {
            standing.SetActive(true);
            isStanding = true;
            staticStance.SetActive(false);
        }

        if (canMove && Time.timeScale != 0)
        {
            #region MOVEMENT
            if (isStanding || isCrawling)
            {

                #region MOVESPEED
                if (Input.GetAxisRaw("Horizontal") > .1f)
                {
                    if (isCrawling)
                    {
                        moveSpeed = minMoveSpeed;
                        theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
                    }
                    else
                    {
                        moveSpeed = Mathf.MoveTowards(moveSpeed, maxMoveSpeed, timeBeforeMaxSpeed * Time.deltaTime);
                        theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
                    }
                }
                else if (Input.GetAxisRaw("Horizontal") < -.1f)
                {
                    if (isCrawling)
                    {
                        moveSpeed = minMoveSpeed;
                        theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
                    }
                    else
                    {
                        moveSpeed = Mathf.MoveTowards(moveSpeed, maxMoveSpeed, timeBeforeMaxSpeed * Time.deltaTime);
                        theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
                    }
                }

                if (Input.GetAxisRaw("Horizontal") == 0f)
                {
                    moveSpeed = Mathf.MoveTowards(moveSpeed, 0f, (timeBeforeMaxSpeed * 2f) * Time.deltaTime);
                }

                if (isJumping)
                {
                    moveSpeed = jumpSpeed;
                }
                #endregion

                #region DRAG
                if (isOnGround)
                {
                    theRB.drag = walkDrag;
                    theRB.gravityScale = walkGravity;
                }
                
                if (isJumping)
                {
                    theRB.drag = jumpDrag;
                    if (theRB.gravityScale < jumpGravity)
                    {
                        theRB.gravityScale = Mathf.MoveTowards(theRB.gravityScale, jumpGravity, gravityTimeJump * Time.deltaTime);
                    }
                }

                if (!isOnGround && !isJumping)
                {
                    theRB.drag = jumpDrag;
                    if (theRB.gravityScale < fallGravity)
                    {
                        theRB.gravityScale = Mathf.MoveTowards(theRB.gravityScale, fallGravity, gravityTimeFall * Time.deltaTime);
                    }
                }
                #endregion
            }

            if (isCrouching)
            {
                if (Input.GetAxisRaw("Horizontal") > .1f)
                {
                    transform.localScale = Vector3.one;
                }
                else if (Input.GetAxisRaw("Horizontal") < -.1f)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
            }

            velocity = theRB.velocity.x;
            #endregion

            #region DIRECTION CHANGE
            if (theRB.velocity.x < -0.1f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (theRB.velocity.x > 0.1f)
            {
                transform.localScale = Vector3.one;
            }
            #endregion

            #region GROUND CHECK
            isOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);
            isInWater = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsWater);
            isInTightSpace = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsTightSpace);
            isOnSlope = Physics2D.OverlapCircle(groundPoint.position, .01f, whatIsSlope);

            if (isCrawling && !isOnGround)
            {
                isCrawling = false;
                isStanding = true;
                crawling.SetActive(false);
                standing.SetActive(true);
            }

            if (isOnSlope)
            {
                if (Input.GetAxisRaw("Horizontal") == 0f && !Input.GetButton("Jump"))
                {
                    theRB.velocity = new Vector2(0f, 0f);
                    theRB.isKinematic = true;
                }
                else
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, theRB.velocity.y);
                    theRB.isKinematic = false;
                }
            }
            #endregion

            #region JUMP
            if (isStanding == true)
            {
                cantJump = false;
            }
            else
            {
                cantJump = true;
            }

            if (!cantJump && !isCrawling && Input.GetButtonDown("Jump") && (isOnGround || (canDoubleJump && abilities.canDoubleJump)))
            {
                isJumping = true;
                jumpButtonPress = 0f;
                #region JUMP ANIMATIONS
                if ((theRB.velocity.x >= jumpThreshold || theRB.velocity.x <= -jumpThreshold))
                {
                    //special jump
                    if (isOnGround)
                    {
                        //double jump
                        canDoubleJump = true;
                    }
                    else
                    {
                        canDoubleJump = false;

                        anim.SetTrigger("doubleJump");
                    }

                    anim.SetTrigger("holdJump");
                }
                else
                {
                    //normal jump
                    if (isOnGround)
                    {
                        //double jump
                        canDoubleJump = true;
                    }
                    else
                    {
                        canDoubleJump = false;

                        anim.SetTrigger("doubleJump");
                    }

                    anim.SetTrigger("normalJump");
                }
                #endregion
            }

            #region JUMP BUTTON HOLD
            if (isJumping || !isOnGround)
            {
                if (isJumping)
                {
                    jumpButtonPress += Time.deltaTime;
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);

                    if (jumpButtonPress > jumpButtonPressLimit || Input.GetKeyUp(KeyCode.Space))
                    {
                        isJumping = false;
                    }
                }
            }
            #endregion

            #endregion

            #region ATTACK
            attackCounter -= Time.deltaTime;
            anim.SetBool("isAttacking", false);

            if (attackCounter <= 0)
            {
                if (isStanding)
                {
                    if (attackCount == 0)
                    {
                        if (Input.GetButtonDown("Fire1") && isOnGround)
                        {
                            anim.SetTrigger("punch");
                            anim.SetBool("isAttacking", true);

                            //lunge on no enemy hit, else do not lunge
                            theRB.velocity = new Vector2(attackLunge * transform.localScale.x, theRB.velocity.y);

                            attackCounter = attackDelay;
                            attackCount++;
                        }
                    }
                    else if (attackCount == 1)
                    {
                        if (Input.GetButtonDown("Fire1") && isOnGround)
                        {
                            anim.SetTrigger("followupPunch");
                            anim.SetBool("isAttacking", true);

                            //lunge on no enemy hit, else do not lunge
                            theRB.velocity = new Vector2(attackLunge * transform.localScale.x, theRB.velocity.y);

                            attackCounter = attackDelay;
                            attackCount--;
                        }
                    }
                }
            }
            #endregion

            #region CROUCH & CRAWL
            if (Input.GetAxisRaw("Vertical") < -.9f)
            {
                if (isStanding == true && moveSpeed == 0f)
                {
                    isStanding = false;
                    isCrawling = true;
                    standing.SetActive(false);
                    crawling.SetActive(true);
                }
            }

            if (Input.GetAxisRaw("Vertical") > .9f)
            {
                if (isCrawling == true && moveSpeed == 0f)
                {
                    isCrawling = false;
                    isStanding = true;
                    crawling.SetActive(false);
                    standing.SetActive(true);
                }
            }
            #endregion

            #region ANIMATIONS
            if (standing.activeSelf)
            {
                anim.SetBool("isOnGround", isOnGround);
                anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
            }

            if (crawling.activeSelf)
            {
                animCrawl.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
            }
            #endregion

        }
        else
        {
            theRB.velocity = Vector2.zero;
        }

        /*#region DELETE PLAYPREFS
        if (Input.GetKeyDown("z"))
        {
            PlayerPrefs.DeleteAll();
        }
        #endregion*/
    }

    #region METHODS
    public void ChangeToStanding()
    {
        isCrawling = false;
        crawling.SetActive(false);
        standing.SetActive(true);
        moveSpeed = minMoveSpeed;
        cantJump = false;
    }
    #endregion
}

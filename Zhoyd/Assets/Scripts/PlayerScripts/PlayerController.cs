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

    [Header("Animators")]
    public Animator anim;
    public Animator animCrouchShoot;
    public Animator animCrawlShoot;

    [Header("Abilities")]
    private PlayerAbilityTracker abilities;

    [Header("Sprites")]
    public SpriteRenderer theSR;
    public SpriteRenderer afterImage;
    public GameObject standingShoot;
    public GameObject crouchingShoot;
    public GameObject crawlingShoot;
    public GameObject staticShoot;
    public bool isStanding;
    public bool isCrouching;
    public bool isCrawling;
    private float crouchButtonPress;
    public float crouchButtonPressLimit;

    [Header("Shot")]
    public BulletController shotToFire;
    public Transform shotPoint;
    public Transform shotPointCrouch;
    public float shotDelay;
    private float shotCounter;

    [Header("Dash")]
    public float dashSpeed;
    public float dashTime;
    private float dashCounter;

    public float afterImageLifetime;
    public float timeBetweenAfterImages;
    private float afterImageCounter;
    public Color afterImageColor;

    public float waitAfterDashing;
    private float dashRechargeCounter;
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

        shotCounter = shotDelay;

        theRB.gravityScale = walkGravity;
    }

    // Update is called once per frame
    void Update()
    {
        gravity = theRB.gravityScale;

        if (isUsingElevator == true)
        {
            standingShoot.SetActive(false);
            crouchingShoot.SetActive(false);
            crawlingShoot.SetActive(false);
            isStanding = false;
            isCrouching = false;
            isCrawling = false;
            staticShoot.SetActive(true);
            crouchButtonPress = 0f;
        }
        else if (isCrouching == false && isCrawling == false)
        {
            standingShoot.SetActive(true);
            isStanding = true;
            staticShoot.SetActive(false);
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

            if (isCrawling && !isOnGround)
            {
                isCrawling = false;
                isStanding = true;
                crawlingShoot.SetActive(false);
                standingShoot.SetActive(true);
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
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                if (isStanding)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        anim.SetTrigger("shoot");
                        Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0f);

                        shotCounter = shotDelay;
                    }
                }
                else if (isCrouching)
                {
                    if (Input.GetButtonDown("Fire1") && isOnGround)
                    {
                        anim.SetTrigger("shootCrouch");
                        Instantiate(shotToFire, shotPointCrouch.position, shotPointCrouch.rotation).moveDir = new Vector2(transform.localScale.x, 0f);

                        shotCounter = shotDelay;
                    }
                }
            }
            #endregion

            #region CROUCH & CRAWL
            if (Input.GetAxisRaw("Vertical") < -.9f)
            {
                crouchButtonPress += Time.deltaTime;
                if (crouchButtonPress < crouchButtonPressLimit && isStanding == true && moveSpeed == 0f)
                {
                    isStanding = false;
                    isCrouching = true;
                    standingShoot.SetActive(false);
                    crouchingShoot.SetActive(true);
                }
                else if (crouchButtonPress >= crouchButtonPressLimit && isCrouching == true)
                {
                    isCrouching = false;
                    isCrawling = true;
                    crouchingShoot.SetActive(false);
                    crawlingShoot.SetActive(true);
                }
            }

            if (Input.GetAxisRaw("Vertical") > .9f)
            {
                crouchButtonPress += Time.deltaTime;
                if (crouchButtonPress < crouchButtonPressLimit && isCrawling == true && moveSpeed == 0f && !isInTightSpace)
                {
                    isCrawling = false;
                    isCrouching = true;
                    crawlingShoot.SetActive(false);
                    crouchingShoot.SetActive(true);
                }
                else if (crouchButtonPress >= crouchButtonPressLimit && isCrouching == true)
                {
                    isCrouching = false;
                    isStanding = true;
                    crouchingShoot.SetActive(false);
                    standingShoot.SetActive(true);
                }
            }

            if (Input.GetAxisRaw("Vertical") == 0f)
            {
                if (isCrouching != true)
                {
                    crouchButtonPress = 0f;
                }
            }
            #endregion


            /*#region DASH
            if (dashRechargeCounter > 0)
            {
                dashRechargeCounter -= Time.deltaTime;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    dashCounter = dashTime;

                    ShowAfterImage();
                }
            }

            if (dashCounter > 0)
            {
                dashCounter = dashCounter - Time.deltaTime;

                theRB.velocity = new Vector2(dashSpeed * transform.localScale.x, theRB.velocity.y);

                afterImageCounter -= Time.deltaTime;
                if (afterImageCounter <= 0)
                {
                    ShowAfterImage();
                }

                dashRechargeCounter = waitAfterDashing;
            }
            #endregion*/

            #region ANIMATIONS
            if (standingShoot.activeSelf)
            {
                anim.SetBool("isOnGround", isOnGround);
                anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
            }

            if (crawlingShoot.activeSelf)
            {
                animCrawlShoot.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
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
        crawlingShoot.SetActive(false);
        standingShoot.SetActive(true);
        moveSpeed = minMoveSpeed;
        cantJump = false;
    }

    public void ShowAfterImage()
    {
        SpriteRenderer image = Instantiate(afterImage, transform.position, transform.rotation);
        image.sprite = theSR.sprite;
        image.transform.localScale = transform.localScale;
        image.color = afterImageColor;

        Destroy(image.gameObject, afterImageLifetime);

        afterImageCounter = timeBetweenAfterImages;
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    #region VARIABLES
    [Header("Door Type")]
    public bool energy;
    public bool flux;
    public bool scorch;
    public bool rime;
    public bool boss;

    [Header("Boss Access")]
    public bool needsThresherAccess;

    public Animator anim;
    public float distanceToOpen;
    private PlayerController thePlayer;
    private PlayerAbilityTracker abilities;

    private bool playerExit;

    public Transform exitPoint;
    public float movePlayerSpeed;

    public string levelToLoad;

    public GameObject doorCollider2D;

    public bool isNotOpen = true;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        abilities = PlayerHealthController.instance.GetComponent<PlayerAbilityTracker>();

        thePlayer = PlayerHealthController.instance.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        #region DOOR ACCESS CHECK
        // Checks the type of door and if player has access to it
        if (energy)
        {
            if (abilities.hasEmeraldAccess)
            {
                isNotOpen = false;
                anim.SetTrigger("hasAccess");
            }
            else
            {
                isNotOpen = true;
            }
        }
        
        if (flux)
        {
            if (abilities.hasVioletAccess)
            {
                isNotOpen = false;
                anim.SetTrigger("hasAccess");
            }
            else
            {
                isNotOpen = true;
            }
        }
        
        if (scorch)
        {
            if (abilities.hasScarletAccess)
            {
                isNotOpen = false;
                anim.SetTrigger("hasAccess");
            }
            else
            {
                isNotOpen = true;
            }
        }
        
        if (rime)
        {
            if (abilities.hasSapphireAccess)
            {
                isNotOpen = false;
                anim.SetTrigger("hasAccess");
            }
            else
            {
                isNotOpen = true;
            }
        }

        //boss doors
        if (boss)
        {
            if (needsThresherAccess && abilities.hasThresherAccess)
            {
                isNotOpen = false;
                anim.SetTrigger("hasAccess");
            }
            else
            {
                isNotOpen = true;
            }
        }
        
        if (!energy && !flux && !scorch && !rime && !boss)
        {
            isNotOpen = false;
            anim.SetTrigger("hasAccess");
        }
        #endregion

        // Checks if the door can be opened
        if (isNotOpen == true)
        {
            anim.SetBool("doorOpen", false);
            isNotOpen = true;
        } 
        else
        {
            if (Vector3.Distance(transform.position, thePlayer.transform.position) < distanceToOpen)
            {
                anim.SetBool("doorOpen", true);
                doorCollider2D.SetActive(false);
            }
            else
            {
                anim.SetBool("doorOpen", false);
                doorCollider2D.SetActive(true);
            }
        }
        

        if (playerExit)
        {
            thePlayer.transform.position = Vector3.MoveTowards(thePlayer.transform.position, exitPoint.position, movePlayerSpeed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && isNotOpen != true && thePlayer.isOnGround == true)
        {
            if (!playerExit)
            {
                thePlayer.canMove = false;

                StartCoroutine(UseDoorCo());
            }
        }
    }


    IEnumerator UseDoorCo()
    {
        thePlayer.ChangeToStanding();

        playerExit = true;

        //thePlayer.anim.enabled = false;

        UIController.instance.StartFadeToBlack();

        yield return new WaitForSeconds(1.5f);

        RespawnController.instance.SetSpawn(exitPoint.position);
        thePlayer.canMove = true;
        thePlayer.anim.enabled = true;

        UIController.instance.StartFadeFromBlack();

        doorCollider2D.SetActive(true);

        SceneManager.LoadScene(levelToLoad);
    }
}

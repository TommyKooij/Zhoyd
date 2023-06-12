using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorEntry : MonoBehaviour
{

    public Animator anim;
    public float distanceToOpen;
    private PlayerController thePlayer;

    public bool playerExit;

    public Transform exitPoint;
    public float movePlayerSpeed;

    public string levelToLoad;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = PlayerHealthController.instance.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Vector3.Distance(transform.position, thePlayer.transform.position) < distanceToOpen)
        {
            anim.SetBool("doorOpen", true);
        } else
        {
            anim.SetBool("doorOpen", false);
        }

        if (playerExit)
        {
            thePlayer.transform.position = Vector3.MoveTowards(thePlayer.transform.position, exitPoint.position, movePlayerSpeed * Time.deltaTime);
        }*/
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            anim.SetBool("doorOpen", true);
        }
    }


    IEnumerator UseDoorCo()
    {
        playerExit = true;

        thePlayer.anim.enabled = false;

        UIController.instance.StartFadeToBlack();

        yield return new WaitForSeconds(1.5f);

        RespawnController.instance.SetSpawn(exitPoint.position);
        thePlayer.canMove = true;
        thePlayer.anim.enabled = true;

        UIController.instance.StartFadeFromBlack();

        SceneManager.LoadScene(levelToLoad);
    }
}

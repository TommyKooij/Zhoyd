using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    #region VARIABLES
    private CameraController theCamera;
    public Transform camPosition;
    public float camSpeed;

    public Animator anim;
    public Transform theBoss;

    public int threshold1, threshold2;
    public float activeTime;
    private float activeCounter;

    public float shotCounter, timeBetweenShots;
    public GameObject bullet;
    public Transform shotPoint;

    public bool isOnGround;

    public GameObject rewards;
    private bool battleEnded;

    public int bossMusicNumber;
    public int worldMusicNumber;

    public string bossRef;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //theCamera = FindObjectOfType<CameraController>();
        //theCamera.enabled = false;

        //shotCounter = timeBetweenShots;

        AudioManager.instance.PlayBossMusic(bossMusicNumber);
    }

    // Update is called once per frame
    void Update()
    {
        if (!battleEnded)
        {
            //theCamera.transform.position = Vector3.MoveTowards(theCamera.transform.position, camPosition.transform.position, camSpeed * Time.deltaTime);

        } 
        else
        {
            if (rewards != null)
            {
                rewards.SetActive(true);
                rewards.transform.SetParent(null);
            }

            gameObject.SetActive(false);
            AudioManager.instance.PlayWorldMusic(worldMusicNumber);
        }
    }

    public void EndBattle()
    {
        battleEnded = true;

        theBoss.GetComponent<Collider2D>().enabled = false;
        gameObject.SetActive(false);

        PlayerPrefs.SetInt(bossRef, 1);
    }
}

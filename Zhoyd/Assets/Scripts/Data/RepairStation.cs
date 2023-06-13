using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairStation : MonoBehaviour
{
    #region VARIABLES
    private PlayerController player;

    public GameObject placePlayer;
    public string levelToLoad;
    private bool isSaving;

    public float timeToSave;
    private float saveCounter;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthController.instance.GetComponent<PlayerController>();

        saveCounter = timeToSave;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSaving == true)
        {
            placePlayer.SetActive(true);
            player.transform.position = placePlayer.transform.position;
            player.canMove = false;
            player.isUsingElevator = true;
            PlayerHealthController.instance.FillHealthbar();

            saveCounter -= Time.deltaTime;
            if (saveCounter <= 0f)
            {
                placePlayer.SetActive(false);
                player.canMove = true;
                player.isUsingElevator = false;
                saveCounter = timeToSave;
                isSaving = false;
            }
            else
            {
                PlayerPrefs.SetString("LoadLevel", levelToLoad);
                PlayerPrefs.SetFloat("PosX", placePlayer.transform.position.x);
                PlayerPrefs.SetFloat("PosY", placePlayer.transform.position.y);
                PlayerPrefs.SetFloat("PosZ", placePlayer.transform.position.z);
            }
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && Input.GetAxisRaw("Vertical") > .9f)
        {
            isSaving = true;
        }
    }

    public void Save()
    {
        SaveSystem.SavePlayer(player);
    }
}

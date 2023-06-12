using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairStation : MonoBehaviour
{
    #region VARIABLES
    private PlayerController player;

    public GameObject placePlayer;
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

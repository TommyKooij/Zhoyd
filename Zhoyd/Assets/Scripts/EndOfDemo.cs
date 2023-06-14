using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfDemo : MonoBehaviour
{
    #region VARIABLES
    public GameObject elevator;
    #endregion

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && Input.GetAxisRaw("Vertical") > .9f)
        {
            PlayerPrefs.DeleteAll();
            AudioManager.instance.PlayMainMenuMusic();
            Destroy(elevator);
            UIController.instance.MainMenu();
        }
    }
}

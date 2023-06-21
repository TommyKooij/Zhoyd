using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfDemo : MonoBehaviour
{
    #region VARIABLES
    public GameObject elevator;
    #endregion

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerPrefs.DeleteAll();
            AudioManager.instance.PlayMainMenuMusic();
            Destroy(elevator);
            UIController.instance.MainMenu();
        }
    }
}

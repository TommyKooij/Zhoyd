using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    #region VARIABLES
    public GameObject bossToActivate;
    public GameObject obstacles;
    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (obstacles != null)
            {
                obstacles.SetActive(true);
            }

            bossToActivate.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}

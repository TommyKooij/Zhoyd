using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    #region VARIABLES
    public GameObject bossToActivate;
    public string bossRef;
    #endregion

    private void Awake()
    {
        if (PlayerPrefs.HasKey(bossRef))
        {
            if (PlayerPrefs.GetInt(bossRef) == 1)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (PlayerPrefs.HasKey(bossRef))
            {
                if (PlayerPrefs.GetInt(bossRef) != 1)
                {
                    bossToActivate.SetActive(true);
                    gameObject.SetActive(false);
                }
            }
            else
            {
                bossToActivate.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityUnlock : MonoBehaviour
{
    #region VARIABLES
    public string nameAbility;

    [Header("Access")]
    public bool emeraldAccess;
    public bool violetAccess;
    public bool scarletAccess;
    public bool sapphireAccess;

    [Header("Boss Access")]
    public bool thresherAccess;

    [Header("Suits")]
    public bool vulcanSuit;
    public bool arcticSuit;
    public bool kaiserSuit;

    [Header("Modules")]
    public bool unlockDoubleJump;
    public bool unlockDash;
    public bool unlockRockets;

    [Header("Chargers")]
    public bool unlockFluxCharger;
    public bool unlockScorchCharger;
    public bool unlockRimeCharger;

    public GameObject PickupEffect;

    public string unlockMessage;
    public TMP_Text unlockText;
    #endregion

    private void Awake()
    {
        if (PlayerPrefs.HasKey(nameAbility))
        {
            if (PlayerPrefs.GetInt(nameAbility) == 1)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerAbilityTracker player = other.GetComponentInParent<PlayerAbilityTracker>();

            #region ABILITIES
            if (unlockDoubleJump)
            {
                player.canDoubleJump = true;
                PlayerPrefs.SetInt("DoubleJumpUnlocked", 1);
            }

            if (unlockDash)
            {
                player.canDash = true;
                PlayerPrefs.SetInt("DashUnlocked", 1);
            }

            if (unlockRockets)
            {
                player.canShootRocket = true;
                PlayerPrefs.SetInt("RocketsUnlocked", 1);
            }
            #endregion

            #region CHARGERS
            if (unlockFluxCharger)
            {
                player.hasFluxCharger = true;
            }

            if (unlockScorchCharger)
            {
                player.hasScorchCharger = true;
            }

            if (unlockRimeCharger)
            {
                player.hasRimeCharger = true;
            }
            #endregion

            #region ACCESS
            if (emeraldAccess)
            {
                player.hasEmeraldAccess = true;
                PlayerPrefs.SetInt("EmeraldAccessUnlocked", 1);
            }

            if (violetAccess)
            {
                player.hasVioletAccess = true;
                PlayerPrefs.SetInt("VioletAccessUnlocked", 1);
            }

            if (scarletAccess)
            {
                player.hasScarletAccess = true;
                PlayerPrefs.SetInt("ScarletAccessUnlocked", 1);
            }

            if (sapphireAccess)
            {
                player.hasSapphireAccess = true;
                PlayerPrefs.SetInt("SapphireAccessUnlocked", 1);
            }

            //boss access
            if (thresherAccess)
            {
                player.hasThresherAccess = true;
                PlayerPrefs.SetInt("ThresherUnlocked", 1);
            }
            #endregion

            #region PICKUP
            Instantiate(PickupEffect, transform.position, transform.rotation);

            unlockText.transform.parent.SetParent(null);
            unlockText.transform.parent.position = transform.position;

            unlockText.text = unlockMessage;
            unlockText.gameObject.SetActive(true);

            Destroy(unlockText.transform.parent.gameObject, 5f);

            Destroy(gameObject);
            #endregion
        }
    }
}

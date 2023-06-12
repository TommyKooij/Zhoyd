using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityUnlock : MonoBehaviour
{
    #region VARIABLES
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerAbilityTracker player = other.GetComponentInParent<PlayerAbilityTracker>();

            #region ABILITIES
            if (unlockDoubleJump)
            {
                player.canDoubleJump = true;
            }

            if (unlockDash)
            {
                player.canDash = true;
            }

            if (unlockRockets)
            {
                player.canShootRocket = true;
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
            }

            if (violetAccess)
            {
                player.hasVioletAccess = true;
            }

            if (scarletAccess)
            {
                player.hasScarletAccess = true;
            }

            if (sapphireAccess)
            {
                player.hasSapphireAccess = true;
            }

            //boss access
            if (thresherAccess)
            {
                player.hasThresherAccess = true;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityTracker : MonoBehaviour
{
    [Header("Access")]
    public bool hasEmeraldAccess;
    public bool hasVioletAccess;
    public bool hasScarletAccess;
    public bool hasSapphireAccess;
    public bool hasBossAccess;

    [Header("Boss Access")]
    public bool hasThresherAccess;

    [Header("Suits")]
    public bool hasVulcanSuit;
    public bool hasArcticSuit;
    public bool hasKaiserSuit;

    [Header("Chargers")]
    public bool hasFluxCharger;
    public bool hasScorchCharger;
    public bool hasRimeCharger;

    [Header("Modules")]
    public bool canDoubleJump;
    public bool canDash;
    public bool canShootRocket;
}

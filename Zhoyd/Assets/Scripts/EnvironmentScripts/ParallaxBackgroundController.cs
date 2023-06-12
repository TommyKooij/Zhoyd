using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackgroundController : MonoBehaviour
{
    private PlayerController playerController;

    public Camera theCam;
    Vector2 startPosition;
    float startZ;

    Vector2 travel => (Vector2)theCam.transform.position - startPosition;

    float distanceFromPlayer => transform.position.z - playerController.transform.position.z;
    float clippinPlane => (theCam.transform.position.z + (distanceFromPlayer > 0 ? theCam.farClipPlane : theCam.nearClipPlane));

    float parallaxFactor => Mathf.Abs(distanceFromPlayer) / clippinPlane;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        startPosition = transform.position;
        startZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController != null)
        {
            Vector2 newPos = startPosition + travel * parallaxFactor;
            transform.position = new Vector3(newPos.x, newPos.y, startZ);
        }
        else
        {
            playerController = FindObjectOfType<PlayerController>();
        }
        
    }
}

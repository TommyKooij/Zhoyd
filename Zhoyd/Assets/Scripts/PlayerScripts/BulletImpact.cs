using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    #region VARIABLES
    public float animationTime;
    private float animationCounter;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        animationCounter = animationTime;
    }

    // Update is called once per frame
    void Update()
    {
        animationCounter -= Time.deltaTime;
        if (animationCounter <= 0f)
        {
            Destroy(gameObject);
        }
    }
}

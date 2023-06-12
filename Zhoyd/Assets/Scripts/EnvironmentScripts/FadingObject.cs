using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingObject : MonoBehaviour
{
    public GameObject block;
    public SpriteRenderer blockImage;
    public float fadeSpeed;
    private bool hasTouched, isTouching;

    public float fadeTimer;
    private float fadeCounter;

    // Start is called before the first frame update
    void Start()
    {
        fadeTimer = fadeCounter;
    }

    // Update is called once per frame
    void Update()
    {
        while (isTouching)
        {
            fadeTimer = fadeCounter;
        }

        if (hasTouched)
        {
            blockImage.color = new Color(blockImage.color.r, blockImage.color.g, blockImage.color.b, Mathf.MoveTowards(blockImage.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (blockImage.color.a == 0f)
            {
                block.SetActive(false);

                fadeTimer -= Time.deltaTime;
                if (fadeTimer <= 0)
                {
                    fadeTimer = fadeCounter;
                    hasTouched = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            hasTouched = true;
        }
    }
}

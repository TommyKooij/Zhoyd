using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingBackObject : MonoBehaviour
{
    public GameObject block;
    public SpriteRenderer blockImage;
    public float fadeSpeed;
    public bool hasTouchedAfterFade;

    public float fadeTimer;
    private float fadeCounter;

    // Start is called before the first frame update
    void Start()
    {
        fadeCounter = 2f;
        fadeTimer = fadeCounter;
    }

    // Update is called once per frame
    void Update()
    {
        if (blockImage.color.a < 1f)
        {
            if (hasTouchedAfterFade)
            {
                fadeTimer -= Time.deltaTime;
                if (fadeTimer <= 0)
                {
                    block.SetActive(true);
                    blockImage.color = new Color(blockImage.color.r, blockImage.color.g, blockImage.color.b, Mathf.MoveTowards(blockImage.color.a, 1f, fadeSpeed * Time.deltaTime));

                }
            }
        } 
        else if (blockImage.color.a == 1f)
        {
            fadeTimer = fadeCounter;
            hasTouchedAfterFade = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (blockImage.color.a == 0f)
        {
            if (other.tag == "Player")
            {
                hasTouchedAfterFade = true;
            }
        } 
        else if (blockImage.color.a == 1f)
        {
            hasTouchedAfterFade = false;
        }
    }
}

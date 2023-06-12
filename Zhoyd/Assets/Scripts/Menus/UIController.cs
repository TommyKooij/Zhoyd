using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public static UIController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Slider healthSlider;
    public Image fadeScreen;
    public float fadeSpeed = 2;
    private bool fadingToBlack, fadingFromBlack;

    public string mainMenuScene;
    public GameObject pauseScreen;
    public GameObject options, arrowsOptionsList;
    public GameObject[] arrowOptions;
    private int i;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fadingToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 1f)
            {
                fadingToBlack = false;
            }
        } else if (fadingFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 0f)
            {
                fadingFromBlack = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseScreen.activeSelf)
            {
                pauseScreen.SetActive(true);

                Time.timeScale = 0f;
            }
            else
            {
                pauseScreen.SetActive(false);

                Time.timeScale = 1f;
            }
        }

        if (pauseScreen.activeInHierarchy && Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (i == 4)
            {
                arrowOptions[4].SetActive(false);
                arrowOptions[0].SetActive(true);
                i = 0;
            }
            else
            {
                arrowOptions[i].SetActive(false);
                arrowOptions[i + 1].SetActive(true);
                i++;
            }
        }
        else if (pauseScreen.activeInHierarchy && Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (i == 0)
            {
                arrowOptions[0].SetActive(false);
                arrowOptions[4].SetActive(true);
                i = 4;
            }
            else
            {
                arrowOptions[i].SetActive(false);
                arrowOptions[i - 1].SetActive(true);
                i--;
            }
        }

        if (pauseScreen.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
        {
            if (i == 0)
            {

            }
            else if (i == 1)
            {

            }
            else if (i == 2)
            {

            }
            else if (i == 3)
            {
                
            }
            else if (i == 4)
            {
                MainMenu();
            }
        }
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void StartFadeToBlack()
    {
        fadingToBlack = true;
        fadingFromBlack = false;
    }

    public void StartFadeFromBlack()
    {
        fadingToBlack = false;
        fadingFromBlack = true;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;

        Destroy(PlayerHealthController.instance.gameObject);
        PlayerHealthController.instance = null;
        Destroy(RespawnController.instance.gameObject);
        RespawnController.instance = null;
        instance = null;
        Destroy(gameObject);

        SceneManager.LoadScene(mainMenuScene);
    }
}

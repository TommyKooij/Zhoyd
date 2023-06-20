using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    #region VARIABLES
    public GameObject energyBar;
    public Slider healthSlider;
    public Image fadeScreen;
    public float fadeSpeed = 2;
    private bool fadingToBlack, fadingFromBlack;

    public string mainMenuScene;
    public GameObject pauseScreen;
    public GameObject options, arrowsOptionsList;
    public GameObject[] arrowOptions;
    private int i;

    public Button map, data, triumphs, settings;
    #endregion

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
        } 
        else if (fadingFromBlack)
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
                map.interactable = false;
                data.interactable = false;
                triumphs.interactable = false;
                settings.interactable = false;

                pauseScreen.SetActive(true);

                Time.timeScale = 0f;
            }
            else
            {
                pauseScreen.SetActive(false);

                Time.timeScale = 1f;
            }
        }
    }

    #region METHODS
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

        AudioManager.instance.PlayMainMenuMusic();
        Destroy(PlayerHealthController.instance.gameObject);
        PlayerHealthController.instance = null;
        Destroy(RespawnController.instance.gameObject);
        RespawnController.instance = null;
        instance = null;
        Destroy(gameObject);

        SceneManager.LoadScene(mainMenuScene);
    }
    #endregion
}

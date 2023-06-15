using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region VARIABLES
    public string newGameScene;
    public GameObject continueButton;
    public PlayerAbilityTracker player;

    [Header("Fade Screen")]
    public Image fadeScreen;
    public float fadeSpeed = 2;
    private bool fadingToBlack, fadingFromBlack;
    private bool isNewGame;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("LoadLevel"))
        {
            continueButton.SetActive(true);
        }

        AudioManager.instance.PlayMainMenuMusic();
    }

    void Update()
    {
        if (fadingToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 1f)
            {
                fadingToBlack = false;

                if (isNewGame)
                {
                    isNewGame = false;

                    SceneManager.LoadScene("Intro");
                }
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
    }

    #region METHODS
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();

        isNewGame = true;
        fadingToBlack = true;
    }

    public void ContinueGame()
    {
        player.gameObject.SetActive(true);
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"), PlayerPrefs.GetFloat("PosZ"));
        SceneManager.LoadScene(PlayerPrefs.GetString("LoadLevel"));

        #region ABILITY UNLOCKS
        if (PlayerPrefs.HasKey("DoubleJumpUnlocked"))
        {
            if (PlayerPrefs.GetInt("DoubleJumpUnlocked") == 1)
            {
                player.canDoubleJump = true;
            }
        }
        #endregion

        #region ACCESS UNLOCKS
        if (PlayerPrefs.HasKey("EmeraldAccessUnlocked"))
        {
            if (PlayerPrefs.GetInt("EmeraldAccessUnlocked") == 1)
            {
                player.hasEmeraldAccess = true;
            }
        }

        if (PlayerPrefs.HasKey("VioletAccessUnlocked"))
        {
            if (PlayerPrefs.GetInt("VioletAccessUnlocked") == 1)
            {
                player.hasVioletAccess = true;
            }
        }

        if (PlayerPrefs.HasKey("ScarletAccessUnlocked"))
        {
            if (PlayerPrefs.GetInt("ScarletAccessUnlocked") == 1)
            {
                player.hasScarletAccess = true;
            }
        }

        if (PlayerPrefs.HasKey("SapphireAccessUnlocked"))
        {
            if (PlayerPrefs.GetInt("SapphireAccessUnlocked") == 1)
            {
                player.hasSapphireAccess = true;
            }
        }
        #endregion
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}

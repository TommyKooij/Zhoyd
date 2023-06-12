using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region VARIABLES
    public GameObject start, optionsList, arrowsOptionsList, savesListStart, savesListContinue, arrowsSavesList, settings;
    public GameObject[] arrowsOptions;
    public GameObject[] arrowsSaves;

    public string newGameScene;

    private int i;
    private int j;

    public Button[] buttons;

    [Header("Fade Screen")]
    public Image fadeScreen;
    public float fadeSpeed = 2;
    private bool fadingToBlack, fadingFromBlack;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
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

    /*// Update is called once per frame
    void Update()
    {
        //Select Save Start
        if (savesListStart.activeInHierarchy && Input.GetKeyDown(KeyCode.Space) || savesListStart.activeInHierarchy && Input.GetKeyDown(KeyCode.Return))
        {
            if (j == 0)
            {
                SceneManager.LoadScene(newGameScene);
            }
        }

        //Select Option
        if (optionsList.activeInHierarchy && Input.GetKeyDown(KeyCode.Space) || optionsList.activeInHierarchy && Input.GetKeyDown(KeyCode.Return))
        {
            if (i == 0)
            {
                if (!savesListStart.activeInHierarchy)
                {
                    optionsList.SetActive(false);
                    arrowsOptionsList.SetActive(false);
                    savesListStart.SetActive(true);
                    arrowsSavesList.SetActive(true);
                }
            }
            else if (i == 1)
            {
                if (!savesListContinue.activeInHierarchy)
                {
                    optionsList.SetActive(false);
                    arrowsOptionsList.SetActive(false);
                    savesListContinue.SetActive(true);
                    arrowsSavesList.SetActive(true);
                }
            }
            else if (i == 2)
            {
                if (!settings.activeInHierarchy)
                {

                }
            }
        }

        //Start Game
        if (Input.GetKeyDown(KeyCode.Return) && start.activeInHierarchy)
        {
            start.SetActive(false);
            optionsList.SetActive(true);
            arrowsOptionsList.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && optionsList.activeInHierarchy)
        {
            optionsList.SetActive(false);
            start.SetActive(true);
            arrowsOptionsList.SetActive(false);
        }

        //Scroll Arrows Options
        if (optionsList.activeInHierarchy && Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (i == 2)
            {
                arrowsOptions[2].SetActive(false);
                arrowsOptions[0].SetActive(true);
                i = 0;
            }
            else
            {
                arrowsOptions[i].SetActive(false);
                arrowsOptions[i + 1].SetActive(true);
                i++;
            }
        }
        else if (optionsList.activeInHierarchy && Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (i == 0)
            {
                arrowsOptions[0].SetActive(false);
                arrowsOptions[2].SetActive(true);
                i = 2;
            }
            else
            {
                arrowsOptions[i].SetActive(false);
                arrowsOptions[i - 1].SetActive(true);
                i--;
            }
        }

        //Cancel Option
        if (!optionsList.activeInHierarchy)
        {
            if (i == 0 && Input.GetKeyDown(KeyCode.Escape))
            {
                if (savesListStart.activeInHierarchy)
                {
                    optionsList.SetActive(true);
                    arrowsOptionsList.SetActive(true);
                    savesListStart.SetActive(false);
                    arrowsSavesList.SetActive(false);
                }
            }
            else if (i == 1 && Input.GetKeyDown(KeyCode.Escape))
            {
                if (savesListContinue.activeInHierarchy)
                {
                    optionsList.SetActive(true);
                    arrowsOptionsList.SetActive(true);
                    savesListContinue.SetActive(false);
                    arrowsSavesList.SetActive(false);
                }
            }
            else if (i == 2 && Input.GetKeyDown(KeyCode.Escape))
            {
                if (settings.activeInHierarchy)
                {

                }
            }
        }

        //Scroll Arrows Saves
        if (savesListStart.activeInHierarchy && Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (j == 2)
            {
                arrowsSaves[2].SetActive(false);
                arrowsSaves[0].SetActive(true);
                j = 0;
            }
            else
            {
                arrowsSaves[j].SetActive(false);
                arrowsSaves[j + 1].SetActive(true);
                j++;
            }
        }
        else if (savesListStart.activeInHierarchy && Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (j == 0)
            {
                arrowsSaves[0].SetActive(false);
                arrowsSaves[2].SetActive(true);
                j = 2;
            }
            else
            {
                arrowsSaves[j].SetActive(false);
                arrowsSaves[j - 1].SetActive(true);
                j--;
            }
        }

        
    }*/

    public void NewGame()
    {
        StartCoroutine(StartGameCo());
    }

    public void ContinueGame()
    {
        
    }

    public void QuitGame()
    {
        
    }

    IEnumerator StartGameCo()
    {
        fadingToBlack = true;

        yield return new WaitForSeconds(5f);

        fadingToBlack = false;
        fadingFromBlack = true;

        SceneManager.LoadScene(newGameScene);
    }
}

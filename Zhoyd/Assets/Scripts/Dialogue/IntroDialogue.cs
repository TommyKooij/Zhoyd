using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroDialogue : MonoBehaviour
{
    #region VARIABLES
    public GameObject text;
    public GameObject title;
    public string sceneToLoad;

    private DialogueTrigger trigger;
    private DialogueManager manager;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        trigger = FindObjectOfType<DialogueTrigger>();
        manager = FindObjectOfType<DialogueManager>();

        if (!text.activeInHierarchy)
        {
            text.SetActive(true);
            title.SetActive(true);
        }

        trigger.TriggerDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.dialogueEnded == true)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}

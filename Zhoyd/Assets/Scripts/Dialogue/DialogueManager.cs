using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    #region VARIABLES
    public TMP_Text nameText;
	public TMP_Text dialogueText;

	public Animator animator;

	public bool dialogueEnded;

	private Queue<string> sentences;
    #endregion

    // Use this for initialization
    void Start()
	{
		sentences = new Queue<string>();
	}

	public void StartDialogue(Dialogue dialogue)
	{
		dialogueEnded = false;
		//animator.SetBool("IsOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			nameText.text = "";
			dialogueText.text = "";
			StopAllCoroutines();
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(0.05f);
		}
	}

	void EndDialogue()
	{
		dialogueEnded = true;
		//animator.SetBool("IsOpen", false);
	}

}
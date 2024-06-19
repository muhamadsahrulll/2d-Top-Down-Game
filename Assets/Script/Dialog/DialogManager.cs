using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI namaText;
    public TextMeshProUGUI dialogText;
    public GameObject tutorialPanel;

    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
        tutorialPanel.SetActive(false); // Hide the tutorial panel at the start
    }

    public void StartDialog(Dialog dialog)
    {
        tutorialPanel.SetActive(true); // Show the tutorial panel when dialog starts

        namaText.text = dialog.name;

        sentences.Clear();
        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }

    void EndDialog()
    {
        tutorialPanel.SetActive(false); // Hide the tutorial panel when dialog ends
    }
}

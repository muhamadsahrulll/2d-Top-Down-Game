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
    private Queue<AudioClip> audioClips; // Tambahkan ini

    private void Start()
    {
        sentences = new Queue<string>();
        audioClips = new Queue<AudioClip>(); // Tambahkan ini
        tutorialPanel.SetActive(false); // Hide the tutorial panel at the start
    }

    public void StartDialog(Dialog dialog)
    {
        tutorialPanel.SetActive(true); // Show the tutorial panel when dialog starts

        namaText.text = dialog.name;

        sentences.Clear();
        audioClips.Clear(); // Tambahkan ini
        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (AudioClip clip in dialog.audioClips)
        {
            audioClips.Enqueue(clip); // Tambahkan ini
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
        AudioClip clip = audioClips.Dequeue(); // Tambahkan ini

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        AudioManager.instance.PlaySoundDialog(clip); // Tambahkan ini
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSound;


    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;

        audioSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(CheckSound());
    }



    IEnumerator CheckSound()
    {
        yield return new WaitForEndOfFrame();
        if (!GameData.InstanceData.onSound)
        {
            audioSound.Stop();
        }
    }

    public void soundSettings(bool on)
    {
        if (on)
        {
            audioSound.Play();
            GameData.InstanceData.onSound = true;
        }
        else
        {
            audioSound.Stop();
            GameData.InstanceData.onSound = false;
        }

        StartCoroutine(CheckSound());
    }
}
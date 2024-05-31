using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSound;
    public AudioClip buttonBakteri;
    public AudioClip buttonKuman;
    public AudioClip buttonOrganik;
    public AudioClip buttonAnorganik;
    public AudioClip sfxBenar;
    public AudioClip sfxSalah;
    public AudioClip sfxSelamat;



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

    public void PlayButtonBakteri()
    {
        if (GameData.InstanceData.onSound)
            audioSound.PlayOneShot(buttonBakteri);
    }

    public void PlayButtonKuman()
    {
        if (GameData.InstanceData.onSound)
            audioSound.PlayOneShot(buttonKuman);
    }

    public void PlayButtonOrganik()
    {
        if (GameData.InstanceData.onSound)
            audioSound.PlayOneShot(buttonOrganik);
    }

    public void PlayButtonAnorganik()
    {
        if (GameData.InstanceData.onSound)
            audioSound.PlayOneShot(buttonAnorganik);
    }
}
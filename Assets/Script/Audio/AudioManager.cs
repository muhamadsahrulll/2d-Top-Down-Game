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
    public AudioClip pickup;

    private Queue<AudioClip> audioQueue = new Queue<AudioClip>();
    private bool isPlaying = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        audioSound = GetComponent<AudioSource>();
        if (audioSound == null)
        {
            Debug.LogError("AudioSource component not found!");
        }
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

    private IEnumerator PlayQueuedSounds()
    {
        while (audioQueue.Count > 0)
        {
            AudioClip clip = audioQueue.Dequeue();
            audioSound.PlayOneShot(clip);
            Debug.Log($"Playing sound: {clip.name}");
            yield return new WaitForSeconds(clip.length);
        }
        isPlaying = false;
    }

    private void PlaySound(AudioClip clip)
    {
        if (GameData.InstanceData.onSound)
        {
            if (!isPlaying)
            {
                audioQueue.Enqueue(clip);
                isPlaying = true;
                StartCoroutine(PlayQueuedSounds());
            }
            else if (audioQueue.Count == 0)
            {
                audioQueue.Enqueue(clip);
            }
        }
    }

    public void PlayButtonBakteri()
    {
        PlaySound(buttonBakteri);
        Debug.Log("Button Bakteri clicked");
    }

    public void PlayButtonKuman()
    {
        PlaySound(buttonKuman);
        Debug.Log("Button Kuman clicked");
    }

    public void PlayButtonOrganik()
    {
        PlaySound(buttonOrganik);
        Debug.Log("Button Organik clicked");
    }

    public void PlayButtonAnorganik()
    {
        PlaySound(buttonAnorganik);
        Debug.Log("Button Anorganik clicked");
    }

    public void PlaySfxBenar()
    {
        PlaySound(sfxBenar);
        Debug.Log("SFX Benar played");
    }

    public void PlaySfxSalah()
    {
        PlaySound(sfxSalah);
        Debug.Log("SFX Salah played");
    }

    public void PlaySfxSelamat()
    {
        PlaySound(sfxSelamat);
        Debug.Log("SFX Selamat played");
    }

    public void PlayPickUp()
    {
        PlaySound(pickup);
    }
}
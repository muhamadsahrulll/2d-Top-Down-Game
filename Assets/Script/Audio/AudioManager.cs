using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] private string name = string.Empty;
    public string Name { get { return name; } }

    [SerializeField] private AudioClip clip = null;
    public AudioClip Clip { get { return clip; } }

    [HideInInspector]
    public AudioSource Source = null;

    public void Play()
    {
        if (Source != null && Clip != null)
        {
            Source.clip = Clip;
            Source.Play();
        }
    }

    public void Stop()
    {
        if (Source != null)
        {
            Source.Stop();
        }
    }
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource sourcePrefab = null;

    [Header("Lobby Music Settings")]
    [SerializeField] private Sound lobbyMusic = null;

    [Header("Level Music Settings")]
    [SerializeField] private Sound levelMusic = null;

    private bool isMusicOn = true;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitSound(lobbyMusic);
            InitSound(levelMusic);
        }
    }

    private void InitSound(Sound sound)
    {
        if (sound != null)
        {
            AudioSource source = Instantiate(sourcePrefab, gameObject.transform);
            source.name = sound.Name;
            sound.Source = source;
        }
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        SetMusicVolume();
    }

    private void SetMusicVolume()
    {
        if (lobbyMusic.Source != null)
        {
            lobbyMusic.Source.volume = isMusicOn ? 1f : 0f;
        }
        if (levelMusic.Source != null)
        {
            levelMusic.Source.volume = isMusicOn ? 1f : 0f;
        }
    }

    public void PlayLobbyMusic()
    {
        if (lobbyMusic != null)
        {
            levelMusic?.Stop();
            lobbyMusic.Play();
            SetMusicVolume();
        }
    }

    public void PlayLevelMusic()
    {
        if (levelMusic != null)
        {
            lobbyMusic?.Stop();
            levelMusic.Play();
            SetMusicVolume();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource backgroundAudioSource;  // Untuk musik latar
    public AudioSource sfxAudioSource;         // Untuk efek suara (SFX)

    public AudioClip backgroundMusic;          // Musik latar
    public AudioClip buttonBakteri;
    public AudioClip buttonKuman;
    public AudioClip buttonOrganik;
    public AudioClip buttonAnorganik;
    public AudioClip sfxBenar;
    public AudioClip sfxSalah;
    public AudioClip sfxSelamat;
    public AudioClip pickup;

    [Header("Suara Level")]
    public AudioClip level1;
    public AudioClip level2;
    public AudioClip level3;
    public AudioClip level4;
    public AudioClip level5;
    public AudioClip level6;
    public AudioClip Soal;

    [Header("sfx")]
    public AudioClip kalahbakteri;
    public AudioClip waktuhabis;
    public AudioClip salahpertanyaan;

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

        if (backgroundAudioSource == null || sfxAudioSource == null)
        {
            Debug.LogError("AudioSource components not found!");
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
            backgroundAudioSource.Stop();
        }
    }

    public void soundSettings(bool on)
    {
        if (on)
        {
            if (!backgroundAudioSource.isPlaying) // Pastikan hanya memulai kembali musik jika belum diputar
            {
                backgroundAudioSource.Play();
            }
            GameData.InstanceData.onSound = true;
        }
        else
        {
            backgroundAudioSource.Stop();
            GameData.InstanceData.onSound = false;
        }

        StartCoroutine(CheckSound());
    }

    private void PlaySound(AudioClip clip)
    {
        if (GameData.InstanceData.onSound && clip != null)
        {
            sfxAudioSource.Stop(); // Hentikan suara SFX yang sedang diputar
            sfxAudioSource.PlayOneShot(clip); // Mainkan suara baru
            Debug.Log($"Playing sound: {clip.name}");
        }
    }

    // Fungsi-fungsi untuk memutar SFX
    public void PlayButtonBakteri() { PlaySound(buttonBakteri); }
    public void PlayButtonKuman() { PlaySound(buttonKuman); }
    public void PlayButtonOrganik() { PlaySound(buttonOrganik); }
    public void PlayButtonAnorganik() { PlaySound(buttonAnorganik); }
    public void PlaySfxBenar() { PlaySound(sfxBenar); }
    public void PlaySfxSalah() { PlaySound(sfxSalah); }
    public void PlaySfxSelamat() { PlaySound(sfxSelamat); }
    public void PlayPickUp() { PlaySound(pickup); }
    public void PlayLevel1() { PlaySound(level1); }
    public void PlayLevel2() { PlaySound(level2); }
    public void PlayLevel3() { PlaySound(level3); }
    public void PlayLevel4() { PlaySound(level4); }
    public void PlayLevel5() { PlaySound(level5); }
    public void PlayLevel6() { PlaySound(level6); }
    public void PlaySoal() { PlaySound(Soal); }
    public void PlayWaktuHabis() { PlaySound(waktuhabis); }
    public void PlayKalahBakteri() { PlaySound(kalahbakteri); }
    public void PlaySoalSalah() { PlaySound(salahpertanyaan); }
    public void PlaySoundDialog(AudioClip clip) { PlaySound(clip); }
}

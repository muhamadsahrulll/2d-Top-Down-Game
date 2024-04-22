using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] public float timeRemaining = 60f;
    private bool timerIsRunning = false;
    public TextMeshProUGUI timerTxt;
    public GameObject player;
    public GameObject gameover;

    private void Start()
    {
        // Mulai timer saat game dimulai
        timerIsRunning = true;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Game Over!"); // Tampilkan pesan ketika waktu habis
                timeRemaining = 0;
                timerIsRunning = false;
                gameover.SetActive(true);
                player.SetActive(false);
            }
        }

        UpdateTimerUI();
    }

    public void PauseTimer()
    {
        timerIsRunning = false;
        player.gameObject.SetActive(false);
    }

    public void ResumeTimer()
    {
        timerIsRunning = true;
        player.gameObject.SetActive(true);
    }

    void UpdateTimerUI()
    {
        // Ubah nilai teks sesuai dengan nilai timeRemaining
        timerTxt.text = "Waktu: " + Mathf.Round(timeRemaining).ToString();
    }

    


}

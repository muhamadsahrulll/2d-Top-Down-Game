using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    public float timeRemaining = 60;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    public GameObject gameOverPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Starts the timer automatically
        //timerIsRunning = true;
        UpdateTimerText();
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                StopTimer();
                GameOver();
            }
        }
    }

    void UpdateTimerText()
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void GameOver()
    {
        // Tampilkan panel game over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Reset state misi di setiap GameManager
        ResetAllGameManagers();

        // Berikan kode tambahan jika ada yang ingin dilakukan ketika game over
        // Misalnya menghentikan pergerakan pemain, dll.
    }

    void ResetAllGameManagers()
    {
        // Dapatkan semua GameObject yang memiliki komponen GameManager
        GameManager gameManager = FindObjectOfType<GameManager>();
        InorganicManager inorganicManager = FindObjectOfType<InorganicManager>();
        QuizManager1 quizManager1 = FindObjectOfType<QuizManager1>();
        QuizManager2 quizManager2 = FindObjectOfType<QuizManager2>();
        RecycleManager recycleManager = FindObjectOfType<RecycleManager>();
        RecycleManager2 recycleManager2 = FindObjectOfType<RecycleManager2>();

        // Reset state misi masing-masing GameManager jika tidak null
        if (gameManager != null) gameManager.ResetProgress();
        if (inorganicManager != null) inorganicManager.ResetProgress();
        if (quizManager1 != null) quizManager1.ResetProgress();
        if (quizManager2 != null) quizManager2.ResetProgress();
        if (recycleManager != null) recycleManager.ResetProgress();
        if (recycleManager2 != null) recycleManager2.ResetProgress();
    }

    public void PauseTimer()
    {
        timerIsRunning = false;
    }

    public void StopTimer()
    {
        timeRemaining = 0;
        timerIsRunning = false;
        UpdateTimerText();
    }

    public void RestartTimer(float newTime)
    {
        timeRemaining = newTime;
        timerIsRunning = true;
    }

    public void StartTimer()
    {
        timerIsRunning = true;
    }
}

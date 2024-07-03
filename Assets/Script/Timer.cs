using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    [SerializeField] public float timeRemaining = 60f;
    private bool timerIsRunning = false;
    public TextMeshProUGUI timerTxt;
    public GameObject player;
    public GameObject gameover;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Uncomment if you need this
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        // Mulai timer saat game dimulai
        //timerIsRunning = true;
    }

    public void StartTimer()
    {
        timerIsRunning = true;
    }

    public void StopTimer()
    {
        timerIsRunning = false;
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

    public void Mulai()
    {
        timerIsRunning = true;
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

    public void Loadscene(string scene)
    {
        SceneManager.LoadScene(scene);
    }


}

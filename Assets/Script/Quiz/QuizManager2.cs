using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManager2 : MonoBehaviour
{
    public static QuizManager2 Instance;
    public Timer timer;

    // Variabel untuk sampah anorganik
    public int quiz2TrashCollected = 0;
    public int totalquiz2Trash = 5;
    public int quiz2TrashReward = 10;
    private int currentLevel = 4;

    public GameObject tutor1;
    public GameObject selamat;
    public GameObject player;
    public ParticleSystem finish1;
    public ParticleSystem finish2;

    public TextMeshProUGUI quiz2TrashCollectedText;
    public TextMeshProUGUI totalquiz2TrashText;
    public TextMeshProUGUI rewardText;

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
        LoadGameData();
        UpdateUIText();
        tutor1.SetActive(true);
    }

    private void Update()
    {
        UpdateUIText();
        SaveGameData();
    }

    public void CollectQuizTrash2()
    {
        quiz2TrashCollected++;
        PlayerPrefs.SetInt("QuizTrashCollected", quiz2TrashCollected);
        UpdateUIText();
        if (quiz2TrashCollected >= totalquiz2Trash)
        {
            Debug.Log("Quiz Trash Mission Completed");
            PlayerPrefs.SetInt("Quiz2TrashReward", quiz2TrashReward);
            selamat.SetActive(true);
            timer.PauseTimer();
            finish1.Play();
            finish2.Play();
            ScoreManager.Instance.AddOrganicScore(currentLevel, quiz2TrashReward);
            rewardText.text = "Selamat Anda Mendapatkan Score :" + quiz2TrashReward;
        }
    }

    public void LoadGameData()
    {
        quiz2TrashCollected = PlayerPrefs.GetInt("Quiz2TrashCollected", 0);
        UpdateUIText();
    }

    public void SaveGameData()
    {
        PlayerPrefs.SetInt("Quiz2TrashCollected", quiz2TrashCollected);
    }

    private void UpdateUIText()
    {
        quiz2TrashCollectedText.text = "Quiz Trash Collected: " + quiz2TrashCollected + "/" + totalquiz2Trash;
        totalquiz2TrashText.text = "Total Quiz Trash: " + totalquiz2Trash;
    }

    private void RestartGame()
    {
        // Reset nilai-nilai yang perlu di-reset
        quiz2TrashCollected = 0;
        PlayerPrefs.SetInt("Quiz2TrashCollected", quiz2TrashCollected);

        // Restart timer
        FindObjectOfType<Timer>().timeRemaining = 60f;

        // Restart scene atau lakukan langkah-langkah lain yang diperlukan untuk memulai ulang game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        player.SetActive(true);
    }

    public void Restartgame()
    {
        RestartGame();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManager1 : MonoBehaviour
{
    public static QuizManager1 Instance;
    public Timer timer;

    // Variabel untuk sampah anorganik
    public int quizTrashCollected = 0;
    public int totalquizTrash = 4;
    public int quizTrashReward = 10;
    public int reward75 = 75;
    public int reward50 = 50;
    public int reward25 = 25;
    private int currentLevel = 3;

    public int totalQuestionsAnswered = 0; // Total pertanyaan yang telah dijawab
    public int totalWrongAnswers = 0; // Total jawaban yang salah

    [Header("MENANG")]
    public GameObject tutor1;
    public GameObject selamat;
    public GameObject selamat75;
    public GameObject Kalah;
    public GameObject KalahSoal;
    public GameObject player;
    public ParticleSystem finish1;
    public ParticleSystem finish2;

    public TextMeshProUGUI quizTrashCollectedText;
    public TextMeshProUGUI totalquizTrashText;
    public TextMeshProUGUI rewardText;
    public TextMeshProUGUI kalahreward;

    public GameObject jawabanBenar;
    public GameObject jawabanSalah;

    public WeaponInfo weaponInfo1;
    public WeaponInfo[] allWeaponInfos; // Tambahkan ini di inspector, isi dengan semua ScriptableObject senjata yang ada

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        AudioManager.instance.PlayLevel3();
        weaponInfo1.isPurchased = true;
        ScoreManager.Instance.allWeaponInfos = new List<WeaponInfo>(allWeaponInfos);

        foreach (WeaponInfo weaponInfo in allWeaponInfos)
        {
            ScoreManager.Instance.LoadWeaponPurchase(weaponInfo, 2);
            ScoreManager.Instance.LoadWeaponPurchase(weaponInfo, 3);
        }
    }

    private void Update()
    {
        UpdateUIText();
        weaponInfo1.isPurchased = true;
    }

    public void CollectQuizTrash()
    {
        quizTrashCollected++;
        totalQuestionsAnswered++;
        PlayerPrefs.SetInt("QuizTrashCollected_Level3", quizTrashCollected);
        UpdateUIText();
        CheckQuizCompletion();
    }

    public void AnswerWrong()
    {
        totalQuestionsAnswered++;
        totalWrongAnswers++;
        UpdateUIText();
        CheckQuizCompletion();
    }

    public void CheckQuizCompletion()
    {
        if (totalQuestionsAnswered >= totalquizTrash)
        {
            if (totalWrongAnswers == 1)
            {
                // Menang dengan satu jawaban salah
                PlayerPrefs.SetInt("QuizTrashReward_Level3", reward75);
                selamat.SetActive(true);
                player.SetActive(false);
                timer.PauseTimer();
                finish1.Play();
                finish2.Play();
                ScoreManager.Instance.AddOrganicScore(currentLevel, reward75);
                ScoreManager.Instance.AddSenjataKoin(reward75);
                rewardText.text = "Selamat Anda Mendapatkan Score: " + reward75;
                AudioManager.instance.PlaySfxSelamat();
            }
            else if (totalWrongAnswers > 1)
            {
                KalahSoal.SetActive(true);
                AudioManager.instance.PlaySoalSalah();
                kalahreward.text = "Skor Kamu : 0";
                timer.PauseTimer();

                // Kondisi tambahan untuk memberikan reward berdasarkan jumlah jawaban salah
                if (totalWrongAnswers == 2)
                {
                    kalahreward.text = "Skor Kamu : " + reward50;
                    ScoreManager.Instance.AddOrganicScore(currentLevel, reward50);
                    ScoreManager.Instance.AddSenjataKoin(reward50);
                    Debug.Log("Score Anda: " + reward50);
                }
                else if (totalWrongAnswers == 3)
                {
                    kalahreward.text = "Skor Kamu : " + reward25;
                    ScoreManager.Instance.AddOrganicScore(currentLevel, reward25);
                    ScoreManager.Instance.AddSenjataKoin(reward25);
                    Debug.Log("Score Anda: " + reward25);
                }
            }
            else
            {
                // Menang tanpa jawaban salah
                PlayerPrefs.SetInt("QuizTrashReward_Level3", quizTrashReward);
                selamat.SetActive(true);
                player.SetActive(false);
                timer.PauseTimer();
                finish1.Play();
                finish2.Play();
                ScoreManager.Instance.AddOrganicScore(currentLevel, quizTrashReward);
                ScoreManager.Instance.AddSenjataKoin(quizTrashReward);
                rewardText.text = "Selamat Anda Mendapatkan Score: " + quizTrashReward;
                AudioManager.instance.PlaySfxSelamat();
            }
        }
    }

    public void LoadGameData()
    {
        quizTrashCollected = PlayerPrefs.GetInt("QuizTrashCollected_Level3", 0);
        totalQuestionsAnswered = PlayerPrefs.GetInt("TotalQuestionsAnswered_Level3", 0);
        totalWrongAnswers = PlayerPrefs.GetInt("TotalWrongAnswers_Level3", 0);
        UpdateUIText();
    }

    public void SaveGameData()
    {
        PlayerPrefs.SetInt("QuizTrashCollected_Level3", quizTrashCollected);
        PlayerPrefs.SetInt("TotalQuestionsAnswered_Level3", totalQuestionsAnswered);
        PlayerPrefs.SetInt("TotalWrongAnswers_Level3", totalWrongAnswers);
    }

    private void UpdateUIText()
    {
        quizTrashCollectedText.text = "Sampah Terkumpul : " + quizTrashCollected;
        totalquizTrashText.text = "Total Sampah : " + totalquizTrash;
    }

    private void RestartGame()
    {
        quizTrashCollected = 0;
        totalQuestionsAnswered = 0;
        totalWrongAnswers = 0;
        PlayerPrefs.SetInt("QuizTrashCollected_Level3", quizTrashCollected);
        PlayerPrefs.SetInt("TotalQuestionsAnswered_Level3", totalQuestionsAnswered);
        PlayerPrefs.SetInt("TotalWrongAnswers_Level3", totalWrongAnswers);

        FindObjectOfType<Timer>().timeRemaining = 60f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        player.SetActive(true);
    }

    public void Restartgame()
    {
        RestartGame();
    }

    public void PlayerDied()
    {
        PlayerPrefs.SetInt("QuizTrashCollected_Level3", 0);
        PlayerPrefs.SetInt("QuizTrashReward_Level3", 0);
        Timer.Instance.StopTimer();
        Kalah.SetActive(true);
        AudioManager.instance.PlayKalahBakteri();
    }

    public void Keluargame()
    {
        PlayerPrefs.SetInt("QuizTrashCollected_Level3", 0);
        PlayerPrefs.SetInt("QuizTrashReward_Level3", 0);
        ResetProgress();
    }

    public IEnumerator jawabanB(float seconds)
    {
        jawabanBenar.gameObject.SetActive(true);
        yield return new WaitForSeconds(seconds);
        jawabanBenar.gameObject.SetActive(false);
    }

    public IEnumerator jawabanS(float seconds)
    {
        jawabanSalah.gameObject.SetActive(true);
        yield return new WaitForSeconds(seconds);
        jawabanSalah.gameObject.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        ResetProgress();
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("QuizTrashCollected_Level3", 0);
        PlayerPrefs.SetInt("QuizTrashReward_Level3", 0);
        PlayerPrefs.SetInt("TotalQuestionsAnswered_Level3", 0);
        PlayerPrefs.SetInt("TotalWrongAnswers_Level3", 0);
    }
}

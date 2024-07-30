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
    public int totalquiz2Trash = 4;
    public int quiz2TrashReward = 10;
    private int currentLevel = 4;
    public int reward75 = 75;
    public int reward50 = 50;
    public int reward25 = 25;

    public int totalQuestionsAnswered = 0; // Total pertanyaan yang telah dijawab
    public int totalWrongAnswers = 0; // Total jawaban yang salah

    [Header("MENANG")]
    public GameObject tutor1;
    public GameObject selamat;
    public GameObject Kalah;
    public GameObject KalahSoal;
    public GameObject player;
    public ParticleSystem finish1;
    public ParticleSystem finish2;

    public TextMeshProUGUI quiz2TrashCollectedText;
    public TextMeshProUGUI totalquiz2TrashText;
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
        AudioManager.instance.PlayLevel4();
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

    public void CollectQuizTrash2()
    {
        quiz2TrashCollected++;
        totalQuestionsAnswered++;
        PlayerPrefs.SetInt("Quiz2TrashCollected_Level4", quiz2TrashCollected);
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
        if (totalQuestionsAnswered >= totalquiz2Trash)
        {
            if (totalWrongAnswers == 1)
            {
                // Menang dengan satu jawaban salah
                PlayerPrefs.SetInt("QuizTrashReward_Level4", reward75);
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
                PlayerPrefs.SetInt("QuizTrashReward_Level4", quiz2TrashReward);
                selamat.SetActive(true);
                player.SetActive(false);
                timer.PauseTimer();
                finish1.Play();
                finish2.Play();
                ScoreManager.Instance.AddOrganicScore(currentLevel, quiz2TrashReward);
                ScoreManager.Instance.AddSenjataKoin(quiz2TrashReward);
                rewardText.text = "Selamat Anda Mendapatkan Score: " + quiz2TrashReward;
                AudioManager.instance.PlaySfxSelamat();
            }
        }
    }

    public void LoadGameData()
    {
        quiz2TrashCollected = PlayerPrefs.GetInt("Quiz2TrashCollected_Level4", 0);
        totalQuestionsAnswered = PlayerPrefs.GetInt("TotalQuestionsAnswered_Level4", 0);
        totalWrongAnswers = PlayerPrefs.GetInt("TotalWrongAnswers_Level4", 0);
        UpdateUIText();
    }

    public void SaveGameData()
    {
        PlayerPrefs.SetInt("Quiz2TrashCollected_Level4", quiz2TrashCollected);
        PlayerPrefs.SetInt("TotalQuestionsAnswered_Level4", totalQuestionsAnswered);
        PlayerPrefs.SetInt("TotalWrongAnswers_Level4", totalWrongAnswers);
    }

    private void UpdateUIText()
    {
        quiz2TrashCollectedText.text = "Sampah Terkumpul : " + quiz2TrashCollected;
        totalquiz2TrashText.text = "Total Sampah : " + totalquiz2Trash;
    }

    private void RestartGame()
    {
        quiz2TrashCollected = 0;
        totalQuestionsAnswered = 0;
        totalWrongAnswers = 0;
        PlayerPrefs.SetInt("Quiz2TrashCollected_Level4", quiz2TrashCollected);
        PlayerPrefs.SetInt("TotalQuestionsAnswered_Level4", totalQuestionsAnswered);
        PlayerPrefs.SetInt("TotalWrongAnswers_Level4", totalWrongAnswers);

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
        PlayerPrefs.SetInt("Quiz2TrashCollected_Level4", 0);
        PlayerPrefs.SetInt("Quiz2TrashReward_Level4", 0);
        Timer.Instance.StopTimer();
        Kalah.SetActive(true);
        AudioManager.instance.PlayKalahBakteri();
    }

    public void Keluargame()
    {
        PlayerPrefs.SetInt("Quiz2TrashCollected_Level4", 0);
        PlayerPrefs.SetInt("Quiz2TrashReward_Level4", 0);
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
        PlayerPrefs.SetInt("Quiz2TrashCollected_Level4", 0);
        PlayerPrefs.SetInt("Quiz2TrashReward_Level4", 0);
        PlayerPrefs.SetInt("TotalQuestionsAnswered_Level4", 0);
        PlayerPrefs.SetInt("TotalWrongAnswers_Level4", 0);
    }
}

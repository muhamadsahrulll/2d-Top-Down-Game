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
    private int currentLevel = 3;

    public int totalQuestionsAnswered = 0; // Tambahan variabel untuk melacak total pertanyaan yang telah dijawab

    public GameObject tutor1;
    public GameObject selamat;
    public GameObject Kalah;
    public GameObject KalahSoal;
    public GameObject player;
    public ParticleSystem finish1;
    public ParticleSystem finish2;

    public TextMeshProUGUI quizTrashCollectedText;
    public TextMeshProUGUI totalquizTrashText;
    public TextMeshProUGUI rewardText;

    public GameObject jawabanBenar;
    public GameObject jawabanSalah;

    public WeaponInfo weaponInfo1;
    public WeaponInfo[] allWeaponInfos; // Tambahkan ini di inspector, isi dengan semua ScriptableObject senjata yang ada

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
        AudioManager.instance.PlayLevel3();
        weaponInfo1.isPurchased = true;
        // Inisialisasi allWeaponInfos di ScoreManager
        ScoreManager.Instance.allWeaponInfos = new List<WeaponInfo>(allWeaponInfos);

        // Memuat status pembelian senjata saat game dimulai
        foreach (WeaponInfo weaponInfo in allWeaponInfos)
        {
            //ScoreManager.Instance.LoadWeaponPurchase(weaponInfo, 1);
            ScoreManager.Instance.LoadWeaponPurchase(weaponInfo, 2);
            ScoreManager.Instance.LoadWeaponPurchase(weaponInfo, 3);
        }
    }

    private void Update()
    {
        UpdateUIText();
        //SaveGameData();
        weaponInfo1.isPurchased = true;
    }

    public void CollectQuizTrash()
    {
        quizTrashCollected++;
        totalQuestionsAnswered++;
        PlayerPrefs.SetInt("QuizTrashCollected", quizTrashCollected);
        UpdateUIText();
        CheckQuizCompletion();
    }

    public void CheckQuizCompletion()
    {
        if (totalQuestionsAnswered >= totalquizTrash)
        {
            if (quizTrashCollected >= totalquizTrash)
            {
                Debug.Log("Quiz Trash Mission Completed");
                PlayerPrefs.SetInt("QuizTrashReward", quizTrashReward);
                selamat.SetActive(true);
                timer.PauseTimer();
                finish1.Play();
                finish2.Play();
                ScoreManager.Instance.AddOrganicScore(currentLevel, quizTrashReward);
                rewardText.text = "Selamat Anda Mendapatkan Score :" + quizTrashReward;
                AudioManager.instance.PlaySfxSelamat();
            }
            else
            {
                Debug.Log("Kamu belum berhasil menjawab pertanyaan dengan benar");
                

                // Cek apakah ada yang belum dijawab dengan benar
                bool allAnsweredCorrectly = quizTrashCollected >= totalquizTrash;
                if (!allAnsweredCorrectly)
                {
                    //Kalah.SetActive(true);
                    KalahSoal.SetActive(true);
                    timer.PauseTimer();
                    // Tambahkan logika untuk game over di sini
                }
            }
        }
    }

    public void LoadGameData()
    {
        quizTrashCollected = PlayerPrefs.GetInt("QuizTrashCollected", 0);
        totalQuestionsAnswered = PlayerPrefs.GetInt("TotalQuestionsAnswered", 0);
        UpdateUIText();
    }

    public void SaveGameData()
    {
        PlayerPrefs.SetInt("QuizTrashCollected", quizTrashCollected);
        PlayerPrefs.SetInt("TotalQuestionsAnswered", totalQuestionsAnswered);
    }

    private void UpdateUIText()
    {
        quizTrashCollectedText.text = "Sampah Terkumpul : " + quizTrashCollected;
        totalquizTrashText.text = "Total Sampah : " + totalquizTrash;
    }

    private void RestartGame()
    {
        // Reset nilai-nilai yang perlu di-reset
        quizTrashCollected = 0;
        totalQuestionsAnswered = 0;
        PlayerPrefs.SetInt("QuizTrashCollected", quizTrashCollected);
        PlayerPrefs.SetInt("TotalQuestionsAnswered", totalQuestionsAnswered);

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

    public void PlayerDied()
    {
        // Tambahkan kode untuk menampilkan image dari canvas
        Debug.Log("Player mati di level 3");
        PlayerPrefs.SetInt("QuizTrashCollected1", 0);
        PlayerPrefs.SetInt("QuizTrashReward1", 0);
        Timer.Instance.StopTimer();
        Kalah.SetActive(true); // Aktifkan image game over
    }

    public void Keluargame()
    {
        PlayerPrefs.SetInt("QuizTrashCollected1", 0);
        PlayerPrefs.SetInt("QuizTrashReward1", 0);
        // Tambahkan kode untuk keluar dari game
        ResetProgress();
        Debug.Log("Keluar game dari level 3");
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
        PlayerPrefs.SetInt("QuizTrashCollected1", 0);
        PlayerPrefs.SetInt("QuizTrashReward1", 0);
        PlayerPrefs.SetInt("TotalQuestionsAnswered", 0);
        Debug.Log("Progress direset karena aplikasi ditutup atau dijeda.");
    }
}

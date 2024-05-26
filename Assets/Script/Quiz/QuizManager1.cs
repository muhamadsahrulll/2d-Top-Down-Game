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
    public int totalquizTrash = 5;
    public int quizTrashReward = 10;
    private int currentLevel = 3;

    public GameObject tutor1;
    public GameObject selamat;
    public GameObject player;
    public ParticleSystem finish1;
    public ParticleSystem finish2;

    public TextMeshProUGUI quizTrashCollectedText;
    public TextMeshProUGUI totalquizTrashText;
    public TextMeshProUGUI rewardText;

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
        SaveGameData();
        weaponInfo1.isPurchased = true;
    }

    public void CollectQuizTrash()
    {
        quizTrashCollected++;
        PlayerPrefs.SetInt("QuizTrashCollected", quizTrashCollected);
        UpdateUIText();
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
        }
    }

    public void LoadGameData()
    {
        quizTrashCollected = PlayerPrefs.GetInt("QuizTrashCollected", 0);
        UpdateUIText();
    }

    public void SaveGameData()
    {
        PlayerPrefs.SetInt("QuizTrashCollected", quizTrashCollected);
    }

    private void UpdateUIText()
    {
        quizTrashCollectedText.text = "Quiz Trash Collected: " + quizTrashCollected + "/" + totalquizTrash;
        totalquizTrashText.text = "Total Quiz Trash: " + totalquizTrash;
    }

    private void RestartGame()
    {
        // Reset nilai-nilai yang perlu di-reset
        quizTrashCollected = 0;
        PlayerPrefs.SetInt("QuizTrashCollected", quizTrashCollected);

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

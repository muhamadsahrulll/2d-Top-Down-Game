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
    public GameObject Kalah;
    public GameObject player;
    public ParticleSystem finish1;
    public ParticleSystem finish2;

    public TextMeshProUGUI quiz2TrashCollectedText;
    public TextMeshProUGUI totalquiz2TrashText;
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
            AudioManager.instance.PlaySfxSelamat();
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
        quiz2TrashCollectedText.text = "Sampah Terkumpul : " + quiz2TrashCollected + "/" + totalquiz2Trash;
        totalquiz2TrashText.text = "Total Sampah: " + totalquiz2Trash;
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

    public void PlayerDied()
    {
        // Tambahkan kode untuk menampilkan image dari canvas
        Debug.Log("Player mati di level 4");
        Kalah.SetActive(true); // Aktifkan image game over
    }

    public void Keluargame()
    {
        PlayerPrefs.SetInt("QuizTrashCollected2", 0);
        PlayerPrefs.SetInt("QuizTrashReward2", 0);
        // Tambahkan kode untuk keluar dari game
        Debug.Log("Keluar game dari level 4");
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

    private void ResetProgress()
    {
        PlayerPrefs.SetInt("QuizTrashCollected2", 0);
        PlayerPrefs.SetInt("QuizTrashReward2", 0);
        Debug.Log("Progress direset karena aplikasi ditutup atau dijeda.");
    }
}

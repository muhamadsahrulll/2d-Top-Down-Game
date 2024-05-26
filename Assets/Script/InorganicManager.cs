using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InorganicManager : MonoBehaviour
{
    public static InorganicManager Instance;
    public Timer timer;

    // Variabel untuk sampah anorganik
    public int inorganicTrashCollected = 0;
    public int totalInorganicTrash = 5;
    public int inorganicTrashReward = 100;
    private int currentLevel = 2;

    public GameObject tutor1;
    public GameObject selamat;
    public GameObject player;
    public ParticleSystem finish1;
    public ParticleSystem finish2;
    public TextMeshProUGUI rewardText;

    public TextMeshProUGUI inorganicTrashCollectedText;
    public TextMeshProUGUI totalInorganicTrashText;

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
        weaponInfo1.isPurchased = true;
        UpdateUIText();
        SaveGameData();
    }

    public void CollectInorganicTrash()
    {
        inorganicTrashCollected++;
        PlayerPrefs.SetInt("InorganicTrashCollected", inorganicTrashCollected);
        UpdateUIText();
        if (inorganicTrashCollected >= totalInorganicTrash)
        {
            Debug.Log("Inorganic Trash Mission Completed");
            PlayerPrefs.SetInt("InorganicTrashReward", inorganicTrashReward);
            selamat.SetActive(true);
            timer.PauseTimer();
            finish1.Play();
            finish2.Play();
            ScoreManager.Instance.AddOrganicScore(currentLevel, inorganicTrashReward); // Menambah skor organik pada level tertentu
            rewardText.text = "Selamat Anda Mendapatkan Score : " + inorganicTrashReward;
        }
    }

    public void LoadGameData()
    {
        inorganicTrashCollected = PlayerPrefs.GetInt("InorganicTrashCollected", 0);
        UpdateUIText();
    }

    public void SaveGameData()
    {
        PlayerPrefs.SetInt("InorganicTrashCollected", inorganicTrashCollected);
    }

    private void UpdateUIText()
    {
        inorganicTrashCollectedText.text = "Inorganic Trash Collected: " + inorganicTrashCollected + "/" + totalInorganicTrash;
        totalInorganicTrashText.text = "Total Inorganic Trash: " + totalInorganicTrash;
    }

    private void RestartGame()
    {
        // Reset nilai-nilai yang perlu di-reset
        inorganicTrashCollected = 0;
        PlayerPrefs.SetInt("InorganicTrashCollected", inorganicTrashCollected);

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

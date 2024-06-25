using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RecycleManager2 : MonoBehaviour
{
    public static RecycleManager2 Instance;
    public Timer timer;

    public int recycleTrashCollected = 0;
    public int totalRecycleTrash = 5;
    public int recycleTrashReward = 100;
    private int currentLevel = 6;

    public GameObject tutor1;
    public GameObject selamat;
    public GameObject Kalah;
    public GameObject player;
    public ParticleSystem finish1;
    public ParticleSystem finish2;
    public TextMeshProUGUI rewardText;

    [Header("Sampah Reference")]
    public TextMeshProUGUI botol;
    private int totalBotol = 2;
    public int sampahBotol;
    public TextMeshProUGUI guting;
    public int sampahGunting;
    private int totalGuting = 1;
    public TextMeshProUGUI bunga;
    public int sampahBunga;
    private int totalBunga = 2;
    public GameObject notComplete;

    [Header("Weapon")]
    public WeaponInfo weaponInfo1;
    public WeaponInfo[] allWeaponInfos;

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
        ScoreManager.Instance.allWeaponInfos = new List<WeaponInfo>(allWeaponInfos);
        foreach (WeaponInfo weaponInfo in allWeaponInfos)
        {
            ScoreManager.Instance.LoadWeaponPurchase(weaponInfo, 1);
            ScoreManager.Instance.LoadWeaponPurchase(weaponInfo, 2);
            ScoreManager.Instance.LoadWeaponPurchase(weaponInfo, 3);
        }
    }

    private void Update()
    {
        UpdateUIText();
    }

    public void CollectRecycleTrash(string category)
    {
        recycleTrashCollected++;
        PlayerPrefs.SetInt("RecycleTrashCollected", recycleTrashCollected);

        switch (category)
        {
            case "BotolDR":
                sampahBotol++;
                break;
            case "Gunting":
                sampahGunting++;
                break;
            case "Bunga":
                sampahBunga++;
                break;
            default:
                Debug.LogWarning("Kategori sampah tidak dikenal: " + category);
                break;
        }

        UpdateUIText();
        SaveGameData();
    }

    public void CraftRecycle()
    {
        if (recycleTrashCollected >= totalRecycleTrash)
        {
            Debug.Log("Recycle Mission Completed");
            PlayerPrefs.SetInt("RecycleTrashReward", recycleTrashReward);
            PlayerPrefs.SetInt("RecycleMissionCompleted", 1);
            selamat.SetActive(true);
            timer.PauseTimer();
            finish1.Play();
            finish2.Play();
            ScoreManager.Instance.AddOrganicScore(currentLevel, recycleTrashReward);
            rewardText.text = "Selamat Anda Mendapatkan Score : " + recycleTrashReward;
            AudioManager.instance.PlaySfxSelamat();
            SaveGameData();
        }
        else
        {
            StartCoroutine(NotCompleted(2.0f));
            Debug.Log("Recycle Mission Not Completed");
        }
    }

    public void PlayerDied()
    {
        Debug.Log("Player mati di level 2");
        PlayerPrefs.SetInt("RecycleTrashCollected", 0);
        PlayerPrefs.SetInt("SampahPlastik", 0);
        PlayerPrefs.SetInt("SampahGunting", 0);
        PlayerPrefs.SetInt("SampahTali", 0);
        PlayerPrefs.SetInt("RecycleTrashReward", 0);
        ResetProgress();
        Kalah.SetActive(true);
    }

    public void LoadGameData()
    {
        recycleTrashCollected = PlayerPrefs.GetInt("RecycleTrashCollected");
        sampahBotol = PlayerPrefs.GetInt("SampahBotol");
        sampahGunting = PlayerPrefs.GetInt("SampahGunting");
        sampahBunga = PlayerPrefs.GetInt("SampahBunga");
        UpdateUIText();
    }

    public void SaveGameData()
    {
        PlayerPrefs.SetInt("RecycleTrashCollected", recycleTrashCollected);
        PlayerPrefs.SetInt("SampahBotol", sampahBotol);
        PlayerPrefs.SetInt("SampahGunting", sampahGunting);
        PlayerPrefs.SetInt("SampahBunga", sampahBunga);
    }

    private void UpdateUIText()
    {
        botol.text = "Plastik : " + sampahBotol + "/" + totalBotol;
        bunga.text = "Tali : " + sampahBunga + "/" + totalBunga;
        guting.text = "Gunting : " + sampahGunting + "/" + totalGuting;
    }

    private void RestartGame()
    {
        recycleTrashCollected = 0;
        PlayerPrefs.SetInt("RecycleTrashCollected", recycleTrashCollected);
        FindObjectOfType<Timer>().timeRemaining = 60f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        player.SetActive(true);
    }

    public void Restartgame()
    {
        RestartGame();
    }

    public void Keluargame()
    {
        PlayerPrefs.SetInt("RecycleTrashCollected", 0);
        PlayerPrefs.SetInt("SampahPlastik", 0);
        PlayerPrefs.SetInt("SampahGunting", 0);
        PlayerPrefs.SetInt("SampahTali", 0);
        PlayerPrefs.SetInt("RecycleTrashReward", 0);
        ResetProgress();
        Debug.Log("Keluar game dari level 2");
    }

    private void OnApplicationQuit()
    {
        ResetProgress();
    }

    private void ResetProgress()
    {
        PlayerPrefs.SetInt("RecycleTrashCollected", 0);
        PlayerPrefs.SetInt("SampahBotol", 0);
        PlayerPrefs.SetInt("SampahGunting", 0);
        PlayerPrefs.SetInt("SampahBunga", 0);
        PlayerPrefs.SetInt("RecycleTrashReward", 0);
        Debug.Log("Progress direset karena aplikasi ditutup atau dijeda.");
    }


    public void Crafting()
    {
        CraftRecycle();
    }

    IEnumerator NotCompleted(float seconds)
    {
        notComplete.SetActive(true);
        yield return new WaitForSeconds(seconds);
        notComplete.SetActive(false);
    }
}

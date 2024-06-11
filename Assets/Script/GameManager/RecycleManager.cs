using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RecycleManager : MonoBehaviour
{
    public static RecycleManager Instance;
    public Timer timer;

    public int recycleTrashCollected = 0;
    public int totalRecycleTrash = 5;
    public int recycleTrashReward = 100;
    private int currentLevel = 5;

    public GameObject tutor1;
    public GameObject selamat;
    public GameObject Kalah;
    public GameObject player;
    public ParticleSystem finish1;
    public ParticleSystem finish2;
    public TextMeshProUGUI rewardText;

    [Header("Sampah Reference")]
    public TextMeshProUGUI plastik;
    private int totalPlastik = 3;
    public int sampahPlastik;
    public TextMeshProUGUI guting;
    public int sampahGunting;
    private int totalGuting = 1;
    public TextMeshProUGUI tali;
    public int sampahTali;
    private int totalTali = 1;
    public GameObject notComplete;

    [Header("Weapon")]
    public WeaponInfo weaponInfo1;
    public WeaponInfo[] allWeaponInfos;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
            case "Plastik":
                sampahPlastik++;
                break;
            case "Gunting":
                sampahGunting++;
                break;
            case "Tali":
                sampahTali++;
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
        Kalah.SetActive(true);
    }

    public void LoadGameData()
    {
        recycleTrashCollected = PlayerPrefs.GetInt("RecycleTrashCollected");
        sampahPlastik = PlayerPrefs.GetInt("SampahPlastik");
        sampahGunting = PlayerPrefs.GetInt("SampahGunting");
        sampahTali = PlayerPrefs.GetInt("SampahTali");
        UpdateUIText();
    }

    public void SaveGameData()
    {
        PlayerPrefs.SetInt("RecycleTrashCollected", recycleTrashCollected);
        PlayerPrefs.SetInt("SampahPlastik", sampahPlastik);
        PlayerPrefs.SetInt("SampahGunting", sampahGunting);
        PlayerPrefs.SetInt("SampahTali", sampahTali);
    }

    private void UpdateUIText()
    {
        plastik.text = "Plastik : " + sampahPlastik + "/" + totalPlastik;
        tali.text = "Tali : " + sampahTali + "/" + totalTali;
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
        Debug.Log("Keluar game dari level 2");
    }

    private void OnApplicationQuit()
    {
        ResetProgress();
    }

    private void ResetProgress()
    {
        PlayerPrefs.SetInt("RecycleTrashCollected", 0);
        PlayerPrefs.SetInt("SampahPlastik", 0);
        PlayerPrefs.SetInt("SampahGunting", 0);
        PlayerPrefs.SetInt("SampahTali", 0);
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

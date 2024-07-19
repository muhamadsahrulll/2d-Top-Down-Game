using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    // Variabel untuk menyimpan skor organik tiap level
    public int level1OrganicScore;
    public int level2OrganicScore;
    public int level3OrganicScore;
    public int level4OrganicScore;
    public int level5OrganicScore;
    public int level6OrganicScore;

    // Variabel untuk menyimpan skor organik keseluruhan
    public int totalOrganicScore;
    public int senjataKoin; // Variabel untuk koin senjata, tidak sama dengan totalOrganicScore

    // Variabel untuk menyimpan referensi ke semua senjata
    public List<WeaponInfo> allWeaponInfos = new List<WeaponInfo>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // Jika perlu agar objek tidak dihancurkan saat pindah scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadScores();
        CalculateTotalOrganicScore();
        LoadWeaponPurchases();
        LoadSenjataKoin();
    }

    private void Update()
    {
        SaveScores();
        SaveWeaponPurchases();
        SaveSenjataKoin();
    }

    // Fungsi untuk menambah skor organik pada level tertentu
    public void AddOrganicScore(int level, int score)
    {
        switch (level)
        {
            case 1:
                level1OrganicScore += score;
                break;
            case 2:
                level2OrganicScore += score;
                break;
            case 3:
                level3OrganicScore += score;
                break;
            case 4:
                level4OrganicScore += score;
                break;
            case 5:
                level5OrganicScore += score;
                break;
            case 6:
                level6OrganicScore += score;
                break;
            default:
                Debug.LogWarning("Level tidak valid.");
                break;
        }
        SaveScores();
        CalculateTotalOrganicScore();
    }

    // Fungsi untuk menambah koin senjata
    public void AddSenjataKoin(int koin)
    {
        senjataKoin += koin;
        SaveSenjataKoin();
    }

    private void SaveScores()
    {
        PlayerPrefs.SetInt("Level1OrganicScore", level1OrganicScore);
        PlayerPrefs.SetInt("Level2OrganicScore", level2OrganicScore);
        PlayerPrefs.SetInt("Level3OrganicScore", level3OrganicScore);
        PlayerPrefs.SetInt("Level4OrganicScore", level4OrganicScore);
        PlayerPrefs.SetInt("Level5OrganicScore", level5OrganicScore);
        PlayerPrefs.SetInt("Level6OrganicScore", level6OrganicScore);
    }

    private void LoadScores()
    {
        level1OrganicScore = PlayerPrefs.GetInt("Level1OrganicScore");
        level2OrganicScore = PlayerPrefs.GetInt("Level2OrganicScore");
        level3OrganicScore = PlayerPrefs.GetInt("Level3OrganicScore");
        level4OrganicScore = PlayerPrefs.GetInt("Level4OrganicScore");
        level5OrganicScore = PlayerPrefs.GetInt("Level5OrganicScore");
        level6OrganicScore = PlayerPrefs.GetInt("Level6OrganicScore");
    }

    private void CalculateTotalOrganicScore()
    {
        totalOrganicScore = level1OrganicScore + level2OrganicScore + level3OrganicScore + level4OrganicScore + level5OrganicScore + level6OrganicScore;
    }

    private void SaveSenjataKoin()
    {
        PlayerPrefs.SetInt("SenjataKoin", senjataKoin);
    }

    private void LoadSenjataKoin()
    {
        senjataKoin = PlayerPrefs.GetInt("SenjataKoin", 0);
    }

    // Fungsi beli senjata
    public void BuyWeapon1(WeaponInfo weaponInfo)
    {
        weaponInfo.isPurchased = true;
        SaveWeaponPurchase(weaponInfo, 1);
        StartCoroutine(WeaponShop.Instance.notifBerhasil(2.0f));
    }

    public void BuyWeapon2(WeaponInfo weaponInfo)
    {
        if (senjataKoin >= 100 && !weaponInfo.isPurchased)
        {
            weaponInfo.isPurchased = true;
            SaveWeaponPurchase(weaponInfo, 2);
            senjataKoin -= weaponInfo.cost; // Kurangi senjataKoin saat membeli senjata
            StartCoroutine(WeaponShop.Instance.notifBerhasil(2.0f));
        }
        else
        {
            Debug.LogWarning("Skor tidak mencukupi atau senjata sudah dibeli.");
            StartCoroutine(WeaponShop.Instance.notifGagal2(2.0f));
        }
    }

    public void BuyWeapon3(WeaponInfo weaponInfo)
    {
        if (senjataKoin >= 200 && !weaponInfo.isPurchased)
        {
            weaponInfo.isPurchased = true;
            SaveWeaponPurchase(weaponInfo, 3);
            senjataKoin -= weaponInfo.cost; // Kurangi senjataKoin saat membeli senjata
            StartCoroutine(WeaponShop.Instance.notifBerhasil(2.0f));
        }
        else
        {
            Debug.LogWarning("Skor tidak mencukupi atau senjata sudah dibeli.");
            StartCoroutine(WeaponShop.Instance.notifGagal2(2.0f));
        }
    }

    private void SaveWeaponPurchase(WeaponInfo weaponInfo, int weaponIndex)
    {
        PlayerPrefs.SetInt(weaponInfo.name + "_Purchased" + weaponIndex, weaponInfo.isPurchased ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void LoadWeaponPurchase(WeaponInfo weaponInfo, int weaponIndex)
    {
        weaponInfo.isPurchased = PlayerPrefs.GetInt(weaponInfo.name + "_Purchased" + weaponIndex, 0) == 1;
    }

    private void SaveWeaponPurchases()
    {
        foreach (WeaponInfo weaponInfo in allWeaponInfos)
        {
            SaveWeaponPurchase(weaponInfo, 1);
            SaveWeaponPurchase(weaponInfo, 2);
            SaveWeaponPurchase(weaponInfo, 3);
        }
    }

    private void LoadWeaponPurchases()
    {
        foreach (WeaponInfo weaponInfo in allWeaponInfos)
        {
            LoadWeaponPurchase(weaponInfo, 1);
            LoadWeaponPurchase(weaponInfo, 2);
            LoadWeaponPurchase(weaponInfo, 3);
        }
    }
}

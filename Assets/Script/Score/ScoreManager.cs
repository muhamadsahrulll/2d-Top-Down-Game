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
    }

    public void Update()
    {
        SaveScores();
        SaveWeaponPurchases();
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
                level5OrganicScore += score; // Tambahkan kasus untuk level 5
                break;
            case 6:
                level6OrganicScore += score; //level6
                break;
            default:
                Debug.LogWarning("Level tidak valid.");
                break;
        }
        SaveScores();
        CalculateTotalOrganicScore();
    }

    private void SaveScores()
    {
        PlayerPrefs.SetInt("Level1OrganicScore", level1OrganicScore);
        PlayerPrefs.SetInt("Level2OrganicScore", level2OrganicScore);
        PlayerPrefs.SetInt("Level3OrganicScore", level3OrganicScore);
        PlayerPrefs.SetInt("Level4OrganicScore", level4OrganicScore);
        PlayerPrefs.SetInt("Level5OrganicScore", level5OrganicScore); // Tambahkan penyimpanan untuk level 5
        PlayerPrefs.SetInt("Level6OrganicScore", level6OrganicScore); //level 6
    }

    // Fungsi untuk memuat skor organik dari PlayerPrefs
    private void LoadScores()
    {
        level1OrganicScore = PlayerPrefs.GetInt("Level1OrganicScore");
        level2OrganicScore = PlayerPrefs.GetInt("Level2OrganicScore");
        level3OrganicScore = PlayerPrefs.GetInt("Level3OrganicScore");
        level4OrganicScore = PlayerPrefs.GetInt("Level4OrganicScore");
        level5OrganicScore = PlayerPrefs.GetInt("Level5OrganicScore"); // Tambahkan pemuatan untuk level 5
        level6OrganicScore = PlayerPrefs.GetInt("Level6OrganicScore");
    }

    // Fungsi untuk menghitung total skor organik keseluruhan
    private void CalculateTotalOrganicScore()
    {
        totalOrganicScore = level1OrganicScore + level2OrganicScore + level3OrganicScore + level4OrganicScore + level5OrganicScore + level6OrganicScore; // Tambahkan level 5
    }
    


    //FUNGSI BELI SENJATA
    // Fungsi beli senjata
    public void BuyWeapon1(WeaponInfo weaponInfo)
    {
        weaponInfo.isPurchased = true;
        SaveWeaponPurchase(weaponInfo, 1);

        //tambahkan start corotine IEnumerator notifBerhasil
        StartCoroutine(WeaponShop.Instance.notifBerhasil(2.0f));
    }

    public void BuyWeapon2(WeaponInfo weaponInfo)
    {
        if (totalOrganicScore >= 100 && !weaponInfo.isPurchased)
        {
            weaponInfo.isPurchased = true;
            SaveWeaponPurchase(weaponInfo, 2);
            //tambahkan start corotine IEnumerator notifBerhasil
            StartCoroutine(WeaponShop.Instance.notifBerhasil(2.0f));
        }
        else
        {
            Debug.LogWarning("Skor tidak mencukupi atau senjata sudah dibeli.");
            //tambahkan start corotine IEnumerator notifBerhasil
            StartCoroutine(WeaponShop.Instance.notifGagal2(2.0f));
        }
    }

    public void BuyWeapon3(WeaponInfo weaponInfo)
    {
        if (totalOrganicScore >= 200 && !weaponInfo.isPurchased)
        {
            weaponInfo.isPurchased = true;
            SaveWeaponPurchase(weaponInfo, 3);
            //tambahkan start corotine IEnumerator notifBerhasil
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

    // Fungsi untuk menyimpan status pembelian senjata saat game berjalan
    private void SaveWeaponPurchases()
    {
        // Disini kamu bisa loop untuk menyimpan status pembelian semua senjata yang kamu punya.
        foreach (WeaponInfo weaponInfo in allWeaponInfos)
        {
            SaveWeaponPurchase(weaponInfo, 1);
            SaveWeaponPurchase(weaponInfo, 2);
            SaveWeaponPurchase(weaponInfo, 3);
        }
    }

    // Fungsi untuk memuat status pembelian senjata saat permainan dimulai
    private void LoadWeaponPurchases()
    {
        // Disini kamu bisa loop untuk memuat status pembelian semua senjata yang kamu punya.
        foreach (WeaponInfo weaponInfo in allWeaponInfos)
        {
            LoadWeaponPurchase(weaponInfo, 1);
            LoadWeaponPurchase(weaponInfo, 2);
            LoadWeaponPurchase(weaponInfo, 3);
        }
    }



}

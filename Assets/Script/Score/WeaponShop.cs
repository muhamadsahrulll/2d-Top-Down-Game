using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponShop : MonoBehaviour
{
    public static WeaponShop Instance;

    public WeaponInfo weaponInfo1;
    public WeaponInfo weaponInfo2;
    public WeaponInfo weaponInfo3;
    public TextMeshProUGUI totalSkor;
    public TextMeshProUGUI senjataKoinText;

    public GameObject notifBerhasil1;
    public GameObject notifGagal;

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

    private void Update()
    {
        // Update text for total organic score
        totalSkor.text = "Total Koin: " + ScoreManager.Instance.totalOrganicScore.ToString();
        senjataKoinText.text = "Koin: " + ScoreManager.Instance.senjataKoin.ToString();
    }
    public void BuyWeapon1()
    {
        ScoreManager.Instance.BuyWeapon1(weaponInfo1);
    }

    public void BuyWeapon2()
    {
        ScoreManager.Instance.BuyWeapon2(weaponInfo2);
    }

    public void BuyWeapon3()
    {
        ScoreManager.Instance.BuyWeapon3(weaponInfo3);
    }

    public IEnumerator notifBerhasil(float seconds)
    {
        notifBerhasil1.gameObject.SetActive(true);
        yield return new WaitForSeconds(seconds);
        notifBerhasil1.gameObject.SetActive(false);
    }

    public IEnumerator notifGagal2(float seconds)
    {
        notifGagal.gameObject.SetActive(true);
        yield return new WaitForSeconds(seconds);
        notifGagal.gameObject.SetActive(false);
    }
}

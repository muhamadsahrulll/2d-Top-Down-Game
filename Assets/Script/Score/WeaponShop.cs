using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponShop : MonoBehaviour
{
    
    public WeaponInfo weaponInfo1;
    public WeaponInfo weaponInfo2;
    public WeaponInfo weaponInfo3;
    public TextMeshProUGUI totalSkor;

    private void Update()
    {
        // Update text for total organic score
        totalSkor.text = "Total Score: " + ScoreManager.Instance.totalOrganicScore.ToString();
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
}

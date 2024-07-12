using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "New Weapon")]
public class WeaponInfo : ScriptableObject
{
    public GameObject weaponPrefab;
    public float Weaponcooldown;
    public bool isPurchased;  // Menandakan apakah senjata sudah dibeli
    public int cost; // Biaya untuk membeli senjata

}

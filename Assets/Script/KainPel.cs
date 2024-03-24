using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KainPel : MonoBehaviour, IWeapon
{
    public void Attack()
    {
        Debug.Log("Kain Pel Attack");
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }
}

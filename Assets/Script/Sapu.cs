using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sapu : MonoBehaviour, IWeapon
{
    public void Attack()
    {
        Debug.Log("Sapu Attack");
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{

    //[SerializeField] private int damageAmount = 20; 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<EnemyAI>())
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            int damageAmount = ActiveWeapon.Instance.CurrentActiveWeapon is IWeapon weapon ? weapon.DamageAmount : 20;
            enemyHealth.TakeDamage(damageAmount);
        }
    }
}

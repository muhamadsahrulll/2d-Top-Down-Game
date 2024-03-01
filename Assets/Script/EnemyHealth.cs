using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int Health = 3;

    private int currentHealth;

    private void Start()
    {
        currentHealth = Health;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        DetechDeath();
    }

    private void DetechDeath()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int Health = 100;
    [SerializeField] private int level = 1; // Level musuh, bisa diubah sesuai level musuh

    private int currentHealth;
    private Knockback knockback;
    private Flash flash;

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        currentHealth = Health;
    }

    private void Update()
    {
        Health = currentHealth;
        DetectDeath();
    }

    public void TakeDamage(int baseDamage)
    {
        int actualDamage = CalculateDamage(baseDamage);
        currentHealth -= actualDamage;
        knockback.GetKnockedBack(PlayerController.Instance.transform, 15f);
        StartCoroutine(flash.FlashRoutine());
    }

    private int CalculateDamage(int baseDamage)
    {
        switch (level)
        {
            case 1:
                return baseDamage + 5;
            case 2:
                return baseDamage + 10;
            case 3:
                return baseDamage + 15;
            case 4:
                return baseDamage + 20;
            case 5:
            case 6:
                return baseDamage + 20;
            default:
                Debug.LogWarning("Level tidak valid, menggunakan base damage.");
                return baseDamage;
        }
    }

    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Musuh Mati");
            Destroy(gameObject);
        }
    }

    public void SetLevel(int newLevel)
    {
        level = newLevel;
    }
}

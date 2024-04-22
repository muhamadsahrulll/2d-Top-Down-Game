using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int damageOnApproach = 20;
    [SerializeField] private float knockBackThrustAmount = 10f;
    //[SerializeField] private float damageRecoveryTime = 1f;

    private int currentHealth;
    private bool canTakeDamage = true;
    private Knockback knockback;
    private Flash flash;

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy && canTakeDamage)
        {
            TakeDamage(damageOnApproach);
            knockback.GetKnockedBack(other.gameObject.transform, knockBackThrustAmount);
            StartCoroutine(flash.FlashRoutine());
            Debug.Log("terkena damage");
        }
    }
    
    public void TakeDamage(int damageAmount)
    {
        canTakeDamage = false;
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Debug.Log("Player mati");
            //Destroy(gameObject);
        }
        StartCoroutine(DamageRecoveryRoutine());

    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(1f); // Misalnya, tunggu 1 detik
        canTakeDamage = true;
    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}

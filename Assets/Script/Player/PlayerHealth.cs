using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (currentHealth == 0)
        {
            Debug.Log("Player mati");
            HandlePlayerDeath();
            Destroy(gameObject);
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

    private void HandlePlayerDeath()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "level1":
                GameManager.Instance?.PlayerDied();
                break;
            case "level2":
                InorganicManager.Instance?.PlayerDied();
                break;
            case "level3":
                QuizManager1.Instance?.PlayerDied();
                break;
            case "level4":
                QuizManager2.Instance?.PlayerDied();
                break;
            case "level5":
                RecycleManager.Instance?.PlayerDied();
                break;
            case "level6":
                RecycleManager2.Instance?.PlayerDied();
                break;
            default:
                Debug.LogWarning("Scene not recognized for player death handling.");
                break;
        }
    }
}

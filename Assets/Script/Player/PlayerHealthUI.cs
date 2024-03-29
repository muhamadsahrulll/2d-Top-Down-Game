using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        UpdateHealthUI();
    }

    void Update()
    {
        // Lakukan update UI hanya jika kesehatan berubah
        if (playerHealth.GetCurrentHealth() != int.Parse(healthText.text))
        {
            UpdateHealthUI();
        }
    }

    void UpdateHealthUI()
    {
        healthText.text = playerHealth.GetCurrentHealth().ToString();
    }
}

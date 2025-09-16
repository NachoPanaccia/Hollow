using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour, IHealthObserver
{
    [SerializeField] private Text healthText;

    public void OnHealthChanged(int currentHealth, int maxHealth)
    {
        healthText.text = $"Health: {currentHealth}/{maxHealth}";
    }
}
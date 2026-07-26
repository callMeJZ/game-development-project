using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private PlayerHealth playerHealth;
    [SerializeField]private Image totalHealthBar;
    [SerializeField]private Image currentHealthBar;
    void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        totalHealthBar.fillAmount = (playerHealth.startingHealth.Value) / 10;
    }
    void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth.Value / 10;
    }
}

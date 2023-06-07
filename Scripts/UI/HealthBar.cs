using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class HealthBar : MonoBehaviour
{
    public TMP_Text healthBarText;
    public Slider healthSlider;

    Damageable playerDamageable;

    private void Awake() 
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDamageable = player.GetComponent<Damageable>();
    }

    private void Start() 
    {
        healthBarText.text = "HP" + playerDamageable.Health + "/" + playerDamageable.MaxHealth;
        healthSlider.value = CalcHealthBarValue(playerDamageable.Health, playerDamageable.MaxHealth);    
    }

    private void OnEnable() 
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);   
    }

    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    private float CalcHealthBarValue(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    private void OnPlayerHealthChanged(float currentHealth, float maxHealth)
    {
        healthBarText.text = "HP:" + currentHealth + "/" + maxHealth;
        healthSlider.value = CalcHealthBarValue(currentHealth, maxHealth);
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
  

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        slider.value = currentHealth / maxHealth;
    }
}

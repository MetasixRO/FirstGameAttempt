using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private Slider slider;
    private TextMeshProUGUI text;

    public void Start()
    {
        slider = GetComponent<Slider>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        Combat.SetHealth += SetHealth;
        Combat.SetMaxHealth += SetMaxHealth;
    }

    public void SetMaxHealth(float health) {
        slider.maxValue = health;
    }

    public void SetHealth(float health) {
        slider.value = health;
        text.text = health.ToString();
    }
}

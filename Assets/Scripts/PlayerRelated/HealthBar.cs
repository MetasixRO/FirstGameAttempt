using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public delegate void HealthBarEvent(bool value);
    public static event HealthBarEvent ManageVignette;

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
        if (health != 0)
        {
            text.text = health.ToString();
            if (health < 0.25 * slider.maxValue)
            {
                ManageVignette?.Invoke(true);
            }
            else {
                ManageVignette?.Invoke(false);
            }
        }
        else
        {
            text.text = "";
        }
    }
}

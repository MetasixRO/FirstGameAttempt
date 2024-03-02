using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int enemyID, float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth( float health)
    {
            slider.value = health;
    }
}

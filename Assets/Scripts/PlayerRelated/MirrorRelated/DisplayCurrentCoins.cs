using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayCurrentCoins : MonoBehaviour
{
    private TextMeshProUGUI coins;

    private void Start()
    {
        coins = GetComponent<TextMeshProUGUI>();
        ResourceManager.DisplayResources += DisplayResources;
    }

    private void DisplayResources(int keys, int coins, int ambrosia)
    {
        this.coins.text = coins.ToString();
    }
}

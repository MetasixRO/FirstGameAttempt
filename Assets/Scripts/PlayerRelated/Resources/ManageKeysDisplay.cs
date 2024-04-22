using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManageKeysDisplay : MonoBehaviour
{
    private TextMeshProUGUI counter;

    private void Start()
    {
        counter = GetComponent<TextMeshProUGUI>();
    }

    private void UpdateKeys(int counter) {
        this.counter.text = counter.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayDescription : MonoBehaviour
{
    private TextMeshProUGUI description;

    private void Start()
    {
        MenuElementManager.DisplayMirrorAbilityDescription += DisplayDescriptionMethod;
        description = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void DisplayDescriptionMethod(string description) {
        this.description.text = description;
    }
}

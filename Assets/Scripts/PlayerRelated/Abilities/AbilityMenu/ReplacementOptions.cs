using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReplacementOptions : MonoBehaviour
{
    private TextMeshProUGUI replacementText;
    private Button[] buttons;

    private void Start()
    {
        replacementText = GetComponentInChildren<TextMeshProUGUI>();
        buttons = GetComponentsInChildren<Button>();
        gameObject.SetActive(false);
        //AbilitiesManager.TooManyAbilities += ActivateElements;
    }

    private void ActivateElements() { 
        gameObject.SetActive(true);
    }
}

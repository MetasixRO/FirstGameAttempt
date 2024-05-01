using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManageResourcesDisplay : MonoBehaviour
{
    private TextMeshProUGUI[] resourcesDisplay;

    private void Start()
    {
        resourcesDisplay = GetComponentsInChildren<TextMeshProUGUI>();
        ResourceManager.DisplayResources += DisplayResources;
    }

    private void DisplayResources(int keys, int coins, int ambrosia) {
        if (resourcesDisplay.Length != 3) {
            Debug.Log("Error obtaining text.");
            return;
        }

        if (resourcesDisplay[0] != null) {
            resourcesDisplay[0].text = keys.ToString();
        }

        if (resourcesDisplay[1] != null) {
            resourcesDisplay[1].text = coins.ToString();
        }

        if (resourcesDisplay[2] != null) {
            resourcesDisplay[2].text = ambrosia.ToString();
        }
    }
}

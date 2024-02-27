using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRestorer : MonoBehaviour, IInteractable
{

    public delegate void HealPlayer(int amountRestored);
    public static event HealPlayer MedpackHeal;

    public string prompt;
    public string InteractionPrompt() {
        return prompt;
    }

    public void Interact() {
        if (MedpackHeal != null) {
            MedpackHeal(15);
        }
        gameObject.SetActive(false);
    }
}
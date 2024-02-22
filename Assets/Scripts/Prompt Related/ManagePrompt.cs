using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManagePrompt : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
        InteractorScript.Manage += PromptManager;
    }

    private void PromptManager(bool shouldBeActive, string prompText) {
        if (shouldBeActive)
        {
            gameObject.SetActive(true);
        }
        else { 
            gameObject.SetActive(false);
        }
    }
}

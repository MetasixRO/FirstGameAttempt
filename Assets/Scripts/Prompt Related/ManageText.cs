using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageText : MonoBehaviour
{

    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        InteractorScript.Manage += TextManager;
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void TextManager(bool shouldBeActive, string prompText)
    {
        if (textMeshPro != null) {
            textMeshPro.text = prompText;
        }

    }
}

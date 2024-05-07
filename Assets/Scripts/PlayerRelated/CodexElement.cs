using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CodexElement : MonoBehaviour
{
    private TextMeshProUGUI npcName;
    private TextMeshProUGUI currentRank;
    private TextMeshProUGUI maxRank;

    private void Awake()
    {
        TextMeshProUGUI[] elements = GetComponentsInChildren<TextMeshProUGUI>();
        if (elements.Length == 3)
        {
            npcName = elements[0];
            currentRank = elements[1];
            maxRank = elements[2];
        }
    }

    public void SetData(string npcName, int currentRank) {
        this.npcName.text = npcName;
        this.currentRank.text = currentRank.ToString();
        this.maxRank.text = "5";
    }
}

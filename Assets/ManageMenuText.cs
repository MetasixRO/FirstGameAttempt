using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManageMenuText : MonoBehaviour
{
    private Button[] buttons;
    private TextMeshProUGUI[] buttonsText;
    private TextMeshProUGUI description;
    private AbilitiesManager player;

    private void Start()
    {
        //AbilityMenuState.OpenMenu += SetAbilities;
        ManageAbilityMenu.Launched += SetAbilities;

        player = PlayerTracker.instance.player.GetComponent<AbilitiesManager>();

        buttons = GetComponentsInChildren<Button>();

        List<TextMeshProUGUI> textForButtons = new List<TextMeshProUGUI>();

        foreach (Button button in buttons) {

            textForButtons.Add(button.GetComponentInChildren<TextMeshProUGUI>());
        }
        buttonsText = textForButtons.ToArray();

        description = GetComponentInChildren<TextMeshProUGUI>();

        InitializeNames();
    }

    private void SetAbilities() {
        if(player!= null)
        {
            List<string> names = player.GetAllNames();

            for(int counter = 0; counter < names.Count; counter++)
            {
                buttonsText[counter].text = names[counter];
            }

            if (names.Count == 0) {
                for (int counter = 0; counter < 3; counter++) {
                    buttonsText[counter].text = "None";
                }
                description.text = "No ability";
            }
        }
    }

    private void InitializeNames() {
        for (int i = 0; i < 3; i++) {
            buttonsText[i].text = "None";
        }
        UpdateDescription(0);
    }

    public void UpdateDescription(int buttonIndex) {
        if (buttonIndex >= player.GetAbilityCount())
        {
            description.text = "No ability";
        }
        else
        {
            description.text = player.GetDescriptionByIndex(buttonIndex);
        }
    }
}

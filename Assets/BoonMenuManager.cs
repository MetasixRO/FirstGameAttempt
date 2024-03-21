using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoonMenuManager : MonoBehaviour
{

    public delegate void BoonButtonSelected(AbilityScriptableObject ability);
    public static event BoonButtonSelected AbilitySelected;

    private Button[] buttons;
    private Dictionary<Button, TextMeshProUGUI[]> components;
    private Dictionary<Button, AbilityScriptableObject> buttonAbilitiy;

    void Start()
    {
        gameObject.SetActive(false);
        BoonInteractable.Interacted += EnableAndManage;

        components = new Dictionary<Button,TextMeshProUGUI[]>();
        buttonAbilitiy = new Dictionary<Button,AbilityScriptableObject>();
        buttons = GetComponentsInChildren<Button>();

        foreach (Button button in buttons) {
            components.Add(button, button.GetComponentsInChildren<TextMeshProUGUI>());
        }
    }

    private void EnableAndManage(List<AbilityScriptableObject> abilities) { 
        gameObject.SetActive(true);
        buttonAbilitiy.Clear();

        for (int i = 0; i < buttons.Length && i < abilities.Count; i++) {
            components[buttons[i]][0].text = abilities[i].name;
            components[buttons[i]][1].text = abilities[i].description;
            buttonAbilitiy.Add(buttons[i], abilities[i]);
        }
    }

    public void ButtonClicked(Button button) {
        if (buttonAbilitiy.ContainsKey(button) && AbilitySelected != null) {
            AbilitySelected(buttonAbilitiy[button]);
            gameObject.SetActive(false);
        }
    }
}

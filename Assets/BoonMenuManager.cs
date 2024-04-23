using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoonMenuManager : MonoBehaviour
{

    public delegate void BoonButtonSelected(AbilityScriptableObject ability);
    public static event BoonButtonSelected AbilitySelected;

    public delegate void BoonMenuStatus();
    public static event BoonMenuStatus Activated;
    public static event BoonMenuStatus Deactivated;

    private Button[] buttons;
    private Dictionary<Button, TextMeshProUGUI[]> components;
    private Dictionary<Button, AbilityScriptableObject> buttonAbilitiy;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        //gameObject.SetActive(false);
        BoonInteractable.Interacted += EnableAndManage;

        components = new Dictionary<Button,TextMeshProUGUI[]>();
        buttonAbilitiy = new Dictionary<Button,AbilityScriptableObject>();
        buttons = GetComponentsInChildren<Button>();

        foreach (Button button in buttons) {
            components.Add(button, button.GetComponentsInChildren<TextMeshProUGUI>());
        }
    }

    private void EnableAndManage(List<AbilityScriptableObject> abilities) {
        if (Activated != null) {
            Activated();
        }
        //gameObject.SetActive(true);
        animator.SetTrigger("Open");
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

            if (Deactivated != null) {
                Deactivated();
            }

            //gameObject.SetActive(false);
            animator.SetTrigger("Close");
        }
    }
}

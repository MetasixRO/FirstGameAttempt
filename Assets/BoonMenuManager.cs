using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoonMenuManager : MonoBehaviour
{

    public delegate void BoonButtonSelected(AbilityScriptableObject ability);
    public static event BoonButtonSelected AbilitySelected;

    public delegate void BoonReplacementButtonSelected(AbilityScriptableObject newAbility, AbilityScriptableObject ability);
    public static event BoonReplacementButtonSelected ReplacementSelected;

    public delegate void BoonMenuStatus();
    public static event BoonMenuStatus Activated;
    public static event BoonMenuStatus Deactivated;

    private Button[] buttons;
    private Dictionary<Button, TextMeshProUGUI[]> components;
    private Dictionary<Button, AbilityScriptableObject> buttonAbilitiy;

    private Animator animator;

    private bool replacement;
    private AbilityScriptableObject replacementAbility;

    void Start()
    {
        replacement = false;
        animator = GetComponent<Animator>();
        //gameObject.SetActive(false);
        BoonInteractable.Interacted += EnableAndManage;

        AbilitiesManager.TooManyAbilities += ReplacementEnableAndManage;

        components = new Dictionary<Button,TextMeshProUGUI[]>();
        buttonAbilitiy = new Dictionary<Button,AbilityScriptableObject>();
        buttons = GetComponentsInChildren<Button>();

        foreach (Button button in buttons) {
            components.Add(button, button.GetComponentsInChildren<TextMeshProUGUI>());
        }
    }

    private void ReplacementEnableAndManage(AbilityScriptableObject ability, List<AbilityScriptableObject> abilities) {
        replacement = true;
        replacementAbility = ability;
        EnableAndManage(abilities);
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
        if (buttonAbilitiy.ContainsKey(button)) {
            if (!replacement)
            {
                AbilitySelected?.Invoke(buttonAbilitiy[button]);
            }
            else
            {
                ReplacementSelected?.Invoke(replacementAbility, buttonAbilitiy[button]);
                replacement = false;
            }

            if (Deactivated != null) {
                Deactivated();
            }

            //gameObject.SetActive(false);
            animator.SetTrigger("Close");
        }
    }

    public void CloseMenu() {
        Deactivated?.Invoke();
        animator.SetTrigger("Close");
    }
}

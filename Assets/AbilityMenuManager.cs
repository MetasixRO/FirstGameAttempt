using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMenuManager : MonoBehaviour
{
    public GameObject abilityMenu;
    Animator animator;
    private bool isOpen = false;

    private void Start()
    {
        abilityMenu.SetActive(false);
        animator = abilityMenu.GetComponent<Animator>();
        AbilityMenuState.OpenMenu += OpenMenu;
        AbilityMenuState.CloseMenu += CloseMenu;
    }

    private void OpenMenu() {
        if (!isOpen)
        {
            isOpen = true;
            abilityMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void CloseMenu() {
        if (isOpen)
        {
            isOpen = false;
            abilityMenu?.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageAbilityMenu : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        gameObject.SetActive(false);
        animator = GetComponent<Animator>();
        AbilityMenuState.OpenMenu += OpenMenu;
        AbilityMenuState.CloseMenu += CloseMenu;
    }

    private void OpenMenu() {
        gameObject.SetActive(true);
        animator.SetTrigger("Open");
    }

    private void CloseMenu() {
        animator.SetTrigger("Close");
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageAbilityMenu : MonoBehaviour
{
    public delegate void AbilityMenuEvent();
    public static event AbilityMenuEvent Launched;
    public static event AbilityMenuEvent Ended;

    Animator animator;

    private void Start()
    {
        gameObject.SetActive(false);
        animator = GetComponent<Animator>();
        //AbilityMenuState.OpenMenu += OpenMenu;
        //AbilityMenuState.CloseMenu += CloseMenu;
        newArenaState.OpenMenu += OpenMenu;
        newAbilityMenuState.CloseMenu += CloseMenu;
    }

    private void OpenMenu() {
        gameObject.SetActive(true);
        animator.SetTrigger("Open");
        if (Launched != null) {
            Launched();
        }
    }

    private void CloseMenu() {
        animator.SetTrigger("Close");
        gameObject.SetActive(false);
        if (Ended != null)
        {
            Ended();
        }
    }
}

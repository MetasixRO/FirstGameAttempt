using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageAbilityMenu : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            animator.SetTrigger("Manage");
        }
    }
}

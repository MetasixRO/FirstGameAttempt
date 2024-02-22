using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

interface IInteractable
{
    public void Interact();

    public string InteractionPrompt();
}

public class InteractorScript : MonoBehaviour
{

    public delegate void PromptManager(bool isActive, string prompText = "default");
    public static event PromptManager Manage;

    //Punctul in care care va fi aplicata raza de interact
    public Transform interactorSource;
    //retine raza in care se poate interactiona cu obiectul
    [SerializeField] private float interactRange;

    public LayerMask interactLayerMask;

    private readonly Collider[] colliders = new Collider[3];

    //Numarul de obiecte gasite
    private int numFound;

    PlayerInput input;

    bool interactPressed;

    private IInteractable interactable;

    private void Awake()
    {
        input = new PlayerInput();
        input.CharacterControls.Use.performed += ctx => interactPressed = ctx.ReadValueAsButton();
    }

    private void OnDrawGizmosSelected()
    {
        if (interactorSource == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactorSource.position, interactRange);
    }

    // Update is called once per frame
    void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactorSource.position, interactRange, colliders, interactLayerMask);
        if (numFound > 0)
        {
            interactable = colliders[0].GetComponent<IInteractable>();

            if (Manage != null && interactable != null) {
                Manage(true, interactable.InteractionPrompt());
            }

            if (interactable != null && interactPressed)
            {
                interactable.Interact();
            }
        }
        else {
            if (Manage != null) {
                Manage(false);
            }
        }
    }

    void OnEnable()
    {
        input.CharacterControls.Enable();
    }

    void OnDisable()
    {
        input.CharacterControls.Disable();
    }
}

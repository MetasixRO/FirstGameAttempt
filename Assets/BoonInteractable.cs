using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonInteractable : BaseInteractable, IInteractable
{
    public delegate void OnBoonInteract(List<AbilityScriptableObject> abilities);
    public static event OnBoonInteract Interacted;

    private BoonAbilitiesContainer container;

    protected void Start()
    {
        gameObject.SetActive(false);
        container = GetComponent<BoonAbilitiesContainer>();
        EnemiesClearedEvent.SpawnAbilityBoon += Spawn;
    }

    protected void Spawn() {
        gameObject.SetActive(true);
        gameObject.transform.position = base.CalculateSpawnPosition();
    }

    public void Interact()
    {
        gameObject.SetActive(false);
        if (container != null && PlayerTracker.instance.player != null && Interacted != null) {
            List<AbilityScriptableObject> abilityObjects = container.RetrieveAbility();
            Interacted(abilityObjects);
        }
    }

    public string InteractionPrompt()
    {
        return "Interact";
    }

  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbrosiaInteractable : BaseInteractable, IInteractable
{
    public delegate void EndRoundInteractable(int resourceAmount);
    public static event EndRoundInteractable InteractedAmbrosia;

    private string promptText;
    private int coinsGenerated;

    protected void Start()
    {
        coinsGenerated = 1;
        gameObject.SetActive(false);
        EnemiesClearedEvent.SpawnAmbrosia += Spawn;
    }

    protected void Spawn()
    {
        gameObject.SetActive(true);
        gameObject.transform.position = base.CalculateSpawnPosition();
    }

    public string InteractionPrompt()
    {
        promptText = "Collect " + coinsGenerated.ToString() + " Ambrosia";
        return promptText;
    }

    public void Interact()
    {
        gameObject.SetActive(false);
        if (InteractedAmbrosia != null)
        {
            InteractedAmbrosia(coinsGenerated);
        }

    }
}

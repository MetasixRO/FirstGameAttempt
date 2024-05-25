using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealInteractable : BaseInteractable, IInteractable
{
    public delegate void EndRoundInteractable(int resourceAmount);
    public static event EndRoundInteractable InteractedHeal;

    private string promptText;
    private int healGenerated;
    private int selection;

    protected  void Start()
    {
        gameObject.SetActive(false);
        EnemiesClearedEvent.SpawnHeal += Spawn;
    }

    protected  void Spawn()
    {
        selection = Random.Range(1, 4);
        switch (selection) {
            case 1: healGenerated = 5; break;
            case 2: healGenerated = 10; break;
            case 3: healGenerated = 15; break;
        }

        gameObject.SetActive(true);
        gameObject.transform.position = base.CalculateSpawnPosition();
    }

    public  string InteractionPrompt()
    {
        promptText = "Collect " + healGenerated.ToString() + " Health";
        return promptText;
    }

    public  void Interact()
    {
        gameObject.SetActive(false);
        if (InteractedHeal != null)
        {
            InteractedHeal(healGenerated);
        }

    }
}

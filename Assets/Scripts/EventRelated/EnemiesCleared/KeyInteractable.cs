using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteractable : BaseInteractable, IInteractable
{
    public delegate void EndRoundInteractable(int resourceAmount);
    public static event EndRoundInteractable InteractedKeys;

    private string promptText;
    private int keysGenerated;

    protected void Start()
    {
        keysGenerated = 1;
        gameObject.SetActive(false);
        EnemiesClearedEvent.SpawnKeys += Spawn;
    }

    protected void Spawn()
    {
        gameObject.SetActive(true);
        gameObject.transform.position = base.CalculateSpawnPosition();
    }

    public string InteractionPrompt()
    {
        promptText = "Collect " + keysGenerated.ToString() + " Key";
        return promptText;
    }

    public void Interact()
    {
        gameObject.SetActive(false);
        if (InteractedKeys != null)
        {
            InteractedKeys(keysGenerated);
        }

    }
}

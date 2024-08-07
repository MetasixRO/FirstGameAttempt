using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsInteractable : BaseInteractable, IInteractable
{
    public delegate void EndRoundInteractable(int resourceAmount);
    public static event EndRoundInteractable InteractedCoins;

    private string promptText;
    private int coinsGenerated;

    protected  void Start()
    {
        
        gameObject.SetActive(false);
        EnemiesClearedEvent.SpawnCoins += Spawn;

        LevelManager.ClearRewards += Despawn;
        newDeadState.RespawnPlayer += Despawn;
    }

    protected  void Spawn() {
        coinsGenerated = Random.Range(5, 20);

        gameObject.SetActive(true);
        gameObject.transform.position = base.CalculateSpawnPosition();
    }

    public  string InteractionPrompt()
    {
        promptText = "Collect " + coinsGenerated.ToString() + " Coins";
        return promptText;
    }

    public  void Interact()
    {
        gameObject.SetActive(false);
        if (InteractedCoins != null) {
            InteractedCoins(coinsGenerated);
        }

    }

    private void Despawn()
    {
        gameObject.SetActive(false);
    }
}

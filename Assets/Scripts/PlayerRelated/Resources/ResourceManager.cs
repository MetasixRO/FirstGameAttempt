using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public delegate void DisplayResource(int keysAmount, int coinsAmount);
    public static event DisplayResource DisplayResources;

    public delegate void CheckResourcesResult(bool result);
    public static event CheckResourcesResult CheckKeysResult;
    public static event CheckResourcesResult CheckCoinsResult;


    [SerializeField] private int amountOfCoins;
    [SerializeField] private int amountOfKeys;

    private void Start()
    {

        amountOfCoins = 0;
        amountOfKeys = 0;

        DisplayEverything();

        CoinsInteractable.InteractedCoins += CollectCoins;
        DeepPockets.AddCoins += CollectCoins;
        KeyInteractable.InteractedKeys += CollectKeys;
        Weapons.WeaponPurchase += UseKeys;
        WeaponPrompt.CheckEnoughCredits += CheckKeys;
        MenuElementManager.CheckIfCanBuyMirrorAbility += CheckCoins;
        MirrorInteractable.MirrorUsed += DisplayEverything;
    }

    private void Update()
    {
        if (Input.GetKeyDown("o"))
        {
            Debug.Log("Debug feature to add keys and coins");
            CollectKeys(10);
            CollectCoins(100);
        }
    }

    private void CollectKeys(int amountCollected) {
        amountOfKeys += amountCollected;
        DisplayEverything();
    }

    private void CollectCoins(int amountCollected) {
        amountOfCoins += amountCollected;
        DisplayEverything();
    }

    private void UseKeys(int amountUsed) {
        amountOfKeys -= amountUsed;
        DisplayEverything();
    }

    private void UseCoins(int amountUsed) { 
        amountOfCoins -= amountUsed;
        DisplayEverything();
    }

    private void DisplayEverything()
    {
        if (DisplayResources != null) {
            DisplayResources(amountOfKeys, amountOfCoins);
        }
    }

    private void CheckKeys(int amount) {
        if (amount <= amountOfKeys)
        {
            if (CheckKeysResult != null)
            {
                CheckKeysResult(true);
            }
        }
        else {
            CheckKeysResult(false);
        }
    }

    private void CheckCoins(int amount) {
        if (amount <= amountOfCoins)
        {
            if (CheckCoinsResult != null)
            {
                CheckCoinsResult(true);
            }

            UseCoins(amount);
        }
        else {
            CheckCoinsResult(false);
        }
    }
}

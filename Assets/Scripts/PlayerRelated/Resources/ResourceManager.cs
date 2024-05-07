using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public delegate void DisplayResource(int keysAmount, int coinsAmount, int ambrosiaAmount);
    public static event DisplayResource DisplayResources;

    public delegate void CheckResourcesResult(bool result);
    public static event CheckResourcesResult CheckKeysResult;
    public static event CheckResourcesResult CheckCoinsResult;
    public static event CheckResourcesResult CheckAmbrosiaResult;


    [SerializeField] private int amountOfCoins;
    [SerializeField] private int amountOfKeys;
    [SerializeField] private int amountOfAmbrosia;

    private bool ambrosiaUsed;

    private void Start()
    {
        ambrosiaUsed = false;

        amountOfCoins = 0;
        amountOfKeys = 0;
        amountOfAmbrosia = 0;

        DisplayEverything();

        CoinsInteractable.InteractedCoins += CollectCoins;
        AmbrosiaInteractable.InteractedAmbrosia += CollectAmbrosia;
        DeepPockets.AddCoins += CollectCoins;
        KeyInteractable.InteractedKeys += CollectKeys;
        Weapons.WeaponPurchase += UseKeys;
        WeaponPrompt.CheckEnoughCredits += CheckKeys;
        MenuElementManager.CheckIfCanBuyMirrorAbility += CheckCoins;
        ManageDialogueBox.checkEnoughAmbrosia += CheckAmbrosia;
        ManageDialogueBox.giftDialogueTriggered += UseAmbrosia;
        DialogueTrigger.NoGiftDialogueLeft += RestoreAmbrosia;
        MirrorInteractable.MirrorUsed += DisplayEverything;
    }

    private void Update()
    {
        if (Input.GetKeyDown("o"))
        {
            Debug.Log("Debug feature to add keys, coins and ambrosia");
            CollectKeys(10);
            CollectCoins(100);
            CollectAmbrosia(10);
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

    private void CollectAmbrosia(int amountCollected) {
        amountOfAmbrosia += amountCollected;
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

    private void UseAmbrosia() {
        amountOfAmbrosia -= 1;
        DisplayEverything();
        ambrosiaUsed = true;
    }

    private void DisplayEverything()
    {
        if (DisplayResources != null) {
            DisplayResources(amountOfKeys, amountOfCoins, amountOfAmbrosia);
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

    private void CheckAmbrosia() {
        if (1 <= amountOfAmbrosia)
        {
            if (CheckAmbrosiaResult != null) {
                CheckAmbrosiaResult(true);
            }
        }
        else
        {
            if (CheckAmbrosiaResult != null)
            {
                CheckAmbrosiaResult(false);
            }
        }
    }

    private void RestoreAmbrosia() {
        if (ambrosiaUsed)
        {
            amountOfAmbrosia += 1;
            DisplayEverything();
            ambrosiaUsed = false;
        }
    }
}

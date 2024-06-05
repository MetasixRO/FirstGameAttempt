using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "High Confidence", menuName = "Passive/High Confidence")]
public class HighConfidence : MirrorAbilityBase
{
    public delegate void HighConfidenceEvent(int percentage);
    public static event HighConfidenceEvent IncreaseDamage;
    public static event HighConfidenceEvent ReduceDamage;

    private int currentRank;
    private int currentBonus;
    private int currentCost;

    private bool alreadyBuffed, alreadyDebuffed;


    public override void Enable()
    {
        alreadyBuffed = false;
        alreadyDebuffed = false;
        Combat.PlayerLowerThanPercentage += RemoveBuff;
        newDeadState.RespawnPlayer += RemoveBuff;
        DoorOpener.AnnounceStart += AddBuff;
        Combat.PlayerHigherThanPercentage += AddBuff;
    }

    public override void IncreaseRank()
    {
        currentRank += 1;
        if (currentRank == 1)
        {
            Enable();
        }
        
    }

    public override void IncreaseCurrentBonus()
    {
        currentBonus += 2;
        //IncreaseDamage?.Invoke(currentBonus);
        //alreadyBuffed = true;
    }

    public override void IncreasePrice()
    {
        currentCost += 50;
    }

    public override int GetBonus()
    {
        return currentBonus;
    }

    public override int GetRank()
    {
        return currentRank;
    }

    public override int GetCost()
    {
        return currentCost;
    }

    public override void RememberOriginalStats()
    {
        currentRank = base.rank;
        currentBonus = base.bonus;
        currentCost = base.cost;
    }

    private void RemoveBuff() {
        if (!alreadyDebuffed)
        {
            ReduceDamage?.Invoke(currentBonus);
            Debug.Log("Called removed buff");
            alreadyDebuffed = true;
            alreadyBuffed = false;
        }
    }

    private void AddBuff() {
        Debug.Log(alreadyBuffed);
        if (!alreadyBuffed)
        {
            IncreaseDamage?.Invoke(currentBonus);
            Debug.Log("Called Added buff");
            alreadyBuffed = true;
            alreadyDebuffed = false;
        }
    }
}

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
        if (alreadyBuffed && ReduceDamage != null) {
            ReduceDamage(currentBonus);
        }
        currentBonus += 5;
        if (IncreaseDamage != null)
        {
            IncreaseDamage(currentBonus);
            alreadyBuffed = true;
        }
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
        if (!alreadyDebuffed) {
            ReduceDamage(currentBonus);
            alreadyDebuffed = true;
            alreadyBuffed = false;
        }
    }

    private void AddBuff() {
        if (!alreadyBuffed) {
            IncreaseDamage(currentBonus);
            alreadyBuffed = true;
            alreadyDebuffed = false;
        }
    }
}

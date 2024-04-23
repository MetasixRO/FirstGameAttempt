using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Regenerate Vitality", menuName = "Passive/Regenerate Vitality")]
public class RegenerateAbility : MirrorAbilityBase
{
    public delegate void RegenerateVitalityEvent(int amount);
    public static event RegenerateVitalityEvent AddVitality;

    private int currentRank;
    private int currentBonus;
    private int currentCost;

    public override void Enable()
    {
        SubscribeToDoorEvent();
    }

    public override void IncreaseRank() {
        currentRank += 1;
        if (currentRank == 1) {
            Enable();
        }
    }

    public override void IncreaseCurrentBonus()
    {
        currentBonus += 2;
    }

    public override void IncreasePrice()
    {
        if (currentRank == maxRank)
        {
        }
        else {
            currentCost *= 2;
        }
    }

    public override int GetBonus()
    {
        return currentBonus;
    }

    public override int GetRank()
    {
        return currentRank;
    }

    public override int GetCost() {
        return currentCost;
    }

    public override void RememberOriginalStats()
    {
        currentRank = base.rank;
        currentBonus = base.bonus;
        currentCost = base.cost;
    }

    private void SubscribeToDoorEvent() {
        CloseArenaDoor.CloseDoor += RegenerateVitality;
    }

    private void RegenerateVitality() {
        if (AddVitality != null) {
            AddVitality(currentBonus);
        }
    }
}

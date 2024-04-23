using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Death Defiance", menuName = "Passive/Death Defiance")]
public class DeathDefianceAbility : MirrorAbilityBase
{
    public delegate void DeathDefianceEvent(int times);
    public static event DeathDefianceEvent DefyDeath;

    private int currentRank;
    private int currentBonus;
    private int currentCost;

    public override void Enable()
    {
        if (DefyDeath != null) {
            DefyDeath(currentBonus);
        }
        WeaponSelectorDoor.EnteredArmory += ResendAmountOfTimes;
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
        currentBonus += 1;
    }

    public override void IncreasePrice()
    {
        switch(currentRank){
            case 0: currentCost = 30; break;
            case 1: currentCost = 500; break;
            case 2: currentCost = 1000; break;
            case 3: currentCost = 1530; break;
            case 4: currentCost = 1530; break;
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

    private void ResendAmountOfTimes() {
        if (DefyDeath != null)
        {
            DefyDeath(currentBonus);
        }
    }
}

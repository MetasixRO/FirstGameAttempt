using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Thick Skin", menuName = "Passive/Thick Skin")]
public class ThickSkinAbility : MirrorAbilityBase
{
    public delegate void ThickSkinEvent(int amountToAddToMaxHealth);
    public static event ThickSkinEvent IncreaseMaxHealth;

    private int currentRank;
    private int currentBonus;
    private int currentCost;

    public override void Enable()
    {
    }

    public override void IncreaseRank()
    {
        currentRank += 1;
        if (IncreaseMaxHealth != null)
        {
            IncreaseMaxHealth(currentBonus);
        }
    }

    public override void IncreaseCurrentBonus()
    {
        currentBonus += 5;
    }

    public override void IncreasePrice()
    {
        currentCost += 5;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deep Pockets", menuName = "Passive/Deep Pockets")]
public class DeepPockets : MirrorAbilityBase
{
    public delegate void DeepPocketEvent(int coinsAmount);
    public static event DeepPocketEvent AddCoins;

    private int currentRank;
    private int currentBonus;
    private int currentCost;

    public override void Enable()
    {
        CloseArenaDoor.CloseDoor += GenerateCoins;
        LevelManager.ReachingNewArena += GenerateCoins;
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

    private void GenerateCoins()
    {
        if (AddCoins != null)
        {
            AddCoins(currentBonus);
        }
    }
}

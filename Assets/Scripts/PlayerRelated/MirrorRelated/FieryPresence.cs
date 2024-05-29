using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fiery Presence", menuName = "Passive/Fiery Presence")]
public class FieryPresence : MirrorAbilityBase
{
    public delegate void FieryPresenceEvent(int percentage);
    public static event FieryPresenceEvent IncreaseFirstHitDamage;

    private int currentRank;
    private int currentBonus;
    private int currentCost;

    public override void Enable() {
        LevelManager.ReachingNewArena += ResendSignal;
    }

    public override void IncreaseRank()
    {
        currentRank += 1;
        Enable();
    }


    public override void IncreaseCurrentBonus()
    {
        currentBonus += 15;
        if (IncreaseFirstHitDamage != null) {
            IncreaseFirstHitDamage(currentBonus);
        }
    }

    public override void IncreasePrice()
    {
        currentCost += 15;
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

    private void ResendSignal() {
        IncreaseFirstHitDamage?.Invoke(currentBonus);
    }
}

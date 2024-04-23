using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MirrorAbilityBase : ScriptableObject
{
    public int maxRank;
    public int rank;
    public int cost;
    public int bonus;
    public string abilityName;
    public string description;

    public virtual void Enable() { }

    public virtual void Disable() { }

    public virtual bool IsTimed() { return false; }

    public virtual float GetTimer() { return 0.0f; }

    public virtual void IncreasePrice() { }

    public virtual void IncreaseRank() { }

    public virtual void IncreaseCurrentBonus() { }

    public string GetName() { return abilityName; }

    public string GetDescription() { return description; }

    public virtual int GetCost() { return cost; }

    public virtual int GetRank() { return rank; }

    public virtual int GetBonus() { return bonus; }

    public virtual void RememberOriginalStats() { }
}

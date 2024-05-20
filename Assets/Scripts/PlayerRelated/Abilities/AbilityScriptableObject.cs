using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    DashEffect,
    DashTimeReducer,
    DamageBoost,
    HealthBoost,
    HealthProtection,
    NPCSpecial,
    Rewards,
    InstantRewards
}

public abstract class AbilityScriptableObject : ScriptableObject
{
    public new string name;
    public float cooldown;
    public float activeTime;
    public string description;
    public AbilityType abilityType;

    public bool isActive;

    public abstract void Activate();

    public abstract void Disable();

    public bool GetAbilityStatus() {
        return isActive;
    }
}

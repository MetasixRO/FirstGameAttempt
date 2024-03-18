using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    DashEffect,
    DashTimeReducer,
    DamageBoost,
}

public class AbilityScriptableObject : ScriptableObject
{
    public new string name;
    public float cooldown;
    public float activeTime;
    public string description;
    public AbilityType abilityType;

    public bool isActive;

    public virtual void Activate() { }

    public virtual void Disable() { }

    public bool GetAbilityStatus() {
        return isActive;
    }
}

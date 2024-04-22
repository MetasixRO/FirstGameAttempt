using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : ScriptableObject
{
    public virtual void Activate(float damage) { }

    public virtual void Deactivate() { }

    public virtual bool IsTimed() { return false; }

    public virtual float GetTimer() { return 0.0f; }
}

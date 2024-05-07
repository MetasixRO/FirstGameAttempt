using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spear Special", menuName = "Special/Spear Special")]
public class SpearSpecial : SpecialAttack
{
    public delegate void SpearSpecialEvent(float damage);
    public static event SpearSpecialEvent ApplyBleeding;
    public static event SpearSpecialEvent DisableBleeding;

    [SerializeField] private float activeTimer = 5.0f;
    [SerializeField] private float damage = 5.0f;

    public override void Activate(float damage)
    {
        if (ApplyBleeding != null) {
            ApplyBleeding(damage);
        }
    }

    public override void Deactivate()
    {
        if (DisableBleeding != null) {
            DisableBleeding(damage);
        }
    }

    public override bool IsTimed()
    {
        return true;
    }

    public override float GetTimer()
    {
        return activeTimer;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Knife Special", menuName = "Special/Knife Special")]
public class KnifeSpecial : SpecialAttack
{
    public delegate void KnifeSpecialEvent();
    public static event KnifeSpecialEvent BoostStats;
    public static event KnifeSpecialEvent ResetStats;

    [SerializeField] private float activeTimer = 5.0f;

    public override void Activate(float damage)
    {
        if (BoostStats != null) {
            BoostStats();
        }
    }

    public override void Deactivate()
    {
        if (ResetStats != null) {
            ResetStats();
        }
    }

    public override bool IsTimed() {
        return true;
    }

    public override float GetTimer() {
        return activeTimer;
    }
}

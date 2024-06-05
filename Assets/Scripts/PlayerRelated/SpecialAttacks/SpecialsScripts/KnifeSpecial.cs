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
    private bool notReset;

    public override void Activate(float damage)
    {
        if (BoostStats != null) {
            BoostStats();
        }
        notReset = false;
        newDeadState.ReachedZeroHealth += SetNotReset;
    }

    public override void Deactivate()
    {
        if (!notReset)
        {
            if (ResetStats != null)
            {
                ResetStats();
            }
        }
        newDeadState.RespawnPlayer += SetNotReset;
    }

    public override bool IsTimed() {
        return true;
    }

    public override float GetTimer() {
        return activeTimer;
    }

    private void SetNotReset() {
        notReset = true;
    }
}

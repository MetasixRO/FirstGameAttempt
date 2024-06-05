using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Permanent Tradeoff", menuName = "Ability/Permanent Tradeoff")]
public class PermanentTradeoff : Tradeoff
{
    public static event TradeoffEvent ReduceMaxHealth;
    public static event TradeoffEvent RestoreMaxHealth;

    public override void ReduceHealth()
    {
        ReduceMaxHealth?.Invoke(2);
        //Combat.PlayerDead += Disable;
    }

    public override void Disable()
    {
        RestoreMaxHealth.Invoke(2);
        //Combat.PlayerDead += Disable;
    }
}

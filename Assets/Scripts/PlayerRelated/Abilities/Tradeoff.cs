using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tradeoff", menuName = "Ability/Tradeoff")]
public class Tradeoff : AbilityScriptableObject
{
    public delegate void TradeoffEvent(int amount);
    public static event TradeoffEvent Coins;
    public static event TradeoffEvent Keys;
    public static event TradeoffEvent Ambrosia;
    public static event TradeoffEvent ReduceCurrentHealth;

    public override void Activate()
    {
        switch (Random.Range(1, 4))
        {
            case 1: Coins?.Invoke(Random.Range(10, 40)); break;
            case 2: Keys?.Invoke(Random.Range(2, 4)); break;
            case 3: Ambrosia?.Invoke(Random.Range(2, 4)); break;
        }
        ReduceHealth();
    }

    public virtual void ReduceHealth() {
        ReduceCurrentHealth?.Invoke(5);
    }

    public override void Disable()
    {
    }
}

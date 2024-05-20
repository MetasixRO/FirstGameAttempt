using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Treasure", menuName = "Ability/Treasure")]
public class Treasure : AbilityScriptableObject
{
    public delegate void TreasureEvent(int amount);
    public static event TreasureEvent Coins;
    public static event TreasureEvent Keys;
    public static event TreasureEvent Ambrosia;

    public override void Activate()
    {
        switch (Random.Range(1, 4)) {
            case 1: Coins?.Invoke(Random.Range(5, 20)); break;
            case 2: Keys?.Invoke(Random.Range(1,3)); break;
            case 3: Ambrosia?.Invoke(Random.Range(1, 3)); break;
        }
    }

    public override void Disable()
    {
    }
}

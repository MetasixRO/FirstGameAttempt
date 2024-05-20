using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Double Rewards", menuName = "Ability/Double Rewards")]
public class DoubleRewards : AbilityScriptableObject
{
    public delegate void SendDoubleRewards();
    public static event SendDoubleRewards DoubleCurrentReward;

    public override void Activate()
    {
        ResourceManager.VerifyDoubleRewards += DoubleTheRewards;
    }

    public override void Disable()
    {
        ResourceManager.VerifyDoubleRewards -= DoubleTheRewards;
    }

    private void DoubleTheRewards() {
        DoubleCurrentReward?.Invoke();
    }
}

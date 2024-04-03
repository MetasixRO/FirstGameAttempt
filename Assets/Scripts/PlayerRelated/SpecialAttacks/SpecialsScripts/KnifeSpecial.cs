using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Knife Special", menuName = "Special/Knife Special")]
public class KnifeSpecial : SpecialAttack
{
    public override void Activate(float damage)
    {
        Debug.Log("Knife activated");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackHandler : MonoBehaviour
{
    private SpecialAttack special;

    private void Start()
    {
        WeaponPrompt.SpecialAttackInside += SetSpecialAttack;
        Combat.ManageSpecial += ActivateSpecial;
    }

    public void SetSpecialAttack(SpecialAttack special) { 
        this.special = special;
    }

    private void ActivateSpecial(float damage) {
        if (special != null)
        {
            special.Activate(damage);
        }
        else {
            Debug.Log("No Special Attack Set.");
        }
    }


}

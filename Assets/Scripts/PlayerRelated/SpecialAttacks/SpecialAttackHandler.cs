using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackHandler : MonoBehaviour
{
    private SpecialAttack special;
    private bool isTimed;
    private float timer;

    private void Start()
    {
        isTimed = false;
        timer = 0.0f;
        WeaponPrompt.SpecialAttackInside += SetSpecialAttack;
        Combat.ManageSpecial += ActivateSpecial;
    }

    public void SetSpecialAttack(SpecialAttack special) { 
        this.special = special;
        if (this.special.IsTimed()) { 
            isTimed = true;
            timer = this.special.GetTimer();
        }
    }

    private void ActivateSpecial(float damage) {
        //Debug.Log("Activated");
        if (special != null)
        {
            special.Activate(damage);
            if (isTimed) {
                StartCoroutine(DisableSpecial());
            }
        }
        else {
            Debug.Log("No Special Attack Set.");
        }
    }

    private IEnumerator DisableSpecial() {
        yield return new WaitForSeconds(timer);
        if (special != null)
        {
            special.Deactivate();
        }
        else {
            Debug.Log("Special Deactivation Encountered a problem");
        }
    }
    private void DeactivateSpecial() {
        special.Deactivate();
    }


}

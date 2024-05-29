using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIndicator : MonoBehaviour
{
    private MaterialObtainer materialManager;
    private bool alreadyTransitioning;

    private void Start()
    {
        alreadyTransitioning = false;
        materialManager = GetComponentInChildren<MaterialObtainer>();
        if (materialManager != null) {
            materialManager.obtainMaterial(1.0f);
        }
    }

    public void Indicate() {
        if (!alreadyTransitioning)
        {
            alreadyTransitioning = true;
            materialManager.Indicate();
        }
    }

    public void Reset()
    {
        if (alreadyTransitioning)
        {
            alreadyTransitioning = false;
            materialManager.Reset();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        AbilitiesManager.ActivateIndicator += Indicate;
        AbilitiesManager.DeactivateIndicator += StopIndicating;
    }

    private void Indicate() { 
        gameObject.SetActive(true);
    }

    private void StopIndicating() { 
        gameObject?.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{
    private Image imageElement;
    void Start()
    {
        imageElement = GetComponent<Image>();
        if (imageElement != null) { 
            imageElement.enabled = false;
        }
        Combat.ReceivedDamage += ApplyEffect;
    }

    private void ApplyEffect() {
        if (imageElement != null) {
            imageElement.enabled = true;
        }
        StartCoroutine(StopEffect());
    }

    private IEnumerator StopEffect() {
        yield return new WaitForSeconds(0.2f);
        if (imageElement != null)
        {
            imageElement.enabled = false;
        }
    }
}

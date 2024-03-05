using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFader : MonoBehaviour
{
    public bool doFade;
    private Material material;
    float originalOpacity;
    public float fadeSpeed,fadeAmount;

    private void Start()
    {
        doFade = false;
        material = GetComponent<Renderer>().material;
        originalOpacity = material.color.a;
    }

    private void Update()
    {
        if (doFade)
        {
            TriggerFade();
        }
        else {
            ResetFade();
        }
    }

    private void TriggerFade() { 
        Color currentColor = material.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, fadeAmount, fadeSpeed * Time.deltaTime));

        material.color = smoothColor;
    }

    private void ResetFade() {
        Color currentColor = material.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, originalOpacity, fadeSpeed * Time.deltaTime));

        material.color = smoothColor;
    }
}

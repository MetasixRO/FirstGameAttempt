using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialObtainer : MonoBehaviour
{
    private Material material;
    private Color originalColor;
    private float transitionDuration;

    public void obtainMaterial(float delay) {
        material = GetComponent<Renderer>().material;
        originalColor = material.color;
        transitionDuration = delay;
    }

    public void Indicate() {
        StartCoroutine(ChangeOverTime(Color.white));
    }

    private IEnumerator ChangeOverTime(Color color) {
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration) {
            float timer = elapsedTime / transitionDuration;

            Color newColor = Color.Lerp(originalColor, color, timer);

            material.color = newColor;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        material.color = color;
    }

    public void Reset()
    {
        if (material.color != originalColor)
        {
            material.color = originalColor;
        }
    }
}

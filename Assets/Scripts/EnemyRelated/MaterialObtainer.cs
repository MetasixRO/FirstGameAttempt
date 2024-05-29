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

    private IEnumerator ChangeOverTime(Color color, bool reset = false) {
        float elapsedTime = 0f;

        if (!reset)
        {
            while (elapsedTime < transitionDuration)
            {
                float timer = elapsedTime / transitionDuration;

                Color newColor = Color.Lerp(originalColor, color, timer);

                material.color = newColor;

                elapsedTime += Time.deltaTime;

                yield return null;
            }
        }

        material.color = color;
    }

    public void Reset()
    {
        StopCoroutine(ChangeOverTime(originalColor));
        StopCoroutine("ChangeOverTime");
        ChangeOverTime(originalColor, true);
        //Debug.Log("Current:" + material.color + " and the original is : " + originalColor);
        if (material.color != originalColor)
        {
            //Debug.Log("Reseting");
            material.color = originalColor;
        }
    }
}

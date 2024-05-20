using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingManager : MonoBehaviour
{
    private PostProcessVolume postProcessVolume;
    private Vignette vignette;

    void Start()
    {
        HealthBar.ManageVignette += ManageVignette;
        postProcessVolume = FindObjectOfType<PostProcessVolume>();
        if (postProcessVolume.profile.TryGetSettings(out vignette))
        {
            vignette.intensity.value = 0.5f;
            vignette.enabled.value = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("y")) {
            vignette.enabled.value = false;
        }
    }

    private void ManageVignette(bool value) {
        vignette.enabled.value = value;
    }
}

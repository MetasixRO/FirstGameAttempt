using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("[")) {
            Time.timeScale = 0.25f;
        }

        if (Input.GetKeyDown("]")){
            Time.timeScale = 1.0f;
        }
    }
}

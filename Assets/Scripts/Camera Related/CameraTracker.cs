using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    #region Singleton
    public static CameraTracker instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public new GameObject camera;

}

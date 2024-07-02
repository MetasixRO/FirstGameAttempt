using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    public delegate void Reseter();
    public static event Reseter EventR;

    private void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            EventR?.Invoke();
        }
    }
}

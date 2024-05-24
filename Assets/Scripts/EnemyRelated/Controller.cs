using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public virtual void Freeze() { Debug.Log("This one is called"); }

    public abstract void Movement();

    public void ManageAgent() {
        gameObject.SetActive(true);
    }
}

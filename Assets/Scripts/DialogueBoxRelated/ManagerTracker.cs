using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerTracker : MonoBehaviour
{
    #region Singleton

    public static ManagerTracker instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public DialogueManager dialogueManager;
}

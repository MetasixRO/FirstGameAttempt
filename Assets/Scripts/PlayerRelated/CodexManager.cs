using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CodexManager : MonoBehaviour
{
    private CodexElement[] codexElements;

    public delegate void CodexManagerEvent(int npcID);
    public static event CodexManagerEvent RequestInfoById;

    public delegate void CodexManagement();
    public static event CodexManagement OpenCodexEvent;
    public static event CodexManagement CloseCodexEvent;

    private Animator animator;

    private void Start()
    {
        LayerMask layer = LayerMask.GetMask("NPCLayer");
        codexElements = GetAllChildrenWithLayer(layer);

        animator = GetComponent<Animator>();


        newAbilityMenuState.CloseCodex += CloseCodex;
        FriendshipTracker.ResponseWithInfo += SetData;
        NewLobbyState.OpenCodex += OpenCodex;
    }

    private CodexElement[] GetAllChildrenWithLayer(LayerMask layer) {
        var result = new List<CodexElement>();

        foreach (Transform child in transform)
        {
            if (((1 << child.gameObject.layer) & layer) != 0)
            {
                result.Add(child.gameObject.GetComponent<CodexElement>());
            }
        }
        return result.ToArray();
    }

    private void OpenCodex() {
        animator.ResetTrigger("Close");
        animator.SetTrigger("Open");
        for (int i = 0; i < codexElements.Length; i++) {
            if (RequestInfoById != null) {
                RequestInfoById(i);
            }
        }

        if (OpenCodexEvent != null) {
            OpenCodexEvent();
        }
        
        
    }

    private void CloseCodex() {
        animator.ResetTrigger("Open");
        animator.SetTrigger("Close");
        if (CloseCodexEvent != null) {
            CloseCodexEvent();
        }

        

        
    }

    private void SetData(int id, string name, int rank) {
        codexElements[id].SetData(name, rank);
    }
}

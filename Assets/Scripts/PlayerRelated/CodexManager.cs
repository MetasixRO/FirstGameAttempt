using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodexManager : MonoBehaviour
{
    private CodexElement[] codexElements;

    public delegate void CodexManagerEvent(int npcID);
    public static event CodexManagerEvent RequestInfoById;

    private void Start()
    {
        LayerMask layer = LayerMask.GetMask("NPCLayer");
        codexElements = GetAllChildrenWithLayer(layer);

        FriendshipTracker.ResponseWithInfo += SetData;
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
        for (int i = 0; i < codexElements.Length; i++) {
            if (RequestInfoById != null) {
                RequestInfoById(i);
            }
            break;
        }
    }

    private void SetData(int id, string name, int rank) {
        codexElements[id].SetData(name, rank);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendshipTracker : MonoBehaviour
{
    public delegate void FriendshipTrackerEvent(int id, string name, int rank);
    public static event FriendshipTrackerEvent ResponseWithInfo;

    private Dictionary<int,int> friendshipStatus = new Dictionary<int,int>();
    private Dictionary<int, string> npcNames = new Dictionary<int,string>();

    private void Awake()
    {
        DialogueTrigger.DialogueStatus += ModifyFriendshipStatus;
        CodexManager.RequestInfoById += SendInfoById;
    }

    private void ModifyFriendshipStatus(int npcID,string npcName, int currentStatus) {
        if (friendshipStatus.ContainsKey(npcID))
        {
            friendshipStatus[npcID] = currentStatus;
            npcNames[npcID] = npcName;
        }
        else { 
            friendshipStatus.Add(npcID, currentStatus);
            npcNames.Add(npcID, npcName);
        }

        /*
        foreach (int key in friendshipStatus.Keys) {
            Debug.Log("ID: " + key);
            Debug.Log("Friendship status: " + friendshipStatus[key]);
            Debug.Log("Name: " + npcNames[key]);
        }
       */ 
    }

    private void SendInfoById(int id) {
            if (ResponseWithInfo != null && npcNames.ContainsKey(id+1)) {
            ResponseWithInfo(id, npcNames[id+1], friendshipStatus[id+1]);
        }
    }
}

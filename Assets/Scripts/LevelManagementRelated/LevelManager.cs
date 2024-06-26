using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public delegate void LevelManagerEvent(int rewardNumber);
    public static event LevelManagerEvent SelectNextReward;
    public static event LevelManagerEvent EnterNextArena;
    public static event LevelManagerEvent ActivateNextSpawner;
    public static event LevelManagerEvent ObtainCurrentDoors;

    public delegate void LevelManagerEmptyEvent();
    public static event LevelManagerEmptyEvent ReachingNewArena;

    public delegate void ClearActiveRewards();
    public static event ClearActiveRewards ClearRewards;

    private int currentChamber;
    private int nextReward;

    private bool firstCheck = true;

    private List<int> everyArenaID = new List<int>();
    private List<int> availableArenas = new List<int>();

    //DEBUG ONLY
    private int numberOfDoors;
    public GameObject[] chamberSpawners;
    public Sprite[] rewards;
    private List<UnlockableDoor> currentDoors = new List<UnlockableDoor>();
    //DEBUG ONLY

    private void Start()
    {
        EnemiesClearedEvent.EnemiesCleared += PrepareNextChambers;
        currentChamber = -1;
        nextReward = -1;

        CloseArenaDoor.CloseDoor += ActivateSpawner;
        ReachedChamber.ReachedNewChamber += ActivateSpawner;

        DoorOpener.GoToChamber += TeleportToArenaBasedOnID;

        UnlockableDoor.ReturnReference += SetCurrentDoors;

        UnlockableDoor.AdvanceChambers += AdvanceChamberAndSetReward;
    }

    private void Update()
    {
        if (Input.GetKeyDown("1")) {
            EnterNextArena?.Invoke(1);
            ActivateNextSpawner?.Invoke(1);
            ReachingNewArena?.Invoke();
        }
        if (Input.GetKeyDown("2"))
        {
            EnterNextArena?.Invoke(2);
            ActivateNextSpawner?.Invoke(2);
            ReachingNewArena?.Invoke();
        }
        if (Input.GetKeyDown("3"))
        {
            EnterNextArena?.Invoke(3);
            ActivateNextSpawner?.Invoke(3);
            ReachingNewArena?.Invoke();
        }
    }

    private void TeleportToArenaBasedOnID(int arenaID) {
        ClearRewards?.Invoke();
        EnterNextArena?.Invoke(arenaID);
        ReachingNewArena?.Invoke();
        ActivateNextSpawner?.Invoke(arenaID);
        currentChamber = arenaID;
        if (firstCheck) {
            for (int i = 1; i < ArenaTeleporter.GetGeneralID(); i++) { 
                everyArenaID.Add(i);
            }
            firstCheck = false;
        }
    }

    //PROBABLY DELETE THIS
    private void ActivateSpawner() {
        currentChamber++;
        chamberSpawners[currentChamber].GetComponent<EnemySpawner>().ActivateSpawner();
    }

    private void PrepareNextChambers() {
        currentDoors.Clear();
        ObtainCurrentDoors?.Invoke(currentChamber);

        foreach(var door in currentDoors) {
            int reward = Random.Range(0, 5);
            door.EnableInteractable();
            door.EnableRewardImage(rewards[reward], reward);
        }
    }

    private void SetCurrentDoors(int doorID, UnlockableDoor doorObject) {
        currentDoors.Add(doorObject);
    }

    private void AdvanceChamberAndSetReward(int arenaID, int rewardID) {
        nextReward = rewardID;
        SelectNextReward?.Invoke(nextReward);
        availableArenas.Clear();
        availableArenas = everyArenaID.FindAll(element => !element.Equals(arenaID));
        if (availableArenas.Count != 0) {
            int newArenaIndex = Random.Range(0, availableArenas.Count);
            //Debug.Log("Next arena is : " + newArenaIndex + " And the reward will be " + rewards[nextReward]);
            TeleportToArenaBasedOnID(availableArenas[newArenaIndex]);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public delegate void LevelManagerEvent(int rewardNumber);
    public static event LevelManagerEvent SelectNextReward;

    private int currentChamber;
    private int nextReward;
    //DEBUG ONLY
    private int numberOfDoors;
    public GameObject[] chamber1Doors;
    public GameObject[] chamber2Doors;
    public GameObject[] chamber3Doors;
    public GameObject[] chamberSpawners;
    public Sprite[] rewards;
    //DEBUG ONLY

    private void Start()
    {
        ClearAllImages();
        EnemiesClearedEvent.EnemiesCleared += PrepareNextRewards;
        currentChamber = -1;
        nextReward = -1;
        //DEBUG ONLY VALUE
        numberOfDoors = 2;
        Debug.Log("The number of doors for each chamber is now set to 2. JUST FOR DEBUG");

        CloseArenaDoor.CloseDoor += ActivateSpawner;
        ReachedChamber.ReachedNewChamber += ActivateSpawner;
    }

    private void ActivateSpawner() {
        currentChamber++;
        chamberSpawners[currentChamber].GetComponent<EnemySpawner>().ActivateSpawner();
    }

    private bool GetIfShopOpen() {
        return Random.value < 0.5f;
    }

    private void PrepareNextRewards() {
        nextReward = Random.Range(1, 6);
        if (SelectNextReward != null) {
            SelectNextReward(nextReward);
        }

        bool isShopOpen = GetIfShopOpen();
        switch (currentChamber) {
            case 0:
                chamber1Doors[0].GetComponentInChildren<Image>().enabled = true;
                chamber1Doors[0].GetComponentInChildren<Image>().sprite = rewards[nextReward];
                if (isShopOpen)
                {
                    chamber1Doors[1].GetComponentInChildren<Image>().enabled = true;
                    chamber1Doors[1].GetComponentInChildren<Image>().sprite = rewards[nextReward];
                }
                break;
            case 1:
                chamber2Doors[0].GetComponentInChildren<Image>().enabled = true;
                chamber2Doors[0].GetComponentInChildren<Image>().sprite = rewards[nextReward];
                if (isShopOpen)
                {
                    chamber2Doors[1].GetComponentInChildren<Image>().enabled = true;
                    chamber2Doors[1].GetComponentInChildren<Image>().sprite = rewards[nextReward];
                }
                break;
            case 2:
                if (isShopOpen) {
                    chamber3Doors[0].GetComponentInChildren<Image>().enabled = true;
                    chamber3Doors[0].GetComponentInChildren<Image>().sprite = rewards[nextReward];
                }
                break;
        }
    }

    private void ClearAllImages() {
        chamber1Doors[0].GetComponentInChildren<Image>().enabled = false;
        chamber1Doors[1].GetComponentInChildren<Image>().enabled = false;
        chamber2Doors[0].GetComponentInChildren<Image>().enabled = false;
        chamber2Doors[1].GetComponentInChildren<Image>().enabled = false;
        chamber3Doors[0].GetComponentInChildren<Image>().enabled = false;
    }
}

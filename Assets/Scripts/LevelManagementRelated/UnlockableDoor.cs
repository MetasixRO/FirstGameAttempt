using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockableDoor : MonoBehaviour
{
    public delegate void UnlockableDoorEvent(int id, UnlockableDoor doorReference);
    public static event UnlockableDoorEvent ReturnReference;

    public delegate void UnlockableDoorUsedEvent(int arenaID, int rewardID);
    public static event UnlockableDoorUsedEvent AdvanceChambers;

    private int correspondingArenaID;
    private int doorID;

    private Image rewardImage;
    private int rewardNumber;
    private NextChamberDoor interactableComponent;

    private void Start()
    {
        rewardImage = GetComponentInChildren<Image>();
        rewardImage.enabled = false;
        interactableComponent = GetComponent<NextChamberDoor>();
        interactableComponent.SetInteractable(false);
        //LevelManager.SetupNextChambers += UpdateRewards;
        LevelManager.ObtainCurrentDoors += SendThisDoor;

        NextChamberDoor.NewChamber += CheckDoorAndSendEvent;

        newDeadState.RespawnPlayer += ResetDoors;

        LevelManager.EnterNextArena += ResetDoorsFromAdvancing;
    }

    public void SetDoorInfo(int arenaID, int doorID) {
        this.doorID = doorID;
        correspondingArenaID = arenaID;
    }

    private void UpdateRewards(Sprite rewardSprite, int currentArena) {
        if (correspondingArenaID == currentArena) {
            rewardImage.enabled = true;
            rewardImage.sprite = rewardSprite;
            interactableComponent.enabled = true;
        }
    }

    public void EnableRewardImage(Sprite rewardSprite, int rewardNumber) {
        rewardImage.enabled = true;
        Debug.Log("(" + doorID + ") This door has " + rewardSprite + "(" + rewardNumber + ")");
        rewardImage.sprite = rewardSprite;
        this.rewardNumber = rewardNumber;
    }

    public void EnableInteractable() {
        interactableComponent.SetInteractable(true);
    }

    public void DisableInteractable() {
        interactableComponent.SetInteractable(false);
    }

    private void SendThisDoor(int chamberID) {
        if (chamberID == correspondingArenaID) {
            ReturnReference?.Invoke(doorID, this);
        }
    }

    private void CheckDoorAndSendEvent(NextChamberDoor instance) {
        if (instance == interactableComponent) {
            AdvanceChambers?.Invoke(correspondingArenaID, rewardNumber);
        }
    }

    private void ResetDoors() {
        interactableComponent.SetInteractable(false);
        rewardImage.enabled = false;
    }

    private void ResetDoorsFromAdvancing(int id) {
        ResetDoors();
    }
}

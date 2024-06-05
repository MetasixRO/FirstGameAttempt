using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour, IInteractable
{
    public delegate void WeaponSelectorDoorEvent(int chamber);
    public static event WeaponSelectorDoorEvent GoToChamber;

    public delegate void ChallengeBeginningEvent();
    public static event ChallengeBeginningEvent AnnounceStart;

    private bool usable;

    public string prompt;
    //private Animator animator;

    private void Start()
    {
        usable = false;

        WeaponPrompt.ChangeWeapon += ActivateDoor;

        newDeadState.RespawnPlayer += ResetDoor;
    }

    public void Interact()
    {
        if (usable)
        {
            int firstArenaID;
            firstArenaID = Random.Range(1, ArenaTeleporter.GetGeneralID());
            GoToChamber?.Invoke(firstArenaID);
            AnnounceStart?.Invoke();
        }
    }

    public string InteractionPrompt()
    {
        if (prompt != null)
        {
            return prompt;
        }
        else
            return "DefaultText";
    }

    private void ResetDoor() {
        usable = false;
    }

    private void ActivateDoor(int weaponID) {
        usable = true;
    }
}

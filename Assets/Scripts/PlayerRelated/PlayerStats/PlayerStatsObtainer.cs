using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsObtainer : MonoBehaviour
{
    public PlayerStats statsComponent;

    private void Awake()
    {
        statsComponent.currentMaxHealth = statsComponent.originalMaxHealth;
        statsComponent.temporaryMaxHealth = statsComponent.originalMaxHealth;
    }

    private void Start()
    {
        newDeadState.RespawnPlayer += Reset;
    }

    public float ObtainOriginalMaxHealth() {
        return statsComponent.originalMaxHealth;
    }

    public float ObtainCurrentMaxHealth() {
        return statsComponent.currentMaxHealth;
    }

    public float ObtainTemporaryMaxHealth() {
        return statsComponent.temporaryMaxHealth;
    }

    public void SetCurrentMaxHealth(float maxHealth) { 
        statsComponent.currentMaxHealth = maxHealth;
    }

    public void SetTemporaryMaxHealth(float maxHealth) {
        statsComponent.temporaryMaxHealth = maxHealth;
    }

    private void Reset()
    {
        statsComponent.temporaryMaxHealth = statsComponent.currentMaxHealth;
    }
}

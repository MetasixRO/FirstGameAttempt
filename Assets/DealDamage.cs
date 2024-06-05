using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{

    public delegate void DealtDamageEvent();
    public static event DealtDamageEvent EnemyHit;
    public static event DealtDamageEvent NoEnemyHit;

    private bool canDealDamage;
    private bool isDashing;
    private float attackDamage;

    private bool shouldIncrease;
    private bool isDoubled, knifeDoubled;
    private float hitPointsToIncreaseBy;
    private float firstHitBoostPercentage = 0.0f;

    private BoxCollider currentCollider;

    private void Start()
    {
        shouldIncrease = false;
        attackDamage = 0;
        canDealDamage = false;
        isDashing = false;
        isDoubled = false;
        knifeDoubled = false;
        Combat.ManageWeapon += ManageWeaponDamageDealing;
        CharacterMovement.Dash += SetIsDashing;
        NewDash.DashDone += ResetIsDashing;

        FieryPresence.IncreaseFirstHitDamage += AddFirstHitDamageBoost;

        HighConfidence.IncreaseDamage += ModifyDamage;
        HighConfidence.ReduceDamage += ResetDamage;

        DoubleDamage.DamageDouble += DoubleCurrentDamage;
        DoubleDamage.DamageReset += ResetCurrentDamage;

        KnifeSpecial.BoostStats += KnifeDoubleCurrentDamage;
        KnifeSpecial.ResetStats += KnifeResetCurrentDamage;

        NewLobbyState.ReachedLobby += ResetDamage;

        currentCollider = GetComponent<BoxCollider>();
        currentCollider.enabled = false;
    }

    private void ManageWeaponDamageDealing(float damage) {
        if (gameObject.activeSelf)
        {
            //Debug.Log("Set");
            if (attackDamage == 0)
            {
                attackDamage = damage;
            }

            if (shouldIncrease) {
                attackDamage += hitPointsToIncreaseBy;
                if (isDoubled) {
                    attackDamage += hitPointsToIncreaseBy;
                }
                if (knifeDoubled) {
                    attackDamage += hitPointsToIncreaseBy;
                }
                shouldIncrease = false;
               // Debug.Log(attackDamage + " " + shouldIncrease);
            }

            if (!canDealDamage)
            {
                if (!isDashing)
                {
                    currentCollider.enabled = true;
                    canDealDamage = true;
                    NoEnemyHit?.Invoke();
                }
            }
            else
            {
                currentCollider.enabled = false;
                canDealDamage = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canDealDamage && other.CompareTag("Enemy") && gameObject.activeSelf) {
            EnemyCombat combatComponent = other.GetComponent<EnemyCombat>();

            //Debug.Log(attackDamage);
            //Debug.Log(firstHitBoostPercentage + " Salut " + combatComponent.IsUntouched());
            if (firstHitBoostPercentage != 0.0 && combatComponent.IsUntouched())
            {
                //Debug.Log(attackDamage * firstHitBoostPercentage);
                combatComponent.TakeSecondaryDamage(attackDamage * firstHitBoostPercentage);
            }
            combatComponent.TakeDamage(attackDamage);

            EnemyHit?.Invoke();
        }
    }

    private void SetIsDashing()
    {
        isDashing = true;
        currentCollider.enabled = false;
        canDealDamage = false;
    }

    private void ResetIsDashing() {
        isDashing = false;
    }

    private void ModifyDamage(int hitPoints) {
        shouldIncrease = true;
        hitPointsToIncreaseBy = hitPoints;
    }

    private void ResetDamage(int hitPoints) {
        shouldIncrease = false;
        attackDamage -= hitPointsToIncreaseBy;
        if (isDoubled) {
            attackDamage -= hitPointsToIncreaseBy;
        }
        if (knifeDoubled) {
            attackDamage -= hitPointsToIncreaseBy * 2;
        }
    }
    private void KnifeDoubleCurrentDamage() {
        knifeDoubled = true;
        DoubleCurrentDamage();
    }

    private void KnifeResetCurrentDamage() { 
        ResetCurrentDamage();
    }


    private void DoubleCurrentDamage() {
        attackDamage *= 2;
        if (!knifeDoubled)
        {
            isDoubled = true;
        }
        //Debug.Log("" + attackDamage);
    }

    private void ResetCurrentDamage() {
        if (!knifeDoubled)
        {
            isDoubled = false;
        }
        else {
            knifeDoubled = false;
        }
        attackDamage /= 2;
    }
    
    private void AddFirstHitDamageBoost(int percentage) {
        switch (percentage) {
            case 15: firstHitBoostPercentage = 0.15f; break;
            case 30: firstHitBoostPercentage = 0.30f; break;
            case 45: firstHitBoostPercentage = 0.45f; break;
            case 60: firstHitBoostPercentage = 0.60f; break;
            case 75: firstHitBoostPercentage = 0.75f; break;
        }
    }

    private void ResetDamage() {
        attackDamage = 0;
    }
    

}

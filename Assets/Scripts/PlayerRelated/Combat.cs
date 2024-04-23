using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{

    public delegate void ModifyHealthbar(float health);
    public static event ModifyHealthbar SetHealth;
    public static event ModifyHealthbar SetMaxHealth;

    public delegate void ManageDealDamage(float damage = 0);
    public static event ManageDealDamage ManageWeapon;
    public static event ManageDealDamage ManageSpecial;

    public delegate void HealthRelatedEvent();
    public static event HealthRelatedEvent PlayerDead;
    public static event HealthRelatedEvent PlayerDefiedDeath;
    public static event HealthRelatedEvent PlayerLowerThanPercentage;
    public static event HealthRelatedEvent PlayerHigherThanPercentage;


    [SerializeField] private float maxHealth = 100;
    private float currentHealth;

    private float attackDamage, specialAttackDamage;
    private bool canAttack, canSpecial;
    private float attackCooldown, specialAttackCooldown;
    private bool attackPressed, specialPressed;

    Animator animator;
    int isAttackingHash;

    private int timesCanDefyDeath = 0;


    private void Awake()
    {
        canAttack = false;
        canSpecial = false;
    }


    void Start()
    {
        DeathDefianceAbility.DefyDeath += SetDeathDefiance;
        ThickSkinAbility.IncreaseMaxHealth += IncreaseMaxHealth;


        newDeadState.RespawnPlayer += FillHealth;
        HealthRestorer.MedpackHeal += RestoreHealth;
        RegenerateAbility.AddVitality += RestoreHealth;
        HealInteractable.InteractedHeal += RestoreHealth;
        WeaponPrompt.ChangeWeaponStats += ChangeStatsForWeapon;
        EnemyDealDamage.PlayerReceiveDamage += TakeDamage;
        Bullet.Hit += TakeDamage;
        DashDealDamage.PlayerReceiveDamage += TakeDamage;

        DoubleMaxHealth.DoubleHealth += ModifyMaxHealth;
        DoubleMaxHealth.ResetHealth += ResetMaxHealth;

        KnifeSpecial.BoostStats += ModifyMaxHealth;
        KnifeSpecial.ResetStats += ResetMaxHealth;

        animator = GetComponent<Animator>();
        isAttackingHash = Animator.StringToHash("isAttacking");

        currentHealth = maxHealth;
        if (SetHealth != null)
        {
            SetHealth(currentHealth);
        }
    }


    void Update()
    {
        if (attackPressed && canAttack)
        {
            handleAttack();
        }

        if (specialPressed && canSpecial) {
            handleSpecial();
        }

        //DEBUG***************************
        if (Input.GetKeyDown("p")) {
            TakeDamage(90);
        }

        if (Input.GetKeyDown("i")) {
            //maxHealth = 9999;
            //currentHealth = 9999;
            Debug.Log("I have removed godmode from this debug key.");
        }
        //DEBUG***************************
    }

    private void ManageWeaponDamageAbility() {
        if (ManageWeapon != null)
        {
            ManageWeapon(attackDamage);
        }
    }

    private void ManageWeaponSpecialDamage() {
        if (ManageSpecial != null)
        {
            ManageSpecial(specialAttackDamage);
        }
    }

    void handleAttack()
    {
        canAttack = false;
        animator.SetTrigger("isAttacking");
    }

    void handleSpecial()
    {
        canSpecial = false;
        animator.SetTrigger("Special");
    }


    private void ChangeStatsForWeapon(float damage, float cooldown, float specialDamage, float specialCooldown) {
        attackDamage = damage;
        attackCooldown = cooldown;
        specialAttackDamage = specialDamage;
        specialAttackCooldown = specialCooldown;
        canAttack = true;
        canSpecial = true;
    }


    IEnumerator resetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    IEnumerator resetSpecialCooldown()
    {
        yield return new WaitForSeconds(specialAttackCooldown);
        canSpecial = true;
    }


    public void TakeDamage(float damage)
    {
        
        if (currentHealth - damage < 0)
        {
            currentHealth = 0;
        }
        else {
            currentHealth -= damage;
        }

        if (SetHealth != null)
        {
            SetHealth(currentHealth);
        }

        CheckPercentage();

        if (currentHealth <= 0)
        {
            if (timesCanDefyDeath > 0)
            {
                timesCanDefyDeath--;
                RestoreHealth(50);
                if (PlayerDefiedDeath != null) {
                    PlayerDefiedDeath();
                }
                //Debug.Log(timesCanDefyDeath);
            }
            else
            {
                Die();
            }
        }
    }

    void Die()
    {
        if (PlayerDead != null) {
            PlayerDead();
        }
        animator.SetTrigger("isDead");


        GetComponent<Collider>().enabled = false;
    }

    public void RestoreHealth(int amount)
    {
        if (currentHealth + amount > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }

        if (SetHealth != null)
        {
            SetHealth(currentHealth);
        }
        CheckPercentage();
    }

    public void ReceiveAttackButtonStatus(bool receivedAttackPressed, bool receivedSpecialPressed) { 
        attackPressed = receivedAttackPressed;
        specialPressed = receivedSpecialPressed;
    }

    private void IncreaseMaxHealth(int amount) {
        maxHealth = maxHealth - (amount - 5);
        maxHealth += amount;
        currentHealth = maxHealth;

        if (SetMaxHealth != null)
        {
            SetMaxHealth(maxHealth);
        }

        if (SetHealth != null)
        {
            SetHealth(currentHealth);
        }
    }

    private void ModifyMaxHealth() {
        maxHealth *= 2;
        if (SetMaxHealth != null) { 
            SetMaxHealth(maxHealth);
        }
        RestoreHealth((int)currentHealth);
    }

    private void ResetMaxHealth() {
        maxHealth /= 2;
        if (SetMaxHealth != null)
        {
            SetMaxHealth(maxHealth);
        }
        TakeDamage(currentHealth / 2);
    }

    private void FillHealth() {
        RestoreHealth((int)(maxHealth - currentHealth));
    }

    private void SetDeathDefiance(int numberOfTimes) {
        timesCanDefyDeath = numberOfTimes;
        //Debug.Log(timesCanDefyDeath);
    }

    private void CheckPercentage() {
        if (currentHealth < 0.8f * maxHealth)
        {
            if (PlayerLowerThanPercentage != null)
            {
                PlayerLowerThanPercentage();
            }
        }
        else {
            if (PlayerHigherThanPercentage != null) {
                PlayerHigherThanPercentage();
            }
        }
    }
}

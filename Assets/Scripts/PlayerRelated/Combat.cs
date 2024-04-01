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

    public delegate void EnterDeathState();
    public static event EnterDeathState PlayerDead;


    [SerializeField] private float maxHealth = 100;
    private float currentHealth;

    private float attackDamage;
    private bool canAttack;
    private float attackCooldown;
    private bool attackPressed;

    Animator animator;
    int isAttackingHash;


    private void Awake()
    {
        canAttack = false;
    }


    void Start()
    {
        newDeadState.RespawnPlayer += FillHealth;
        HealthRestorer.MedpackHeal += RestoreHealth;
        WeaponPrompt.ChangeWeaponStats += ChangeStatsForWeapon;
        EnemyDealDamage.PlayerReceiveDamage += TakeDamage;

        DoubleMaxHealth.DoubleHealth += ModifyMaxHealth;
        DoubleMaxHealth.ResetHealth += ResetMaxHealth; 

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

        //DEBUG***************************
        if (Input.GetKeyDown("p")) {
            TakeDamage(90);
        }

        if (Input.GetKeyDown("i")) {
            maxHealth = 9999;
            currentHealth = 9999;
        }
        //DEBUG***************************
    }

    private void ManageWeaponDamageAbility() {
        if (ManageWeapon != null)
        {
            ManageWeapon(attackDamage);
        }
    }

    void handleAttack()
    {
        canAttack = false;
        animator.SetTrigger("isAttackingDeprecated");
    }


    private void ChangeStatsForWeapon(float damage, float cooldown) {
        attackDamage = damage;
        attackCooldown = cooldown;
        canAttack = true;
    }


    IEnumerator resetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (SetHealth != null)
        {
            SetHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
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
    }

    public void ReceiveAttackButtonStatus(bool receivedAttackPressed) { 
        attackPressed = receivedAttackPressed;
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
        TakeDamage(currentHealth / 2);
    }

    private void FillHealth() {
        RestoreHealth((int)(maxHealth - currentHealth));
    }
}

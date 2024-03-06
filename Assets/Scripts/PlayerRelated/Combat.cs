using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{

    public delegate void ModifyHealthbar(float health);
    public static event ModifyHealthbar SetHealth;

    public delegate void ManageDealDamage(float cooldown, float damage = 0);
    public static event ManageDealDamage ManageWeapon;


    [SerializeField] private float maxHealth = 100;
    private float currentHealth;

    private float attackDamage;
    private bool canAttack;
    private float attackCooldown;
    private bool attackPressed;

    Animator animator;
    int isAttackingHash;

    PlayerInput input;


    private void Awake()
    {
        canAttack = false;
        input = new PlayerInput();
        input.CharacterControls.Shoot.performed += ctx => attackPressed = ctx.ReadValueAsButton();
    }


    void Start()
    {
        HealthRestorer.MedpackHeal += RestoreHealth;
        WeaponPrompt.ChangeWeaponStats += ChangeStatsForWeapon;
        EnemyDealDamage.PlayerReceiveDamage += TakeDamage;

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
        bool isAttacking = animator.GetBool(isAttackingHash);

        
        if (attackPressed && canAttack)
        {
            if (ManageWeapon != null) {
                ManageWeapon(attackCooldown, attackDamage);
            }
            handleAttack();
        }

        if (isAttacking)
        {
            animator.SetBool(isAttackingHash, false);
        }
    }

    void handleAttack()
    {
        canAttack = false;
        animator.SetBool(isAttackingHash, true);


        StartCoroutine(resetAttackCooldown());
    }


    private void ChangeStatsForWeapon(float damage, float cooldown) {
        attackDamage = damage;
        attackCooldown = cooldown;
        canAttack = true;
    }


    IEnumerator resetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        if (ManageWeapon != null)
        {
            ManageWeapon(attackCooldown);
        }
        canAttack = true;
        //animator.SetBool(isAttackingHash, false);
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
        //Animation + Disable enemy
        animator.SetBool("isDead", true);


        GetComponent<Collider>().enabled = false;
        GetComponent<CharacterController>().enabled = false;

        this.enabled = false;
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

    void OnEnable()
    {
        input.CharacterControls.Enable();
    }

    void OnDisable()
    {
        input.CharacterControls.Disable();
    }
}

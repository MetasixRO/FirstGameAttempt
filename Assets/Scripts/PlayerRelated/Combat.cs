using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{

    public delegate void ModifyHealthbar(float health);
    public static event ModifyHealthbar SetHealth;


    [SerializeField] private float maxHealth = 100;
    private float currentHealth;

    private float attackDamage;
    private bool canAttack;
    private float attackCooldown;
    private bool attackPressed;

    Animator animator;
    int isAttackingHash;

    public Transform attackPoint;
    public float attackRange = 1.0f;
    public LayerMask enemyLayers;

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
            handleAttack();
        }

        if (!attackPressed && isAttacking)
        {
            animator.SetBool(isAttackingHash, false);
        }
    }

    void handleAttack()
    {
        canAttack = false;
        animator.SetBool(isAttackingHash, true);


        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies) {
            enemy.GetComponent<EnemyCombat>().SetCanReceiveDamage(attackDamage);
        }

        //Incepe o rutina separata (in acelasi Thread) pentru a reseta cooldown
        StartCoroutine(resetAttackCooldown(hitEnemies));
    }


    private void ChangeStatsForWeapon(float damage, float cooldown) {
        attackDamage = damage;
        attackCooldown = cooldown;
        canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWeapon")) {
            TakeDamage(15);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    IEnumerator resetAttackCooldown(Collider[] hitEnemies)
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyCombat>().ClearCanReceiveDamage();
        }
    }


    public void TakeDamage(int damage)
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

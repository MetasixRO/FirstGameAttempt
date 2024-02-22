using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private float attackDamage = 40;
    private bool canAttack = true;
    private float attackCooldown = 1.0f;

    Animator animator;
    int isAttackingHash;

    public Transform attackPoint;
    public float attackRange = 1.0f;
    public LayerMask enemyLayers;

    PlayerInput input;

    bool attackPressed;

    private void Awake()
    {
        canAttack = false;
        input = new PlayerInput();
        input.CharacterControls.Shoot.performed += ctx => attackPressed = ctx.ReadValueAsButton();
    }


    // Start is called before the first frame update
    void Start()
    {
        WeaponPrompt.ChangeWeaponStats += ChangeStatsForWeapon;
        WeaponPrompt.ChangeWeapon += ChangeStance;
        animator = GetComponent<Animator>();
        isAttackingHash = Animator.StringToHash("isAttacking");
    }

    // Update is called once per frame
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

    private void ChangeStance(int weaponID) {
        switch (weaponID)
        {
            case 0:
                animator.SetBool("isGreatSword", true);
                break;
            case 1:
                animator.SetBool("isGreatSword", false);
                break;
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWeapon")) {
            GetComponent<Player>().TakeDamage(15);
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

    void OnEnable()
    {
        input.CharacterControls.Enable();
    }

    void OnDisable()
    {
        input.CharacterControls.Disable();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public int attackDamage = 40;

    public bool canAttack = true;
    public float attackCooldown = 1.0f;

    Animator animator;
    int isAttackingHash;

    //Punctul care va primi damage
    public Transform attackPoint;
    //Zona de damage
    public float attackRange = 0.5f;
    //Fiecare inamic va apartine unui Layer specific
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
        WeaponPrompt.ChangeWeapon += ChangeStatsForWeapon;
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
    {   //Initializeaza delay
        canAttack = false;

        //Porneste animatia
        animator.SetBool(isAttackingHash, true);
        
        //Retine care inamici sunt afectati de atac
        Collider[] hitEnemies =  Physics.OverlapSphere(attackPoint.position,attackRange,enemyLayers);

        //Aplica damage asupra inamicilor
        foreach (Collider enemy in hitEnemies) {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            GetComponent<Player>().TakeDamage(15);
        }

        //Incepe o rutina separata (in acelasi Thread) pentru a reseta cooldown
        StartCoroutine(resetAttackCooldown());
    }

    public void ChangeStatsForWeapon(int weaponNumber) {
        switch (weaponNumber) {
            case 0:
                animator.SetBool("isGreatSword", true);
                attackCooldown = 1;
                attackDamage = 25;
                attackRange = 0.6f;
                canAttack = true;
                break;
            case 1:
                animator.SetBool("isGreatSword", false);
                attackCooldown = 1.2f;
                attackDamage = 50;
                attackRange = 0.7f;
                canAttack = true;
                break;
        }
    }

    //Functie pentru DEBUG : indica zona de atac
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator resetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
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

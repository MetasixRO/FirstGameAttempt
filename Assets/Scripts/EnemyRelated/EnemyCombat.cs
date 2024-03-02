using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    private bool canReceiveDamage;
    private float damageReceived;

    private Animator animator;
    public float maxHealth;
    float currentHealth;
    private EnemyHealthBar healthBar;

    void Start()
    {
        canReceiveDamage = false;
        damageReceived = 0;

        EnemyStats stats = GetComponent<EnemyStats>();
        maxHealth = stats.GetHealth();

        healthBar = GetComponentInChildren<EnemyHealthBar>();

        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        animator = GetComponent<Animator>();
    }

    public void SetCanReceiveDamage(float damageAmount) { 
        canReceiveDamage=true;
        if (damageReceived == 0) { 
            damageReceived = damageAmount;
        }
    }

    public void ClearCanReceiveDamage() {
        canReceiveDamage = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerWeapon") && canReceiveDamage) {
            TakeDamage(damageReceived);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        animator.SetBool("isDead", true);

        GetComponent<Collider>().enabled = false;

        this.enabled = false;
        StartCoroutine(despawnEnemy());
    }


    IEnumerator despawnEnemy()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
}

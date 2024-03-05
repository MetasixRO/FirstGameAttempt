using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCombat : MonoBehaviour
{

    private Animator animator;
    public float maxHealth;
    float currentHealth;
    private EnemyHealthBar healthBar;

    void Start()
    {
        EnemyStats stats = GetComponent<EnemyStats>();
        
        maxHealth = stats.GetHealth();

        GetComponentInChildren<EnemyDealDamage>().SetDamage(stats.GetDamage());

        healthBar = GetComponentInChildren<EnemyHealthBar>();

        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        animator = GetComponent<Animator>();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int enemyID;
    private Animator animator;
    public float maxHealth;
    float currentHealth;
    private EnemyHealthBar healthBar;

    void Start()
    {
        EnemyStats stats = GetComponent<EnemyStats>();
        enemyID = stats.GetId();
        maxHealth = stats.GetHealth();

        healthBar = GetComponentInChildren<EnemyHealthBar>();

        currentHealth = maxHealth;
        if (healthBar != null) {
            healthBar.SetHealth(currentHealth);
        }

        animator = GetComponent<Animator>();
    }


    //declar o functie publica TakeDamage (pentru a putea fi apelata din script-ul Combat)
    public void TakeDamage(float damage) { 
        currentHealth -= damage;

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0) {
            Die();
        }
    }


    void Die() {
        animator.SetBool("isDead", true);

        GetComponent<Collider>().enabled = false;

        this.enabled = false;
        StartCoroutine(despawnEnemy());
    }


    IEnumerator despawnEnemy() {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
}

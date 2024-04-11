using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCombat : MonoBehaviour
{


    public delegate void EnemyEvent();
    public static event EnemyEvent EnemyDead;

    private Animator animator;
    public float maxHealth;
    float currentHealth;
    private EnemyHealthBar healthBar;
    private ParticleSystem particles;
    private Knockback knockbackObject;

    void Start()
    {
        EnemyStats stats = GetComponent<EnemyStats>();
        
        maxHealth = stats.GetHealth();

        GetComponentInChildren<EnemyDealDamage>().SetDamage(stats.GetDamage());

        healthBar = GetComponentInChildren<EnemyHealthBar>();

        particles = GetComponentInChildren<ParticleSystem>();

        knockbackObject = GetComponent<Knockback>();

        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            maxHealth = 1;
            currentHealth = 0;
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (particles != null) {
            particles.Play();
        }

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if(knockbackObject != null)
        {
            knockbackObject.ApplyKnockback();
        }
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void SetDead() {
        animator.SetBool("isDead", true);
    }


    void Die()
    {
        animator.SetTrigger("Die");

        GetComponent<Collider>().enabled = false;
        GetComponent<AgentController>().enabled = false;
        GetComponent<CharacterController>().enabled = false;

        if (EnemyDead != null) {
            EnemyDead();
        }

        this.enabled = false;
        StartCoroutine(despawnEnemy());
    }


    IEnumerator despawnEnemy()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
}

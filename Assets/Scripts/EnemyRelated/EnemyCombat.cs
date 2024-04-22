using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCombat : MonoBehaviour
{


    public delegate void EnemyEvent();
    public static event EnemyEvent EnemyDead;

    private AgentAnimations animations;
    public float maxHealth;
    float currentHealth;
    private EnemyHealthBar healthBar;
    private ParticleSystem particles;
    private Knockback knockbackObject;

    private void Start()
    {
        EnemyStats stats = GetComponent<EnemyStats>();

        maxHealth = stats.GetHealth();

        healthBar = GetComponentInChildren<EnemyHealthBar>();

        particles = GetComponentInChildren<ParticleSystem>();

        knockbackObject = GetComponent<Knockback>();

        animations = GetComponent<AgentAnimations>();
        animations.SetDelay(stats.GetDelay());

        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(currentHealth);
            healthBar.SetHealth(currentHealth);
        }

        GetComponentInChildren<EnemyDealDamage>().SetDamage(stats.GetDamage());
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

        if (animations != null) {
            animations.HandlePunched();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (animations != null) {
            animations.HandleDeath();
        }

        GetComponent<Collider>().enabled = false;
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

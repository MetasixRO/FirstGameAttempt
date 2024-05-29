using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCombat : MonoBehaviour
{
    public delegate void EnemyCombatEvent(GameObject enemyInstance);
    public static event EnemyCombatEvent EnemyToDelete;

    public delegate void EnemyEvent();
    public static event EnemyEvent EnemyDead;

    private AgentAnimations animations;
    public float maxHealth;
    protected float currentHealth;
    protected EnemyHealthBar healthBar;
    private ParticleSystem particles;
    private Knockback knockbackObject;

    private bool canReceiveDamage;

    private void Start()
    {

        canReceiveDamage = true;

        healthBar = GetComponentInChildren<EnemyHealthBar>();

        particles = GetComponentInChildren<ParticleSystem>();

        knockbackObject = GetComponent<Knockback>();

        animations = GetComponent<AgentAnimations>();

        EnemyStart();
    }

    public virtual void EnemyStart() {
        EnemyStats stats = GetComponent<EnemyStats>();

        maxHealth = stats.GetHealth();

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
            
            TakeDamage(999);
        }
    }

    public void TakeDamage(float damage)
    {
        if (canReceiveDamage)
        {
            canReceiveDamage = false;
            StartCoroutine(ResetCanReceiveDamage());
            currentHealth -= damage;

            if (particles != null)
            {
                particles.Play();
            }

            if (healthBar != null)
            {
                healthBar.SetHealth(currentHealth);
            }

            if (knockbackObject != null)
            {
                knockbackObject.ApplyKnockback();
            }

            if (animations != null)
            {
                animations.HandlePunched();
            }

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        if (animations != null) {
            animations.HandleDeath();
        }

        GetComponent<BoxCollider>().enabled = false;
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
        EnemyToDelete?.Invoke(gameObject);
    }

    public bool IsUntouched() {
        return (currentHealth == maxHealth);
    }

    private IEnumerator ResetCanReceiveDamage()
    {
        yield return new WaitForSeconds(0.5f);
        canReceiveDamage = true;
    }
}

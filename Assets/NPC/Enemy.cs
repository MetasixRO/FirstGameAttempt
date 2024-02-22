using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    public HealthBar healthBar;
    public Canvas canvas;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
        animator = GetComponent<Animator>();
    }

    //declar o functie publica TakeDamage (pentru a putea fi apelata din script-ul Combat)
    public void TakeDamage(int damage) { 
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        //Maybe play animation
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0) {
            Die();
        }
    }

    void Die() {
        //Animation + Disable enemy
        animator.SetBool("isDead", true);

        
        GetComponent<Collider>().enabled = false;
        GetComponent<CharacterController>().enabled = false;

        canvas.enabled = false;
        this.enabled = false;
        StartCoroutine(despawnEnemy());
    }

    IEnumerator despawnEnemy() {
        yield return new WaitForSeconds(5);
        enemy.SetActive(false);
    }
}

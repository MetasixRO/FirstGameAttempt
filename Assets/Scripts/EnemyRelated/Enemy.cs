using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public delegate void ModifyEnemyHealthbar(float health);
    public static event ModifyEnemyHealthbar SetEnemyHealth;

    private Animator animator;
    public float maxHealth = 100;
    float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        if (SetEnemyHealth != null) {
            SetEnemyHealth(maxHealth);
        }
        animator = GetComponent<Animator>();
    }


    //declar o functie publica TakeDamage (pentru a putea fi apelata din script-ul Combat)
    public void TakeDamage(float damage) { 
        currentHealth -= damage;
       
        if (SetEnemyHealth != null) {
            SetEnemyHealth(currentHealth);
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

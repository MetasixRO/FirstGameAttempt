using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public delegate void ModifyHealthbar(int health);
    public static event ModifyHealthbar SetHealth;

    private Animator animator;
    public int maxHealth = 100;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        HealthRestorer.MedpackHeal += RestoreHealth;
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        if (SetHealth != null)
        {
            SetHealth(currentHealth);
        }
    }

    //declar o functie publica TakeDamage (pentru a putea fi apelata din script-ul Combat)
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (SetHealth != null)
        {
            SetHealth(currentHealth);
        }

        //Maybe play animation
        //animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Animation + Disable enemy
        animator.SetBool("isDead", true);


        GetComponent<Collider>().enabled = false;
        GetComponent<CharacterController>().enabled = false;

        this.enabled = false;
    }

    public void RestoreHealth(int amount) {
        if (currentHealth + amount > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else {
            currentHealth += amount;
        }

        if (SetHealth != null) {
            SetHealth(currentHealth);
        }
    }

    /*
    public void SwitchWeapon(int weaponNumber) {
        switch (weaponNumber) {
            case 1: 
                weapon1.SetActive(true); 
                weapon2.SetActive(false); 
                animator.SetBool("isGreatSword", false);
                GetComponent<Combat>().ChangeStatsForWeapon(1);
                break; //Knife
            case 2: 
                weapon1.SetActive(false); 
                weapon2.SetActive(true); 
                animator.SetBool("isGreatSword", true);
                GetComponent<Combat>().ChangeStatsForWeapon(2);
                break; //Great Sword
        }
    }
    */
}

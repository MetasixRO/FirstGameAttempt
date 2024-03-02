using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyObject enemy;

    private int id;
    private float damage;
    private float health;

    private void Start()
    {
        id = enemy.id;
        damage = enemy.damage;
        health = enemy.health;
    }

    public int GetId() {
        return id;
    }

    public float GetDamage() {
        return damage;
    }

    public float GetHealth() { 
        return health;
    }
}

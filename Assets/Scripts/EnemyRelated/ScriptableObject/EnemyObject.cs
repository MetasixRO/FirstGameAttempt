using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyObject : ScriptableObject
{
    public int id;
    public float damage;
    public float health;
}

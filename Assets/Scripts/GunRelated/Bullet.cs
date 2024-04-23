using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public delegate void BulletHit(float damage);
    public static event BulletHit Hit;

    [SerializeField]private float life = 3f;
    [SerializeField] private float damage = 0.0f;

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        //Debug.Log(other.gameObject);
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Hit with " + damage);
            if (Hit != null)
            {
                Hit(5.0f);
            }
        }
        Destroy(gameObject);

    }

    public void SetDamage(float damage) {
        this.damage = damage;
    }
}
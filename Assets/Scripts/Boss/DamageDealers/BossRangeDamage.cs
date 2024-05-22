using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRangeDamage : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    [SerializeField] private float bulletSpeed = 20f;
    private float damage;
    private void Awake()
    {
        BossCombat.RangeDamage += SetDamage;
        BossAnimations.RangeLaunched += ManageShot;
    }

    private void SetDamage(float damage) {
        this.damage = damage;
    }

    private void ManageShot() {
        Debug.Log("Managing");
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        bullet.GetComponent<Bullet>().SetDamage(damage);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = bulletSpawnPoint.forward * bulletSpeed;
    }
}

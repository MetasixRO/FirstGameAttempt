using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : EnemyDealDamage
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float delayBetweenBullets = 0.5f;
    [SerializeField] private float spreadAngle = 5f;

    public void Shoot() {
        StartCoroutine(DelayShootingBullets());
    }

    private IEnumerator DelayShootingBullets() {
        for (int i = 0; i < 3; i++)
        {
            //Debug.Log("Bullet " + i);
            Quaternion spreadRotation = Quaternion.Euler(0f, (i - 1) * spreadAngle, 0f) * bulletSpawnPoint.rotation;

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, spreadRotation);

            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

            yield return new WaitForSeconds(delayBetweenBullets);
        }
    }

    public void ShootDeprecated() {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
    }

    public override void SetDamage(float damage) {
        bulletPrefab.GetComponent<Bullet>().SetDamage(damage);
    }
}

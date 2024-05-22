using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAreaDamage : MonoBehaviour
{
    public delegate void BossArea(float damage);
    public static event BossArea BossDamage;

    private SphereCollider currentCollider;
    private Renderer currentRenderer;
    private float damage;

    private void Awake()
    {
        BossCombat.AreaDamage += SetDamage;
        BossAnimations.AreaLaunched += ManageCollider;
        BossAnimations.CurrentPosition += TeleportPoint;
    }

    private void Start()
    {
        currentCollider = GetComponent<SphereCollider>();
        currentRenderer = GetComponent<Renderer>();
    }

    private void SetDamage(float damage) {
        this.damage = damage;
    }

    private void TeleportPoint(GameObject boss) {
        Vector3 currentPosition = gameObject.transform.position;

        Vector3 bossPosition = boss.transform.position;

        Vector3 newPosition = new Vector3(bossPosition.x, currentPosition.y, bossPosition.z);

        gameObject.transform.position = newPosition;
    }

    private void ManageCollider() {
        currentCollider.enabled = !currentCollider.enabled;
        currentRenderer.enabled = currentCollider.enabled;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentCollider.enabled && other.CompareTag("Player")) {
            BossDamage?.Invoke(damage);
        }
    }
}

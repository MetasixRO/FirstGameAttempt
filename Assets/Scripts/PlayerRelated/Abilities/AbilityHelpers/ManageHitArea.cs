using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageHitArea : MonoBehaviour
{
    private Collider areaCollider;
    private Renderer areaRenderer;
    void Start()
    {
        AreaNPCAbility.SetAreaSignal += ActivateArea;
        AreaNPCAbility.ResetAreaSignal += DeactivateArea;

        areaCollider = GetComponent<SphereCollider>();
        areaRenderer = GetComponent<Renderer>();
        areaCollider.enabled = false;
        areaRenderer.enabled = false;
    }

    private void ActivateArea() {
        Debug.Log("Area set");

        Vector3 currentPosition = gameObject.transform.position;
        Vector3 playerPosition = PlayerTracker.instance.transform.position;
        gameObject.transform.position = new Vector3(playerPosition.x, currentPosition.y, playerPosition.z);
        areaRenderer.enabled = true;
        areaCollider.enabled = true;
    }

    private void DeactivateArea() {
        Debug.Log("Area reset");
        areaRenderer.enabled = false;
        areaCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) {
            other.GetComponent<EnemyCombat>().TakeDamage(5);
        }
    }
}

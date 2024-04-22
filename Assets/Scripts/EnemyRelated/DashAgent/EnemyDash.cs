using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDash : MonoBehaviour
{
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    private DashDealDamage dashDamage;

    public void InitiateDash()
    {
        if (!dashDamage) { 
            dashDamage = GetComponent<DashDealDamage>();
        }
        StartCoroutine(PerformDash());
    }

    private IEnumerator PerformDash()
    {
        dashDamage.ManageWeaponDamageDealing(true);
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + transform.forward * dashSpeed;

        endPosition = CheckEndPosition(endPosition);

        float startTime = Time.time;
        while (Time.time < startTime + dashTime && transform.position != endPosition)
        {
            float elapsedTime = (Time.time - startTime) / dashTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime);
            yield return null;
        }
        transform.position = endPosition;
        dashDamage.ManageWeaponDamageDealing(false);
    }

    private Vector3 CheckEndPosition(Vector3 endPosition)
    {
        Vector3 newEndPosition = endPosition;

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, dashSpeed))
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("WorldLimit"))
                {
                    float distance = Vector3.Distance(transform.position, hit.point);
                    float newDashSpeed = Mathf.Min(dashSpeed, distance);
                    newEndPosition = transform.position + transform.forward * newDashSpeed;
                }
            }
        }
        return newEndPosition;
    }
}

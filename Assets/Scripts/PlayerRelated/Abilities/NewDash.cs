using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDash : MonoBehaviour
{
    public delegate void DashDoneEvent();
    public static event DashDoneEvent DashDone;

    public float dashSpeed;
    public float dashTime;

    private bool isDashPressed = false;

    private bool canDash = true;


    // Start is called before the first frame update
    void Awake()
    {
        CharacterMovement.Dash += InitiateDash;
    }

    void Update()
    {
        if (isDashPressed && canDash)
        {
            StartCoroutine(PerformDash());
        }
    }

    private void InitiateDash() {
        StartCoroutine(PerformDash());
    }

    private IEnumerator PerformDash()
    {
        if (canDash)
        {
            canDash = false;
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
            if (DashDone != null) {
                DashDone();
            }
            yield return new WaitForSeconds(0.5f);
            canDash = true;
        }
    }

    public void ReceiveDashButtonStatus(bool isPressed) { 
        isDashPressed = isPressed;
    }

    private Vector3 CheckEndPosition(Vector3 endPosition) {
        Vector3 newEndPosition = endPosition;

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, dashSpeed)) {
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

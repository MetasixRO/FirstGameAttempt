using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDash : MonoBehaviour
{
    public delegate void DashDoneEvent();
    public static event DashDoneEvent DashDone;
    public static event DashDoneEvent DelayPassed;

    public float dashSpeed;
    public float dashTime;
    public float dashDelay;

    private int dashReducer = 0;
    private bool inCycle = false;

    private float originalDelay;
    private int counter = 0;

    // Start is called before the first frame update
    void Awake()
    {
        originalDelay = dashDelay;
        CharacterMovement.Dash += InitiateDash;
        DoubleDashAbility.ReduceTime += SetDoubleReducer;
        DoubleDashAbility.ResetTime += ResetReducer;
    }

    private void InitiateDash() {
        StartCoroutine(PerformDash());
    }

    private IEnumerator PerformDash()
    {
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

        ManageDelay();
        
        StartCoroutine(DelayDash(dashDelay));

    }

    private IEnumerator DelayDash(float delay) {
        yield return new WaitForSeconds(delay);
        if (DelayPassed != null) {
            DelayPassed();
        }
    }

    private void ManageDelay() {
        switch (dashReducer)
        {
            case 0:
                break;
            case 2:
                counter++;
                if (counter == 1)
                {
                    ReduceDelay();
                    inCycle = true;
                    StartCoroutine(TimerToResetDelay(0.75f));
                }
                else if (counter == 2)
                {
                    ResetDelay();
                    inCycle = false;
                    counter = 0;
                }
                break;
            case 3:
                counter++;
                break;
        }
    }

    private IEnumerator TimerToResetDelay(float timer) {
        yield return new WaitForSeconds(timer);
        if (inCycle) {
            inCycle = false;
            counter = 0;
            ResetDelay();
        }

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

    private void ReduceDelay() {
        dashDelay = 0.25f;
    }

    private void ResetDelay() {
        dashDelay = originalDelay;
    }

    private void ResetReducer() {
        DoubleDashAbility.ResetTime -= ResetReducer;
        DoubleDashAbility.ReduceTime -= SetDoubleReducer;
        dashDelay = originalDelay;
        dashReducer = 0;
    }

    private void SetDoubleReducer() {
        dashReducer = 2;
    }
}

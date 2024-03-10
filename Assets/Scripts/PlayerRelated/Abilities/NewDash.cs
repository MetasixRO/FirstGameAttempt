using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDash : MonoBehaviour
{
    Animator animator;

    public float dashSpeed;
    public float dashTime;

    private bool isDashPressed = false;

    private bool canDash = true;


    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();

        CharacterMovement.Dash += InitiateDash;
    }

    void Update()
    {
        if (isDashPressed && canDash)
        {
            animator.applyRootMotion = false;
            StartCoroutine(PerformDash());
        }
    }

    private void InitiateDash() {
        animator.applyRootMotion = false;
        StartCoroutine(PerformDash());
    }

    private IEnumerator PerformDash()
    {
        if (canDash)
        {
            canDash = false;
            Vector3 startPosition = transform.position;
            Vector3 endPosition = startPosition + transform.forward * dashSpeed;

            float startTime = Time.time;
            while (Time.time < startTime + dashTime)
            {
                float elapsedTime = (Time.time - startTime) / dashTime;
                transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime);
                yield return null;
            }

            animator.applyRootMotion = true;
            yield return new WaitForSeconds(0.5f);
            canDash = true;
        }
    }

    public void ReceiveDashButtonStatus(bool isPressed) { 
        isDashPressed = isPressed;
    }
}

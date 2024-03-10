using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{

    public float dashDistance = 0.1f;
    public float dashTime = 0.1f;

    Animator animator;

    private bool isDashPressed = false;

    private bool canDash = true;


    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashPressed && canDash) {
            animator.applyRootMotion = false;
            StartCoroutine(PerformDash());
        }
    }

    IEnumerator PerformDash() {
        canDash = false;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + transform.forward * dashDistance;

        float elapsedTime = 0f;
        while (elapsedTime < dashTime) {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / dashTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        //transform.position = endPosition;
        animator.applyRootMotion = true;
        yield return new WaitForSeconds(2f);
        canDash = true;
        
    }


    public void ReceiveDashButtonStatus(bool isPressed) { 
        isDashPressed = isPressed;
    }
}

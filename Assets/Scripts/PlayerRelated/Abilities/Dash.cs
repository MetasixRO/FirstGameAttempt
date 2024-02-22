using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{

    public float dashDistance;
    public float dashTime;

    PlayerInput input;

    private bool isDashPressed = false;


    // Start is called before the first frame update
    void Awake()
    {
        input = new PlayerInput();

        input.CharacterControls.Dash.performed += ctx => isDashPressed = ctx.ReadValueAsButton();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashPressed) {
            StartCoroutine(PerformDash());
        }
    }

    IEnumerator PerformDash() {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + transform.forward * dashDistance;

        float startTime = Time.time;
        while (Time.time < startTime + dashTime) {
            float perform = (Time.time - startTime) / dashTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, perform);
            yield return null;
        }
    }

    void OnEnable()
    {
        input.CharacterControls.Enable();
    }

    void OnDisable()
    {
        input.CharacterControls.Disable();
    }
}

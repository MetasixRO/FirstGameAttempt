using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScriptController : MonoBehaviour
{
    Animator animatorComponent;
    int isWalkingHash;
    int isRunningHash;
    float velocity = 0.0f;
    public float acceleration = 0.5f;
    public float deceleration = 0.9f;
    int velocityHash;


    // Start is called before the first frame update
    void Start()
    {
        //Preia componente care sunt atasate la aceeasi componenta cu script
        animatorComponent = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        velocityHash = Animator.StringToHash("Velocity");
    }

    // Update is called once per frame
    void Update()
    {
        bool givenInput = Input.GetKey("w");
        bool secondInput = Input.GetKey(KeyCode.LeftShift);

        bool isWalking = animatorComponent.GetBool(isWalkingHash);
        bool isRunning = animatorComponent.GetBool(isRunningHash);

        if (givenInput && !secondInput && velocity < 1.0f ) {
            velocity = 0.1f;
            velocity += Time.deltaTime * acceleration;
        }

        if (givenInput && !secondInput && velocity > 0.2f) { 
            velocity -= Time.deltaTime * deceleration;
        }

        if (givenInput && secondInput && velocity < 1.0f) {
            velocity += Time.deltaTime * acceleration;
        }

        if (!givenInput && velocity > 0.0f) { 
            velocity -= Time.deltaTime * deceleration;
        }

        if (!givenInput && !secondInput && velocity < 0.0f) {
            velocity = 0.0f;
        }

        animatorComponent.SetFloat(velocityHash, velocity);

        /*

        if (givenInput && secondInput && !isRunning)
        {
            animatorComponent.SetBool(isWalkingHash, false);
            animatorComponent.SetBool(isRunningHash, true);
        }
        else if (givenInput && !secondInput && !isWalking)
        {
            animatorComponent.SetBool(isRunningHash, false);
            animatorComponent.SetBool(isWalkingHash, true);
        }
        else if (!givenInput && !secondInput && (isWalking || isRunning)) {
            animatorComponent.SetBool(isRunningHash, false);
            animatorComponent.SetBool(isWalkingHash, false);
        }
        */

    }
}

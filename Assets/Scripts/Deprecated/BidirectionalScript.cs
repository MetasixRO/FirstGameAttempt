using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BidirectionalScript : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    float velocityX = 0.0f;
    float velocityZ = 0.0f;
    public float acceleration = 4.0f;
    public float deceleration = 2.0f;
    int velocityXHash;
    int velocityYHash;


    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        velocityXHash = Animator.StringToHash("Velocity X");
        velocityYHash = Animator.StringToHash("Velocity Z");
    }

    // Update is called once per frame
    void Update()
    {
        bool walkInput = Input.GetKey("w");
        bool leftInput = Input.GetKey("a");
        bool rightInput = Input.GetKey("d");
        bool runInput = Input.GetKey(KeyCode.LeftShift);

        if (walkInput && !runInput) {
            if (velocityZ < 2.0f) {
                velocityZ += Time.deltaTime * acceleration;
            }

            if (velocityZ > 0.5f) {
                velocityZ -= Time.deltaTime * deceleration;
            }
            
        }

        if (walkInput && runInput) {
            if (velocityZ < 2.0f){
                velocityZ += Time.deltaTime * acceleration;
            }
        }

        if (!walkInput && runInput && velocityZ > 0.0f) {
            velocityZ -= Time.deltaTime * deceleration;

            if (velocityZ < 0.05f) { 
                velocityZ = 0.0f;
            }
        }

        if (!walkInput && !runInput && velocityZ > 0.0f) {
            velocityZ -= Time.deltaTime * deceleration;
        }

        if (leftInput && !rightInput)
        {
            if (velocityX > -2.0f)
            {
                velocityX -= Time.deltaTime * acceleration;
            }

            if (!runInput) { 
                if (velocityX < -0.5f)
                {
                    velocityX += Time.deltaTime * deceleration;
                }
            }
        }

        if (rightInput && !leftInput)
        {
            if (velocityX < 2.0f)
            {
                velocityX += Time.deltaTime * acceleration;
            }

            if (!runInput)
            {
                if (velocityX > 0.5f)
                {
                    velocityX -= Time.deltaTime * deceleration;
                }
            }
        }

        if (!rightInput && !leftInput)
        {
            if (velocityX < 0.0f)
            {
                velocityX += Time.deltaTime * deceleration;
            }
            else if (velocityX > 0.0f)
            {
                velocityX -= Time.deltaTime * deceleration;
            }

            if (velocityX > -0.05f && velocityX < 0.05f) { 
                velocityX = 0.0f;
            }
        }

        animator.SetFloat("Velocity Z", velocityZ);
        animator.SetFloat("Velocity X", velocityX);
    }
}

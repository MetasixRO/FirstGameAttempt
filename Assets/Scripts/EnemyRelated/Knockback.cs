using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    private float force = 50f;
    public void ApplyKnockback() {
        gameObject.transform.position += PlayerTracker.instance.player.transform.forward * Time.deltaTime * force;
    }
}

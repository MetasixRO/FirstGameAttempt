using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventsAudioManager : MonoBehaviour
{
    public AudioSource src;
    public AudioClip dashSounds, walkSounds, runSounds, hitSounds, swingSounds;
    private int currentClipPlaying = 0;

    private void Start()
    {
        CharacterMovement.Dash += PlayDash;
        CharacterMovement.Walking += SetWalkingTrue;
        CharacterMovement.NotWalking += SetWalkingFalse;

        DealDamage.NoEnemyHit += PlaySwing;
        DealDamage.EnemyHit += PlayHit;
    }

    private void PlayDash() {
        if (currentClipPlaying != 1) {
            src.loop = false;
            src.clip = dashSounds;
            src.Play();
            currentClipPlaying = 1;
            StartCoroutine(DelayResetCurrentClip());
        }
    }

    private IEnumerator DelayResetCurrentClip() {
        yield return new WaitForSeconds(1.0f);
        currentClipPlaying = 0;
    }

    public void SetWalkingTrue() {
       // walking = true;
    }

    public void SetWalkingFalse()
    {
       // walking = false;
        //notAlreadyWalking = true;
    }

    private void PlayHit() {
        if (currentClipPlaying != 2) {
            src.loop = false;
            src.clip = hitSounds;
            src.Play();
            currentClipPlaying = 2;
            StartCoroutine(DelayResetCurrentClip());
        }
    }

    private void PlaySwing() {
        if (currentClipPlaying != 3)
        {
            src.loop = false;
            src.clip = swingSounds;
            src.Play();
            currentClipPlaying = 3;
            StartCoroutine(DelayResetCurrentClip());
        }
    }
}

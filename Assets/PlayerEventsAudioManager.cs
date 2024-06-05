using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventsAudioManager : MonoBehaviour
{
    public AudioSource src;
    public AudioClip dashSounds, walkSounds, runSounds, hitSounds, swingSounds;
    private int currentClipPlaying = 0;

    // 1 = Dash
    // 2 = Hit
    // 3 = Swing
    // 4 = Walk
    // 5 = Run

    private bool walking,running;

    private void Start()
    {
        walking = false;
        running = false;
        CharacterMovement.Dash += PlayDash;
        NewDash.DashDone += CheckMovement;
        CharacterMovement.Walking += SetWalkingTrue;
        CharacterMovement.NotWalking += SetWalkingFalse;
        CharacterMovement.Running += SetRunningTrue;
        CharacterMovement.NotRunning += SetRunningFalse;

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
        CheckMovement();
    }

    public void SetWalkingTrue() {
        if (!walking) {
            walking = true;
            src.loop = true;
            src.clip = walkSounds;
            src.PlayDelayed(0.2f);
            currentClipPlaying = 4;
        }
    }

    public void SetWalkingFalse()
    {
        if (walking) {
            walking = false;
            src.loop = false;
            src.Stop();
        }
    }

    private void SetRunningTrue() {
        if (!running) {
            running = true;
            src.loop = true;
            src.clip = runSounds;
            src.Play();
            currentClipPlaying = 5;
        }
    }

    private void SetRunningFalse() {
        if (running) {
            running = false;
            src.loop = false;
            src.Stop();
            CheckMovement();
        }
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

    private void CheckMovement() {
        if (running) {
            running = false;
            SetRunningTrue();
        } else if (walking) {
            walking = false;
            SetWalkingTrue();
        }
    }
}

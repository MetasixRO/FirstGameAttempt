using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    public AudioSource src;
    public AudioClip lobbySfx,arenaSfx,deadSfx;
    private int currentClipPlaying = 0;

    private void Start()
    {
        LevelManager.ReachingNewArena += PlayArena;
        newDeadState.ReachedZeroHealth += PlayDead;
        NewLobbyState.ReachedLobby += PlayLobby;

        PlayLobby();
    }

    private void Update()
    {
        if (Input.GetKeyDown("i")) {
            PlayArena();
        }
    }

    public void PlayArena()
    {
        if(currentClipPlaying != 2)
        {
            src.loop = true;
            src.clip = arenaSfx;
            src.Play();
            currentClipPlaying = 2;
        }
    }

    public void PlayDead() {
        if (currentClipPlaying != 3) {
            src.loop = false;
            currentClipPlaying = 3;
            src.clip = deadSfx;
            src.Play();
        }
    }

    public void PlayLobby() {
        if (currentClipPlaying != 1) {
            src.loop = true;
            currentClipPlaying = 1;
            src.clip = lobbySfx;
            src.Play();
        }
    }
}

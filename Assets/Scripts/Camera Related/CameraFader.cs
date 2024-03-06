using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFader : MonoBehaviour
{
    private GameObject player;
    private ObjectFader fader;
    GameObject lastObjectFound;

    void Start()
    {
        player = PlayerTracker.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) { 
            Vector3 direction = player.transform.position - transform.position;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;

            

            if (Physics.Raycast(ray, out hit)) {

                if (hit.collider == null) {
                    return;
                }

                if (lastObjectFound != null && hit.collider.gameObject != lastObjectFound) {
                    if (fader != null)
                    {
                        fader.doFade = false;
                    }
                }

                if (hit.collider.gameObject == player) {
                    if (fader != null)
                    {
                        fader.doFade = false;
                    }
                }
                else
                {
                    lastObjectFound = hit.collider.gameObject;
                    fader = hit.collider.gameObject.GetComponent<ObjectFader>();
                    if (fader != null)
                    {
                        fader.doFade = true;
                    }
                }
            }
        }
    }
}

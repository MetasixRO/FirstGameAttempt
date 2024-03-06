using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCameraFader : MonoBehaviour
{
    private GameObject player;
    private List<ObjectFader> activeFaders;

    void Start()
    {
        player = PlayerTracker.instance.player;
        activeFaders = new List<ObjectFader>();
    }


    void Update()
    {
        if (player != null)
        {
            List<ObjectFader> updatedActiveFaders = new List<ObjectFader>();

            Vector3 direction = player.transform.position - transform.position;

            RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, 20f);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider == null)
                {
                    return;
                }

                ObjectFader fader = hit.collider.gameObject.GetComponent<ObjectFader>();

                if (fader != null) {
                    fader.doFade = (hit.collider.gameObject != player);
                    updatedActiveFaders.Add(fader);
                }
            }

            foreach(ObjectFader fader in activeFaders) {
                if (!updatedActiveFaders.Contains(fader)) {
                    fader.doFade = false;
                }        
            }

            activeFaders = updatedActiveFaders;
        }
    }
}

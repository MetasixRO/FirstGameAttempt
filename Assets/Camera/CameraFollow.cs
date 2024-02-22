using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    public Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate() //apelata dupa toate calculele
    {
        Vector3 newPosition  = target.transform.position + cameraOffset;
        transform.position = newPosition;
    }
}

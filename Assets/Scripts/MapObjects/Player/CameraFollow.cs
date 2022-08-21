using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject followObject;
    private Vector3 cameraOffset;

    private void Start()
    {
        cameraOffset = transform.position - followObject.transform.position;
    }

    void LateUpdate()
    {
        transform.position = followObject.transform.position + cameraOffset;
    }
}

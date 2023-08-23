using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject palla;
    private Vector3 camerapos;

    // Start is called before the first frame update
    void Start()
    {
        camerapos = transform.localPosition - palla.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = palla.transform.localPosition + camerapos;
    }
}

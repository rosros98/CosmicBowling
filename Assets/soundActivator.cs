using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundActivator : MonoBehaviour
{
    [SerializeField]
    AudioSource asPin;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("agent"))
        {
            asPin.Play();
        }
    }
}

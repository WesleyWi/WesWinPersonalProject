using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float destroyDelay = 5.0f; // Set the delay in seconds

    void Start()
    {
        // Call the DestroyObject function after the specified delay
        Invoke("DestroyObject", destroyDelay);
    }

    void DestroyObject()
    {
        // Destroy the GameObject this script is attached to
        Destroy(gameObject);
    }
}


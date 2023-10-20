using UnityEngine;
using TMPro;

public class EnableTextOnEnter : MonoBehaviour
{
    public TextMeshPro textMeshPro; // Reference to your 3D TextMeshPro object.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player has entered the collider, so enable the text.
            textMeshPro.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player has exited the collider, so disable the text.
            textMeshPro.enabled = false;
        }
    }
}

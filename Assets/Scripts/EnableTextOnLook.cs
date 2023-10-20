using UnityEngine;
using TMPro;

public class EnableTextOnLook : MonoBehaviour
{
    public Transform player; // Reference to the player's camera or transform.
    public TextMeshPro textMeshPro; // Reference to your 3D TextMeshPro object.
    public float activationDistance = 5f; // Adjust this distance based on your needs.

    private bool playerInRange = false;

    void Update()
    {
        // Calculate the distance between the box and the player.
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= activationDistance && IsPlayerLookingAtBox())
        {
            // Player is close and looking at the box.
            textMeshPro.enabled = true;
        }
        else
        {
            // Player is not close or not looking at the box.
            textMeshPro.enabled = false;
        }
    }

    bool IsPlayerLookingAtBox()
    {
        // Use Raycasting to determine if the player is looking at the box.
        Ray ray = new Ray(player.position, player.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, activationDistance))
        {
            if (hit.collider.gameObject == gameObject)
            {
                return true;
            }
        }

        return false;
    }
}

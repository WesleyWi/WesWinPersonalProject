using UnityEngine;

public class Teleporter : MonoBehaviour
{
    // The tag you want to teleport (e.g., "Player" or "Box").
    public string targetTag = "Player";
    // The empty object where the target will be teleported.
    public Transform teleportDestination;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object has the specified tag.
        if (other.CompareTag(targetTag))
        {
            if (targetTag == "Player")
            {
                // If the target is the player, teleport them using the Character Controller.
                CharacterController characterController = other.GetComponent<CharacterController>();
                if (characterController != null)
                {
                    characterController.enabled = false; // Disable the Character Controller temporarily.
                    other.transform.position = teleportDestination.position;
                    characterController.enabled = true; // Re-enable the Character Controller.
                }
            }
            else
            {
                // If the target is not the player, teleport it normally.
                other.transform.position = teleportDestination.position;
            }
        }
    }
}

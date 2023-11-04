using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private List<CharacterController> playersOnPlatform = new List<CharacterController>();
    private Vector3 platformStartPosition;
    private Vector3 previousPlatformPosition;
    private float platformSpeed = 2.0f; // Adjust the speed as needed
    private float platformRange = 5.0f; // Adjust the range as needed

    private void Start()
    {
        platformStartPosition = transform.position;
        previousPlatformPosition = platformStartPosition;
    }

    private void Update()
    {
        // Calculate the platform's horizontal (back-and-forth) movement
        float platformMovement = Mathf.PingPong(Time.time * platformSpeed, platformRange * 2) - platformRange;

        // Calculate the final position of the platform
        Vector3 finalPlatformPosition = platformStartPosition + new Vector3(platformMovement, 0, 0);

        // Move the platform to the final position
        transform.position = finalPlatformPosition;

        // Move each player on the platform
        foreach (var playerController in playersOnPlatform)
        {
            // Calculate the player's movement based on platform movement
            Vector3 playerMovement = CalculatePlayerMovement(playerController, platformMovement);
            playerController.Move(playerMovement);
        }

        // Update the previous platform position for the next frame
        previousPlatformPosition = finalPlatformPosition;
    }

    private Vector3 CalculatePlayerMovement(CharacterController playerController, float platformMovement)
    {
        // Calculate the difference between the platform's current and previous positions
        Vector3 platformDelta = transform.position - previousPlatformPosition;

        // Adjust the player's position to match the platform's movement
        Vector3 playerMovement = playerController.transform.position + platformDelta - playerController.transform.position;

        return playerMovement;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if a player enters the trigger zone
        if (other.CompareTag("Player"))
        {
            CharacterController playerController = other.GetComponent<CharacterController>();
            if (playerController != null)
            {
                playersOnPlatform.Add(playerController);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if a player exits the trigger zone
        if (other.CompareTag("Player"))
        {
            CharacterController playerController = other.GetComponent<CharacterController>();
            if (playerController != null)
            {
                playersOnPlatform.Remove(playerController);
            }
        }
    }
}

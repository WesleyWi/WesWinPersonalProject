using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] targetPoints; // An array of target points for the platform to follow
    public float moveSpeed = 2.0f; // Adjust the speed as needed

    private List<CharacterController> playersOnPlatform = new List<CharacterController>();
    private int currentTargetIndex = 0;
    private Vector3 previousPlatformPosition;

    private void Start()
    {
        if (targetPoints.Length == 0)
        {
            Debug.LogWarning("No target points assigned to the moving platform.");
            enabled = false; // Disable the script to prevent errors.
        }

        previousPlatformPosition = transform.position;
    }

    private void Update()
    {
        if (targetPoints.Length > 0)
        {
            // Move the platform toward the current target point
            Transform currentTarget = targetPoints[currentTargetIndex];
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, step);

            // If the platform reaches the current target point, move to the next target
            if (Vector3.Distance(transform.position, currentTarget.position) < 0.01f)
            {
                currentTargetIndex = (currentTargetIndex + 1) % targetPoints.Length;
            }

            Vector3 platformMovementDelta = transform.position - previousPlatformPosition;

            // Move each player on the platform
            foreach (var playerController in playersOnPlatform)
            {
                // Calculate the player's movement based on the platform movement delta
                Vector3 playerMovement = CalculatePlayerMovement(playerController, platformMovementDelta);
                playerController.Move(playerMovement);
            }

            // Update the previous platform position for the next frame
            previousPlatformPosition = transform.position;
        }
    }

    private Vector3 CalculatePlayerMovement(CharacterController playerController, Vector3 platformMovementDelta)
    {
        // Adjust the player's position to match the platform's movement
        Vector3 playerMovement = playerController.transform.position + platformMovementDelta - playerController.transform.position;

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

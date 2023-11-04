using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] targetPoints; // An array of target points for the platform to follow
    public float moveSpeed = 2.0f; // Adjust the speed as needed

    private List<CharacterController> playersOnPlatform = new List<CharacterController>();
    private List<Rigidbody> boxesOnPlatform = new List<Rigidbody>(); // List for objects with the "Box" tag
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

            // Move each object with the "Box" tag on the platform
            foreach (var boxRigidbody in boxesOnPlatform)
            {
                // Calculate the box's movement based on the platform movement delta
                Vector3 boxMovement = CalculateBoxMovement(boxRigidbody, platformMovementDelta);
                boxRigidbody.MovePosition(boxRigidbody.position + boxMovement);
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

    private Vector3 CalculateBoxMovement(Rigidbody boxRigidbody, Vector3 platformMovementDelta)
    {
        // Adjust the box's position to match the platform's movement
        Vector3 boxMovement = platformMovementDelta;

        return boxMovement;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController playerController = other.GetComponent<CharacterController>();
            if (playerController != null)
            {
                playersOnPlatform.Add(playerController);
            }
        }
        else if (other.CompareTag("box")) // Check if an object with the "Box" tag enters the trigger zone
        {
            Rigidbody boxRigidbody = other.GetComponent<Rigidbody>();
            if (boxRigidbody != null)
            {
                boxesOnPlatform.Add(boxRigidbody);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController playerController = other.GetComponent<CharacterController>();
            if (playerController != null)
            {
                playersOnPlatform.Remove(playerController);
            }
        }
        else if (other.CompareTag("box")) // Check if an object with the "Box" tag exits the trigger zone
        {
            Rigidbody boxRigidbody = other.GetComponent<Rigidbody>();
            if (boxRigidbody != null)
            {
                boxesOnPlatform.Remove(boxRigidbody);
            }
        }
    }
}

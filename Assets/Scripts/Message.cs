using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour
{
    public Transform cubeToFollow; // Reference to the Cube's transform component
    public float offset = 1.3f; // Vertical offset above the cube.
    public float proximityDistance = 2.0f; // Distance to enable the 3D text.
    private Transform player; // Reference to the player's transform component
    private bool isTextEnabled = false;

    private bool isCubePickedUp = false;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (cubeToFollow != null && cubeToFollow.CompareTag("box"))
        {
            // Handle picking up the cube
            if (Input.GetMouseButtonDown(1) && !isCubePickedUp)
            {
                // Right-click to pick up the cube
                isCubePickedUp = true;
                // Disable the 3D text
                isTextEnabled = false;
                GetComponent<MeshRenderer>().enabled = false;
            }
            // Handle dropping the cube
            else if (Input.GetMouseButtonDown(1) && isCubePickedUp)
            {
                // Right-click to drop the cube
                isCubePickedUp = false;
                // Re-enable the 3D text
            }

            // Set the text's position to match the cube's position with an offset on the Y-axis.
            Vector3 newPosition = cubeToFollow.position;
            newPosition.y += offset;
            transform.position = newPosition;

            // Check proximity to the player.
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= proximityDistance && !isCubePickedUp)
            {
                // Enable the 3D text
                isTextEnabled = true;
                GetComponent<MeshRenderer>().enabled = true;

                // Rotate the text to face the player
                transform.LookAt(player);
            }
            else if (isTextEnabled)
            {
                // Disable the 3D text
                isTextEnabled = false;
                GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

}

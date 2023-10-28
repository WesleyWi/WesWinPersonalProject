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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (cubeToFollow != null)
        {
            // Set the text's position to match the cube's position with an offset on the Y-axis.
            Vector3 newPosition = cubeToFollow.position;
            newPosition.y += offset;
            transform.position = newPosition;

            // Check proximity to the player.
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= proximityDistance)
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

    // Handle object picked up event
    public void OnObjectPickedUp()
    {
        // Disable the MeshRenderer when an object is picked up
        GetComponent<MeshRenderer>().enabled = false;
    }

    // Handle object dropped event
    public void OnObjectDropped()
    {
        // Enable the MeshRenderer when no object is held
        GetComponent<MeshRenderer>().enabled = true;
    }
}

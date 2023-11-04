using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public string targetTag = "Projectile"; // Set your desired tag here
    public Material greenMaterial; // Assign your green material in the Unity Inspector
    public GameObject objectToMove; // Assign the object you want to move in the Unity Inspector
    public float moveDistance = 2.0f; // Set the distance you want the object to move
    public float moveSpeed = 1.0f; // Set the speed at which the object moves

    private bool hasEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag) && !hasEntered)
        {
            // Change the material to the green material
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.material = greenMaterial;
            }

            // Start the procedural animation to move the assigned object up
            StartCoroutine(MoveObjectUp());
            hasEntered = true;
        }
    }

    private IEnumerator MoveObjectUp()
    {
        float startTime = Time.time;
        Vector3 startPosition = objectToMove.transform.position;
        Vector3 endPosition = startPosition + Vector3.up * moveDistance;

        while (Time.time - startTime < (moveDistance / moveSpeed))
        {
            float progress = (Time.time - startTime) / (moveDistance / moveSpeed);
            objectToMove.transform.position = Vector3.Lerp(startPosition, endPosition, progress);
            yield return null;
        }

        // Ensure the object is at the final position
        objectToMove.transform.position = endPosition;
    }
}

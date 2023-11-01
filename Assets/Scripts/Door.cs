using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string playerTag = "Player"; // Tag of the player or object that triggers the door.
    public Vector3 openDirection = Vector3.up; // Direction to open the door.
    public float slideDistance = 3f; // Distance to slide the door.
    public float slideSpeed = 3f; // Speed of the sliding animation.

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isOpen = false;
    private Transform doorTransform;

    private void Start()
    {
        doorTransform = transform.GetChild(0); // Assuming the door is the first child.
        initialPosition = doorTransform.localPosition;
        targetPosition = initialPosition + openDirection.normalized * slideDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && !isOpen)
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag) && isOpen)
        {
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        StopAllCoroutines();
        StartCoroutine(SlideDoor(targetPosition));
        isOpen = true;
    }

    private void CloseDoor()
    {
        StopAllCoroutines();
        StartCoroutine(SlideDoor(initialPosition));
        isOpen = false;
    }

    private IEnumerator SlideDoor(Vector3 target)
    {
        while (Vector3.Distance(doorTransform.localPosition, target) > 0.01f)
        {
            doorTransform.localPosition = Vector3.MoveTowards(doorTransform.localPosition, target, slideSpeed * Time.deltaTime);
            yield return null;
        }
    }
}

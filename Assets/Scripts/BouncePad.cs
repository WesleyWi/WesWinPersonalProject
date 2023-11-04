using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounceForce = 10.0f;
    public float bounceDuration = 0.5f; // Time it takes to reach full bounce force.

    private bool isBouncing = false;
    private float bounceTimer = 0.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController characterController = other.GetComponent<CharacterController>();
            if (characterController != null && !isBouncing)
            {
                StartCoroutine(BouncePlayer(characterController));
            }
        }
    }

    private IEnumerator BouncePlayer(CharacterController characterController)
    {
        isBouncing = true;

        while (bounceTimer < bounceDuration)
        {
            float bounceProgress = bounceTimer / bounceDuration;
            float currentBounceForce = Mathf.Lerp(0, bounceForce, bounceProgress);

            // Apply the bounce effect to the player by adjusting the vertical velocity.
            characterController.Move(Vector3.up * currentBounceForce * Time.fixedDeltaTime);

            bounceTimer += Time.fixedDeltaTime;
            yield return null;
        }

        // Reset the timer and the bouncing state when the bounce is complete.
        bounceTimer = 0.0f;
        isBouncing = false;
    }
}

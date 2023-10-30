using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingScript : MonoBehaviour
{
    [Header("Shooting Settings")]
    public Transform gunTransform; // Assign your gun's transform in the Inspector.
    public GameObject projectilePrefab; // Assign your projectile prefab in the Inspector.
    public float shootForce = 10f;
    public float maxShootDistance = 5f; // Maximum shooting distance in feet.

    [Header("Pickup Settings")]
    public float pickupDistance = 2f; // Maximum pickup distance in feet.
    private GameObject heldObject; // The object currently held by the player.

    private MyInputAction inputActions;

    private void Awake()
    {
        inputActions = new MyInputAction();
        inputActions.Player.Pickup.performed += _ => TogglePickupDrop();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        // Handle held object's positioning and following the camera.
        if (heldObject != null)
        {
            PositionHeldObject();
            FollowCamera();
        }
    }
    private void TogglePickupDrop()
    {
        if (heldObject == null)
        {
            TryPickup();
        }
        else
        {
            DropHeldObject();
        }
    }

    private void TryPickup()
    {
        // Check if a "box" is in front of the player and within pickupDistance.
        Ray ray = new Ray(gunTransform.position, gunTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupDistance))
        {
            if (hit.collider.CompareTag("box"))
            {
                // Set the heldObject to the picked-up box.
                heldObject = hit.collider.gameObject;
                // Optionally, you can disable the box's physics or renderers to make it look like it's picked up.
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                heldObject.GetComponent<Collider>().enabled = false;
            }
        }
    }

    private void DropHeldObject()
    {
        if (heldObject != null)
        {
            // Release the held object. You can re-enable its physics and colliders here.
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject.GetComponent<Collider>().enabled = true;
            heldObject = null;
        }
    }

    private void Shoot()
    {
        if (gunTransform != null && projectilePrefab != null)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, gunTransform.position, gunTransform.rotation);
            Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
            rb.AddForce(gunTransform.forward * shootForce, ForceMode.VelocityChange);

            // Display a debug message when you shoot
            Debug.Log("Shot fired!");
        }
    }

    private void PositionHeldObject()
    {
        if (gunTransform != null && heldObject != null)
        {
            // Offset the held object slightly in front of the gun.
            Vector3 holdPosition = gunTransform.position + gunTransform.forward * 1.5f; // Adjust the distance as needed.
            heldObject.transform.position = holdPosition;
        }
    }

    private void FollowCamera()
    {
        if (gunTransform != null && heldObject != null)
        {
            // Make the held object always face the gun.
            heldObject.transform.LookAt(gunTransform);
        }
    }
}

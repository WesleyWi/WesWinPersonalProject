using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingScript : MonoBehaviour
{
    [Header("Shooting Settings")]
    public Transform gunTransform; // Assign your gun's transform in the Inspector.
    public GameObject projectilePrefab; // Assign your projectile prefab in the Inspector.
    public float shootForce = 10f;
    public float maxShootDistance = 5f; // Maximum shooting distance in feet.
    public float minDistanceToBlock = 1f; // Minimum distance to "block" objects in feet.

    private MyInputAction inputActions;

    private void Awake()
    {
        inputActions = new MyInputAction();
        inputActions.Player.Shoot.performed += _ => TryShoot();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void TryShoot()
    {
        // Check if there's an obstacle within maxShootDistance
        if (CanShoot())
        {
            Shoot();
        }
        else
        {
            // You can provide feedback to the player here, like a message or sound indicating that shooting is blocked.
            Debug.Log("Obstacle in the way or target is too close to shoot.");
        }
    }

    private bool CanShoot()
    {
        if (gunTransform != null && projectilePrefab != null)
        {
            // Create a ray from the gun position in the direction the gun is pointing
            Ray ray = new Ray(gunTransform.position, gunTransform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, maxShootDistance))
            {
                if (hit.collider.CompareTag("block"))
                {
                    // Calculate the distance to the "block" object
                    float distanceToBlock = Vector3.Distance(gunTransform.position, hit.point);

                    // If the distance is less than the minimum allowed distance, return false to indicate that shooting is blocked.
                    if (distanceToBlock < minDistanceToBlock)
                    {
                        return false;
                    }
                }
            }
        }

        return true; // Shooting is allowed if no obstacles are blocking the line of sight.
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
}

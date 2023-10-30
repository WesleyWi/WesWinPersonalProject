using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingScript : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public Camera playerCamera; // Reference to the player's camera
    public float shootForce = 10f;

    private GameObject heldObject = null;
    private bool isHoldingObject = false;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            if (!isHoldingObject)
            {
                TryPickUp();
            }
            else
            {
                DropHeldObject();
            }
        }
    }

    private void Shoot()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(firePoint.forward * shootForce, ForceMode.VelocityChange);
        }
    }

    private void TryPickUp()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out hit, 10f))
        {
            if (hit.collider.CompareTag("box"))
            {
                Debug.Log("Picked up " + hit.collider.name);
                PickUpObject(hit.collider.gameObject);
            }
        }
    }

    private void PickUpObject(GameObject objToPickUp)
    {
        isHoldingObject = true;
        heldObject = objToPickUp;

        Debug.Log("Holding " + heldObject.name);

        // You can customize the behavior here, like attaching the object to the camera's forward direction.
        heldObject.transform.parent = playerCamera.transform;
        heldObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void DropHeldObject()
    {
        Debug.Log("Dropped " + heldObject.name);

        // You can customize the behavior here, like releasing the object.
        heldObject.transform.parent = null;
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.velocity = Vector3.zero; // Set velocity to zero so it doesn't launch
        }

        heldObject = null;
        isHoldingObject = false;
    }
}

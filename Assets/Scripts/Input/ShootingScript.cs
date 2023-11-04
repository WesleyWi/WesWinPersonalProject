using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingScript : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public Camera playerCamera;
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

    // Makes character shoot at a spawn point.
    private void Shoot()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(firePoint.forward * shootForce, ForceMode.VelocityChange);
        }
    }

    // Makes character pick the object up depending on the tag.
    private void TryPickUp()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out hit, 5f))
        {
            if (hit.collider.CompareTag("box"))
            {
                Debug.Log("Picked up " + hit.collider.name);
                PickUpObject(hit.collider.gameObject);
            }
        }
    }

    // Makes character hold the object
    private void PickUpObject(GameObject objToPickUp)
    {
        isHoldingObject = true;
        heldObject = objToPickUp;

        Debug.Log("Holding " + heldObject.name);

        heldObject.transform.parent = playerCamera.transform;
        heldObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    //Makes the character drop the object
    private void DropHeldObject()
    {
        Debug.Log("Dropped " + heldObject.name);

        heldObject.transform.parent = null;
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.velocity = Vector3.zero; // Set velocity to launch
        }

        heldObject = null;
        isHoldingObject = false;
    }
}

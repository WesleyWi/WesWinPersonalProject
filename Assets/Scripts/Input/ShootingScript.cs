using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [Header("Shooting Settings")]
    public Transform gunTransform; // Assign your gun's transform in the Inspector.
    public GameObject projectilePrefab; // Assign your projectile prefab in the Inspector.
    public float shootForce = 10f;

    private MyInputAction inputActions;

    private void Awake()
    {
        inputActions = new MyInputAction();
        inputActions.Player.Shoot.performed += _ => Shoot();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
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

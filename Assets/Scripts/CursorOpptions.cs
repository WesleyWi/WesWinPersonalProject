using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorOpptions : MonoBehaviour
{
    private bool isCursorLocked = true;

    void Start()
    {
        LockCursor();
    }

    void Update()
    {
        // You can use a key or button press to toggle the cursor lock and visibility.
        if (Input.GetKeyDown(KeyCode.Escape)) // Change to your preferred key or button
        {
            isCursorLocked = !isCursorLocked;
            LockCursor();
        }
    }

    void LockCursor()
    {
        if (isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}

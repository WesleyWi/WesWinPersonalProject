using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorOpptions : MonoBehaviour
{
    // Specify the scene index where you want to lock the cursor
    public int sceneToLockCursor = 1;

    private void Start()
    {
        // Check the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Lock or show the cursor based on the scene index
        if (currentSceneIndex == sceneToLockCursor)
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

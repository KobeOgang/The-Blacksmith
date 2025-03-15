using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pauser : MonoBehaviour
{
    public GameObject pausePanel; // Reference to the pause panel

    private bool isPaused = false;

    private void Update()
    {
        // Toggle pause when pressing ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Freeze the game
        pausePanel.SetActive(true); // Show the pause panel

        // Unlock cursor when paused
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Resume the game
        pausePanel.SetActive(false); // Hide the pause panel

        // Lock cursor back to the game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Reset time scale before loading the scene
        Cursor.lockState = CursorLockMode.None; // Unlock cursor for menu navigation
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }
}

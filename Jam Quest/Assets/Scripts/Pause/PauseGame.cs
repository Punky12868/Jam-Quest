using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] KeyCode pauseKey = KeyCode.Escape;
    [SerializeField] GameObject pauseMenu;

    public static bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if (isPaused)
            {
                ResumeButton();
            }
            else
            {
                PauseButton();
            }
        }

        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public static void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public static void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ResumeButton()
    {
        Resume();
        pauseMenu.SetActive(false);
    }

    public void PauseButton()
    {
        Pause();
        pauseMenu.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] int gameSceneIndex = 1;
    private void Awake()
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }

        if (PauseGame.isPaused && SceneManager.GetActiveScene().buildIndex == gameSceneIndex)
        {
            FindObjectOfType<PauseGame>().ResumeButton();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Loads the next scene in the build order
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); // Loads the previous scene in the build order
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Restart()
    {
        FindObjectOfType<PauseGame>().ResumeButton();
        FindObjectOfType<Damage>().OnDamage();
    }
}

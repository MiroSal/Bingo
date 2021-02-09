using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Static functions to call for Buttons
/// </summary>
public class Bingo : MonoBehaviour
{
    /// <summary>
    /// Pause Game
    /// </summary>
    public static void PauseGame()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// UnPauseGame
    /// </summary>
    public static void SetToDefaultSpeed()
    {
        Time.timeScale = 1;
    }

    /// <summary>
    /// setGameSpeed
    /// </summary>
    public static void SetGameSpeed(int speed = 1)
    {
        if (speed >= 0)
        {
            Time.timeScale = speed;
        }
    }

    public static void LoadMainMenu()
    {
        SceneLoader.LoadScene(SceneLoader.Scene.MainMenu);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
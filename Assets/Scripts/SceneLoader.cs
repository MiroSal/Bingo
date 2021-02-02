using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script for sceneloading
/// If loaded as additive, scene is loaded on top of other open scenes.
/// </summary>
public static class SceneLoader
{
    public enum Scene
    {
        BingoStudio,
        BingoStudio_UI,
        Loading,
        MainMenu,
        _PreLoad
    }

    /// <summary>
    /// Load scene as Single
    /// </summary>
    /// <param name="scene"></param>
    public static void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }  
    
    /// <summary>
    /// Load scene as Additive to other scenes
    /// </summary>
    /// <param name="scene"></param>
    public static void LoadAdditiveScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString(),LoadSceneMode.Additive);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for sceneloading
/// If loaded as additive, scene is loaded on top of other open scenes.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private string sceneName = "";

    [SerializeField]
    private bool bLoadAsAdditiveScene = false;

    void Start()
    {
        if (bLoadAsAdditiveScene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}

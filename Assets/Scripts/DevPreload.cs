using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to make sure that everything is loaded to scene when in development and scene is played directly from the editor.
/// </summary>
public class DevPreload : MonoBehaviour
{
    private void Awake()
    {
        GameObject check = GameObject.Find("Directors");
        if (check == null)
        {
            Debug.Log("Loading _preload scene"+ check);
            UnityEngine.SceneManagement.SceneManager.LoadScene("_Preload");         
        }
    }
}

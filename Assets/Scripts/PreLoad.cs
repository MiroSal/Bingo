using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreLoad : MonoBehaviour
{
    [SerializeField]
    private SceneLoader.Scene sceneToLoad;

    void Start()
    {
        SceneLoader.LoadScene(sceneToLoad);
    }
}

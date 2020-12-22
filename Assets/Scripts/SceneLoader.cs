using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public string MainScenename = "BingoMain";

    void Start()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(MainScenename);
    }
}

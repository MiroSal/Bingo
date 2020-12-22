using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public string MainScenename = "BingoMain";
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(MainScenename);
    }
}

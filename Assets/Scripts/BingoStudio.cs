using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BingoStudio : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        SceneLoader.LoadAdditiveScene(SceneLoader.Scene.BingoStudio_UI);
    }
}

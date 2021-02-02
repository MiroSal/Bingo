using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartBasicGamemode()
    {
        SceneLoader.LoadScene(SceneLoader.Scene.BingoStudio);
    }
}
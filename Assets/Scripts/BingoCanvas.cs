using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Toggle CanvasComponent Visibility when Bingo is found.
/// </summary>
public class BingoCanvas : MonoBehaviour
{
    Canvas canvas = null;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        if(canvas)
        canvas.enabled = false;

        BingoDirector.BingoFoundDelegate += BingoFound;
    }

    //Bingo is found
    void BingoFound()
    {
        if (canvas)
            canvas.enabled = true;

        Time.timeScale = 0;
    }

    //Continue Bingo
    public void Continue()
    {
        if (canvas)
            canvas.enabled = false;
        Time.timeScale = 1;
    }

    private void OnDisable()
    {
        BingoDirector.BingoFoundDelegate -= BingoFound;
    }
}

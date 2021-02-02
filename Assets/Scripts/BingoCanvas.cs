using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Functionality for BingoCanvas
/// </summary>
public class BingoCanvas : MonoBehaviour
{
    //BingoCanvas
    Canvas canvas = null;

    //Button to get back to MainMenu
    GameObject MainMenuButton = null; 
    
    //Button to get back to MainMenu
    GameObject ContinueButton = null;

    //BingoDirector
    private BingoDirector bingoDirector = null;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        if(canvas)
        canvas.enabled = false;

        MainMenuButton = gameObject.transform.Find("MainMenuButton").gameObject;
        if (MainMenuButton)
            MainMenuButton.SetActive(false);
        
        ContinueButton = gameObject.transform.Find("ContinueButton").gameObject;
        if (ContinueButton)
            ContinueButton.SetActive(true);

        bingoDirector = FindObjectOfType<BingoDirector>();

        BingoDirector.BingoFoundDelegate += BingoFound;//Bind to delegate
    }

    /// <summary>
    /// called from delegate when Bingo is found
    /// </summary>
    void BingoFound(bool wasLastRound)
    {
        if (canvas)
            canvas.enabled = true;

        if (wasLastRound)
        {
            if (MainMenuButton)
                MainMenuButton.SetActive(true);

            if (ContinueButton)
                ContinueButton.SetActive(false);
        }

        bingoDirector.PauseGame();
    }

    /// <summary>
    /// Continue Bingo, Binded to continue Button
    /// </summary>
    public void Continue()
    {
        if (canvas)
            canvas.enabled = false;

        bingoDirector.StartNewRound();
        bingoDirector.UnPauseGame();
    }

    /// <summary>
    /// LoadMainMenu, Binded to continue Button
    /// </summary>
    public void LoadMainMenu()
    {
        bingoDirector.UnPauseGame();
        bingoDirector.EndGameMode();
        SceneLoader.LoadScene(SceneLoader.Scene.MainMenu);//Unbind Delegate
    }

    private void OnDisable()
    {
        BingoDirector.BingoFoundDelegate -= BingoFound;
    }
}
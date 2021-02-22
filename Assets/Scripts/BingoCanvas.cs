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
    private Canvas canvas = null;

    //Button to get back to MainMenu
    private GameObject MainMenuButton = null;

    //Button to get back to MainMenu
    private GameObject ContinueButton = null;

    //GridLayoutGroup to visualize competitors that found Bingo this round
    private GameObject Competitors = null;

    //Roundwinner prefab
    public GameObject RoundWinner = null;

    //BingoDirector
    private BingoDirector bingoDirector = null;

    //Winnings
    private Text MoneyText = null;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        if (canvas)
            canvas.enabled = false;

        MainMenuButton = gameObject.transform.Find("MainMenuButton").gameObject;
        if (MainMenuButton)
            MainMenuButton.SetActive(false);

        ContinueButton = gameObject.transform.Find("ContinueButton").gameObject;
        if (ContinueButton)
            ContinueButton.SetActive(true);

        Competitors = gameObject.transform.Find("RoundWinners").gameObject;

        MoneyText = gameObject.transform.Find("Money").GetComponentInChildren<Text>();

        bingoDirector = FindObjectOfType<BingoDirector>();

        BingoDirector.BingoFoundDelegate += BingoFound;//Bind to delegate
        BingoDirector.AnnounceRoundWinnerDelegate += AddRoundWinner;//Bind to delegate
    }

    /// <summary>
    /// called from delegate when Bingo is found
    /// </summary>
    void BingoFound(bool wasLastRound)
    {
        if (canvas)
            canvas.enabled = true;

        if (MoneyText && bingoDirector)
            MoneyText.text = bingoDirector.GetCurrentRoundsWinnings().ToString();

        if (wasLastRound)
        {
            if (MainMenuButton)
                MainMenuButton.SetActive(true);

            if (ContinueButton)
                ContinueButton.SetActive(false);
        }

        Bingo.PauseGame();
    }

    /// <summary>
    /// Add Round winner to canvas to show all winners
    /// </summary>
    /// <param name="Name">Name of the winner</param>
    /// <param name="AvatarIcon">Avatar of the winner</param>
    public void AddRoundWinner(string Name, Sprite AvatarIcon)
    {
        //check if winner is already added
        RoundWinner[] winners = Competitors.GetComponentsInChildren<RoundWinner>();
        foreach (RoundWinner winner in winners)
        {
            if (winner && winner.text.text.ToString() == Name)
            {
                return;
            }
        }

        GameObject roundWinner = Instantiate(RoundWinner);
        if (roundWinner)
        {
            roundWinner.GetComponent<RoundWinner>().Setup(Name, AvatarIcon);
            roundWinner.transform.SetParent(Competitors.transform, false);//set as child.
        }

        if (PlayerPrefs.HasKey("Money") && PlayerPrefs.HasKey("AvatarName"))
        {
            if (Name == PlayerPrefs.GetString("AvatarName"))
            {
                float money = PlayerPrefs.GetFloat("Money");
                if (bingoDirector)
                {
                    money += bingoDirector.GetCurrentRoundsWinnings();
                }               
                PlayerPrefs.SetFloat("Money", money);
                PlayerPrefs.Save();
            }
        }
    }

    /// <summary>
    /// Continue Bingo, Binded to continue Button
    /// </summary>
    public void Continue()
    {
        if (canvas)
            canvas.enabled = false;

        //remove all previous round winners
        foreach (Transform child in Competitors.transform)
            GameObject.Destroy(child.gameObject);

        bingoDirector.StartNewRound();
        Bingo.SetToDefaultSpeed();
    }

    /// <summary>
    /// LoadMainMenu, Binded to continue Button
    /// </summary>
    public void LoadMainMenu()
    {
        Bingo.SetToDefaultSpeed();
        bingoDirector.EndGameMode();
        SceneLoader.LoadScene(SceneLoader.Scene.MainMenu);//Unbind Delegate
    }

    private void OnDisable()
    {
        BingoDirector.BingoFoundDelegate -= BingoFound;
    }
}
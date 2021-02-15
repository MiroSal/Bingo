using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Inheritet from BingoCard, this class is used in player Bingo ticket.
/// </summary>
[System.Serializable]
public class PlayerTicketBingoCard : BingoCard
{
    //Ball Image shown in UI
    [Header("Prefab for the ball in BingoCard")]
    [SerializeField]
    private GameObject ballPrefab = null;

    //Text copmponent to show number how many ball left to bingo
    protected Text numbersLeftToBingo = null;

    private void Awake()
    {
        bingoDirector = FindObjectOfType<BingoDirector>();
        numbersLeftToBingo = gameObject.transform.Find("LeftToBingoText").GetComponent<Text>();

        List<int> addedNumbers = new List<int>();//avoid dublicates
        Image CardImage = gameObject.transform.Find("CardBG").GetComponent<Image>();
        FBingoBallPrefixEnum bingoBallPrefixEnum = FBingoBallPrefixEnum.B;//Ball numbers prefix letter

        FindWantedLines();//get currently wanted lines for win

        for (int i = 0; i < 25; i++)//Create UI balls to card

        {
            GameObject ball = Instantiate(ballPrefab);//Instantiate

            BingoCardBall bingoCardBall = ball.GetComponent<BingoCardBall>();

            BingoBallData ballData = new BingoBallData(bingoBallPrefixEnum, addedNumbers); //Create ballData with balls prefix letter and list of numbers that are already in the card...
            bingoCardBall.Init(ballData); //...Initialize cardball with balldata.

            if (CardImage)
            {
                ball.transform.SetParent(CardImage.transform, false);//set as child.
            }

            bingoBallPrefixEnum++; //Move to next prefix letter, if none start again from next line.
            if (bingoBallPrefixEnum == FBingoBallPrefixEnum.None)
                bingoBallPrefixEnum = FBingoBallPrefixEnum.B;

            addedNumbers.Add(ballData.CurrentValue);//Add created ball to ignore list.
        }

        CheckBingo();//CheckBingo to set balls left to bingo.

        BingoDirector.CheckBingoDelegate += CheckBingo; //Bind to delegate.
        BingoDirector.StartNewRoundDelegate += FindWantedLines; //Bind to delegate.
    }

    /// <summary>
    /// Binded to CheckBingoDelegate
    /// </summary>
    private void CheckBingo()
    {
        BingoCardBall[] Cardballs = GetComponentsInChildren<BingoCardBall>();//get all balldatas from this card

        FCheckBingoResult result = BingoCheck(wantedLines, Cardballs);//check if Bingo was found
        SetNumbersLeftToBingo(result.numbersToBingo);

        //BINGO!!!
        if (result.bIsBingo)
        {
            foreach (BingoCardBall ball in Cardballs)
            {
                for (int i = 0; i < result.bingoDataLine.Count; i++)
                {
                    if (ball.ballData.CurrentValue == result.bingoDataLine[i].CurrentValue)
                    {
                        ball.MarkBall();
                    }
                }
            }

            if (bingoDirector)
            {
                bingoDirector.AnnounceBingo();
                if (PlayerPrefs.HasKey("AvatarName") && PlayerPrefs.HasKey("AvatarIcon"))
                {
                    bingoDirector.AnnounceRoundWinner(PlayerPrefs.GetString("AvatarName"), bingoDirector.competitorIcons.IconSprites[PlayerPrefs.GetInt("AvatarIcon")]);
                }
            }
        }
    }

    /// <summary>
    /// Set text in Text component to show how many balls is left to bingo.
    /// </summary>
    /// <param name="numbersLeft">Numbers left to Bingo</param>
    private void SetNumbersLeftToBingo(int numbersLeft)
    {
        if (numbersLeftToBingo)
        {
            numbersLeftToBingo.text = "" + numbersLeft;
        }
    }

    private void OnDisable()
    {
        BingoDirector.CheckBingoDelegate -= CheckBingo; //Unbind to delegate.
        BingoDirector.StartNewRoundDelegate -= FindWantedLines; //Unbind to delegate.
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PLayerBingoCard : BingoCard
{

    //Ball Image shown in UI
    [Header("Prefab for the ball in BingoCard")]
    [SerializeField]
    private GameObject BallPrefab = null;

    //Text copmponent to show number how many ball left to bingo
    protected Text numbersLeftToBingo = null;

    private void Awake()
    {
        bingoDirector = FindObjectOfType<BingoDirector>();
        numbersLeftToBingo = gameObject.transform.Find("LeftToBingoText").GetComponent<Text>();

        FindWantedLines();//get currently wanted lines for win

        List<int> addedNumbers = new List<int>();

        BingoBallData ballData = new BingoBallData();
        Image CardImage = gameObject.transform.Find("CardBG").GetComponent<Image>();

        //Create UI balls to card
        for (int i = 0; i < 25; i++)
        {
            GameObject ball = Instantiate(BallPrefab);//Instantiate

            BingoCardBall bingoCardBall = ball.GetComponent<BingoCardBall>();

            ballData = new BingoBallData(bingoBallPrefixEnum, addedNumbers); //Create ballData with balls prefix letter and list of numbers that are already in the card...
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

    private void CheckBingo()
    {
        BingoCardBall[] Cardballs = GetComponentsInChildren<BingoCardBall>();

        FCheckBingoResult result = bingoCheck(wantedLines, Cardballs);
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
                        ball.markAsBingoLine();
                    }
                }
            }

            if (bingoDirector)
                bingoDirector.AnnounceBingo();
        }
    }
    private void SetNumbersLeftToBingo(int numbersLeft)
    {
        if (numbersLeftToBingo)
        {
            numbersLeftToBingo.text = "" + numbersLeft;
        }
    }
}

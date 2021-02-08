using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//BINGO prefixes
public enum FBingoBallPrefixEnum
{
    B,
    I,
    N,
    G,
    O,
    None
}

public struct FCheckBingoResult
{
    public bool bIsBingo { get; private set; }
    public int numbersToBingo { get; private set; }
    public List<BingoBallData> bingoDataLine { get; private set; }
    public FCheckBingoResult(bool wasBingo, int numbersToBingo, List<BingoBallData> bingoDataLine)
    {
        bIsBingo = wasBingo;
        this.numbersToBingo = numbersToBingo;
        this.bingoDataLine = bingoDataLine;
    }
}

public class BingoCard : MonoBehaviour
{
    //BingoDirector
    protected BingoDirector bingoDirector = null;

    //Currently wanted lines
    protected Dictionary<int, List<int>> wantedLines = new Dictionary<int, List<int>>();

    //Ball numbers prefix letter
    protected FBingoBallPrefixEnum bingoBallPrefixEnum = FBingoBallPrefixEnum.B;

    private int NumbersLeftToBingo = 5;

    public void Initialize()
    {
        bingoDirector = FindObjectOfType<BingoDirector>();

        FindWantedLines();

        List<int> addedNumbers = new List<int>();
      
        for (int i = 0; i < 25; i++)
        {
            BingoCardBall ball = gameObject.AddComponent<BingoCardBall>();
            ball.Init(new BingoBallData(bingoBallPrefixEnum, addedNumbers));

            bingoBallPrefixEnum++; //Move to next prefix letter, if none start again from next line.
            if (bingoBallPrefixEnum == FBingoBallPrefixEnum.None)
                bingoBallPrefixEnum = FBingoBallPrefixEnum.B;

            addedNumbers.Add(ball.ballData.CurrentValue);//Add created ball to ignore list.
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
        BingoCardBall[] Cardballs = gameObject.GetComponents<BingoCardBall>();

        FCheckBingoResult result = bingoCheck(wantedLines, Cardballs);
        NumbersLeftToBingo = result.numbersToBingo;

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

    protected void FindWantedLines()
    {
        if (bingoDirector != null)
            wantedLines = bingoDirector.GetWantedLines();//get currently wanted lines for win
    }


    public int GetNumbersLeftToBingo()
    {
        return NumbersLeftToBingo;
    }

    /// <summary>
    /// show numbers left to Bingo in BingoCard
    /// </summary>
    protected FCheckBingoResult bingoCheck(Dictionary<int, List<int>> wantedLines, BingoCardBall[] ballDatas)
    {
        bool bIsBingo = false;
        int numbersToBingo = 25;
        List<BingoBallData> bingoDataLine = new List<BingoBallData>();

        foreach (KeyValuePair<int, List<int>> WantedLine in wantedLines)//Loop through all WantedLines
        {
            bingoDataLine.Clear();//Clear last loop data
            bIsBingo = true; //Bingo is true untill non marked ball is found.
            int leftToBingo = 0;//count unmarked numbers to find amount left to Bingo

            foreach (int item in WantedLine.Value)//Check foreach item if it is marked or not.
            {
                BingoBallData ballData = ballDatas[item].ballData;

                if (!ballData.bIsMarked)
                {
                    bIsBingo = false;
                    leftToBingo++;
                }
                bingoDataLine.Add(ballData);//If marked add to list.
            }

            if (leftToBingo < numbersToBingo)
            {
                numbersToBingo = leftToBingo;
            }

            if (bIsBingo)
                break;
        }

        return new FCheckBingoResult(bIsBingo, numbersToBingo, bingoDataLine);
    }
    private void OnDisable()
    {
        BingoDirector.CheckBingoDelegate -= CheckBingo; //Unbind to delegate.
        BingoDirector.StartNewRoundDelegate -= FindWantedLines; //Unbind to delegate.
    }
}


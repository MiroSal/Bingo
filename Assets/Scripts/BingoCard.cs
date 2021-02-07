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

public class BingoCard : MonoBehaviour
{
    //Ball Image shown in UI
    [Header("Prefab for the ball in BingoCard")]
    [SerializeField]
    private GameObject BallPrefab = null;

    //BingoDirector
    private BingoDirector bingoDirector = null;

    //Text copmponent to show number how many ball left to bingo
    private Text numbersLeftToBingo = null;

    //UI balls in card
    private GameObject[] balls = new GameObject[25];

    //Currently wanted lines
    private Dictionary<int, List<int>> wantedLines = new Dictionary<int, List<int>>();

    //Ball numbers prefix letter
    private FBingoBallPrefixEnum bingoBallPrefixEnum = FBingoBallPrefixEnum.B;

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
            balls[i] = Instantiate(BallPrefab);//Instantiate
            if (balls[i] == null) { Debug.Log("instantiate was fail."); continue; }

            BingoCardBall bingoCardBall = balls[i].GetComponent<BingoCardBall>();
            if (bingoCardBall == null) { Debug.Log("BingoCardBall component was not found"); continue; }

            ballData = new BingoBallData(bingoBallPrefixEnum, addedNumbers); //Create ballData with balls prefix letter and list of numbers that are already in the card...
            bingoCardBall.Init(ballData); //...Initialize cardball with balldata.

            if (CardImage)
            {
                balls[i].transform.SetParent(CardImage.transform, false);//set as child.
            }

            bingoBallPrefixEnum++; //Move to next prefix letter, if none start again from next line.
            if (bingoBallPrefixEnum == FBingoBallPrefixEnum.None)
                bingoBallPrefixEnum = FBingoBallPrefixEnum.B;

            addedNumbers.Add(ballData.CurrentValue);//Add created ball to ignore list.
        }

        BingoDirector.CheckBingoDelegate += CheckBingo; //Bind to delegate.
        BingoDirector.StartNewRoundDelegate += FindWantedLines; //Bind to delegate.
    }

    /// <summary>
    /// Binded to CheckBingoDelegate
    /// </summary>
    void CheckBingo()
    {
        bool bIsBingo = false;
        int numbersToBingo = 25;
        List<BingoCardBall> bingoLine = new List<BingoCardBall>();

        foreach (KeyValuePair<int, List<int>> WantedLine in wantedLines)//Loop through all WantedLines
        {
            bingoLine.Clear();//Clear last loop data
            bIsBingo = true; //Bingo is true untill non marked ball is found.
            int leftToBingo = 0;//count unmarked numbers to find amount left to Bingo

            foreach (int item in WantedLine.Value)//Check foreach item if it is marked or not.
            {
                BingoCardBall ball = balls[item].GetComponent<BingoCardBall>();
                if (ball == null)
                    break;

                if (!ball.bIsMarked)
                {
                    bIsBingo = false;
                    leftToBingo++;
                }
                bingoLine.Add(ball);//If marked add to list.
            }

            if (leftToBingo < numbersToBingo)
            {
                numbersToBingo = leftToBingo;
            }

            if (bIsBingo)
                break;
        }

        SetNumbersLeftToBingo(numbersToBingo);

        //BINGO!!!
        if (bIsBingo)
        {
            foreach (BingoCardBall item in bingoLine)//Change Color to visualize line where BINGO was found.
            {
                if (item == null)
                    continue;

                Image image = item.GetComponent<Image>();
                if (image != null)
                {
                    image.color = Color.blue;
                }
            }

            if (bingoDirector)
                bingoDirector.AnnounceBingo();
        }
    }

    void FindWantedLines()
    {
        if (bingoDirector != null)
            wantedLines = bingoDirector.GetWantedLines();//get currently wanted lines for win
    }

    //show numbers left to Bingo in BingoCard
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [Header("Prefab for the ball in Bingocard")]
    public GameObject BallPrefab = null;

    private BingoDirector bingoDirector = null;
    private GameObject[] balls = new GameObject[25];
    private Dictionary<int, List<int>> possibleLines = new Dictionary<int, List<int>>();

    private FBingoBallPrefixEnum bingoBallPrefixEnum = FBingoBallPrefixEnum.B;


    private void Awake()
    {
        bingoDirector = FindObjectOfType<BingoDirector>();

        if (bingoDirector != null)
        possibleLines = bingoDirector.GetWantedLines();

        List<int> addedNumbers = new List<int>();
        BingoBallData ballData;

        //create balls to card
        for (int i = 0; i < 25; i++)
        {
            balls[i] = Instantiate(BallPrefab);
            if (balls[i] == null) { Debug.Log("instantiate was fail."); continue; }

            BingoCardBall bingoCardBall = balls[i].GetComponent<BingoCardBall>();
            if (bingoCardBall == null) { Debug.Log("BingoCardBall component was not found"); continue; }

            ballData = new BingoBallData(bingoBallPrefixEnum, addedNumbers);
            bingoCardBall.Init(ballData);

            balls[i].transform.SetParent(this.transform, false);

            bingoBallPrefixEnum++;
            if (bingoBallPrefixEnum == FBingoBallPrefixEnum.None)
                bingoBallPrefixEnum = FBingoBallPrefixEnum.B;

            addedNumbers.Add(ballData.CurrentValue);
        }

        BingoDirector.CheckBingoDelegate += CheckBingo;
    }

    void CheckBingo()
    {
        bool bIsBingo = false;
        List<BingoCardBall> bingoLine = new List<BingoCardBall>();

        foreach (KeyValuePair<int, List<int>> possibleLine in possibleLines)//possibleLines
        {
            bingoLine.Clear();
            bIsBingo = true;

            //check foreach item if it is marked or not.
            foreach (int item in possibleLine.Value)
            {
                BingoCardBall ball = balls[item].GetComponent<BingoCardBall>();
                if (ball == null)
                    break;

                if (!ball.GetIsMarked())
                {
                    bIsBingo = false;
                    break;
                }
                bingoLine.Add(ball);
            }
            if (bIsBingo)
                break;
        }

        //BINGO!!!
        if (bIsBingo)
        {
            foreach (BingoCardBall item in bingoLine)
            {
                if (item == null)
                    continue;

                Image image = item.GetComponent<Image>();
                if (image != null)
                {
                    image.color = Color.blue;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FBingoTicketTextEnum
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
    public GameObject BallPrefab;

    private BingoDirector bingoDirector = null;
    private GameObject[] Balls = new GameObject[25];
    private Dictionary<int, List<int>> possibleLines = new Dictionary<int, List<int>>();

    private FBingoTicketTextEnum BingoTicketTextEnum = FBingoTicketTextEnum.B;

    private void Awake()
    {
        bingoDirector = FindObjectOfType<BingoDirector>();

        if (bingoDirector != null)
            possibleLines = bingoDirector.GetWantedLines();

        List<int> AddedNumbers = new List<int>();
        BingoBallData ballData;


        //create balls to card
        for (int i = 0; i < 25; i++)
        {
            Balls[i] = Instantiate(BallPrefab);
            ballData = new BingoBallData(BingoTicketTextEnum, AddedNumbers);
            Balls[i].GetComponent<BingoCardBall>().Init(ballData);
            if (Balls[i] != null)
                Balls[i].transform.SetParent(this.transform, false);

            BingoTicketTextEnum++;
            if (BingoTicketTextEnum == FBingoTicketTextEnum.None)
                BingoTicketTextEnum = FBingoTicketTextEnum.B;

            AddedNumbers.Add(ballData.CurrentValue);
        }

        BingoDirector.CheckBingoDelegate += CheckBingo;
    }

    void CheckBingo()
    {
        bool bIsBingo = false;
        List<BingoCardBall> BingoLine = new List<BingoCardBall>();

        foreach (KeyValuePair<int, List<int>> PossibleLine in possibleLines)//possibleLines
        {
            BingoLine = new List<BingoCardBall>();
            bIsBingo = true;

            //check for each item if it is marked or not.
            foreach (int item in PossibleLine.Value)
            {
                BingoCardBall ball = Balls[item].GetComponent<BingoCardBall>();
                if (ball == null)
                    break;

                if (!ball.bIsMarked)
                {
                    bIsBingo = false;
                    break;
                }
                BingoLine.Add(ball);
            }
            if (bIsBingo)
                break;
        }

        //BINGO!!!
        if (bIsBingo)
        {
            foreach (BingoCardBall item in BingoLine)
            {
                Debug.Log(BingoLine.Count);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BallData Struct. 
/// </summary>
public struct BingoBallData
{
    //Balls number
    public int CurrentValue { get; private set; }

    //Balls Prefix letter
    public FBingoBallPrefixEnum BingoBallPrefixEnum { get; private set; }

    /// <summary>
    /// Constructor that adds appropriate letter for the given bingoball number
    /// </summary>
    public BingoBallData(int BallValue)
    {
        CurrentValue = BallValue;

        //Find the right Prefix for the number.
        if (CurrentValue <= 15 && CurrentValue > 0)
        {
            BingoBallPrefixEnum = FBingoBallPrefixEnum.B;
        }
        else if (CurrentValue <= 30 && CurrentValue > 15)
        {
            BingoBallPrefixEnum = FBingoBallPrefixEnum.I;
        }
        else if (CurrentValue <= 45 && CurrentValue > 30)
        {
            BingoBallPrefixEnum = FBingoBallPrefixEnum.N;
        }
        else if (CurrentValue <= 60 && CurrentValue > 45)
        {
            BingoBallPrefixEnum = FBingoBallPrefixEnum.G;
        }
        else if (CurrentValue <= 75 && CurrentValue > 60)
        {
            BingoBallPrefixEnum = FBingoBallPrefixEnum.O;
        }
        else
        {
            BingoBallPrefixEnum = FBingoBallPrefixEnum.None;
        }
    }

    /// <summary>
    /// Constructor that creates number for the given Prefix letter.
    /// Wanted ignored numbers are given to handle dublicates.
    /// </summary>
    public BingoBallData(FBingoBallPrefixEnum bingoTicketTextEnum, List<int> IgnoredNumbers)
    {
        int minNumber = 0;
        int maxNumber = 0;
        BingoBallPrefixEnum = bingoTicketTextEnum;

        //Find the right range for the prefix letter
        switch (BingoBallPrefixEnum)
        {
            case FBingoBallPrefixEnum.B:
                minNumber = 1;
                maxNumber = 15;
                break;
            case FBingoBallPrefixEnum.I:
                minNumber = 16;
                maxNumber = 30;
                break;
            case FBingoBallPrefixEnum.N:
                minNumber = 31;
                maxNumber = 45;
                break;
            case FBingoBallPrefixEnum.G:
                minNumber = 46;
                maxNumber = 60;
                break;
            case FBingoBallPrefixEnum.O:
                minNumber = 61;
                maxNumber = 75;
                break;
            case FBingoBallPrefixEnum.None:
                Debug.Log("This should never happen!!");
                break;
            default:
                break;
        }

        List<int> possibleNumbers = new List<int>();

        for (int i = minNumber; i < maxNumber; i++)//Sort ignored numbers
        {
            if (!IgnoredNumbers.Contains(i))
            {
                possibleNumbers.Add(i);
            }
        }
        CurrentValue = possibleNumbers[(int)Random.Range(0, possibleNumbers.Count-1)];//Create random number from PossibleNumbers.
    }
}

public class BingoDirector : MonoBehaviour
{
    //Delegates/events
    public delegate void OnNumberAnnouncedDelegate(int number);
    public static event OnNumberAnnouncedDelegate NumberAnnouncedDelegate;
    public delegate void OnCheckBingoDelegate();
    public static event OnCheckBingoDelegate CheckBingoDelegate;
    public delegate void OnBingoFoundDelegate();
    public static event OnBingoFoundDelegate BingoFoundDelegate;

    //Current GameModes LineData used
    [SerializeField]
    private LinesData CurrentGameModeLineData;

    //sorted wanted lines from LinesData.
    private Dictionary<int, List<int>> WantedLines = new Dictionary<int, List<int>>();

    private void Awake()
    {
        SortWantedLines();
    }

    //Sort wantedlines from scribtableobjects data
    private void SortWantedLines()
    {
        ArrayLayout[] lineData = new ArrayLayout[0];
        lineData = GetLinesFromData(); //get all wantedline data from current scribtableobject/CurrentGameModeLineData

        //Loop data and add Wantedlines ball index to dictionary.
        if (lineData != null)
        {
            int ballCount = 0;//
            for (int j = 0; j <= lineData.Length - 1; j++)//Loop all currently possible wantedlines that are needed for BINGO from CurrentgameModesLineData
            {
                ballCount = 0;

                List<int> bingoLine = new List<int>();//Line that is wanted for BINGO!!

                for (int i = 0; i <= lineData[j].rows.Length - 1; i++)//Iterate all rows in grid 5x5
                {
                    for (int k = 0; k <= lineData[j].rows[i].row.Length - 1; k++)//Iterate all columns in the current row
                    {
                        if (ballCount > 24)//bingocard has only 25 balls
                            break;

                        if (lineData[j].rows[i].row[k])//if bool in current position in row/column is true add to bingoLine.
                        {
                            bingoLine.Add(ballCount);
                        }
                        ballCount++;
                    }
                }
                WantedLines.Add(j, bingoLine);//add sorted lineData to dictionary
            }
        }
    }

    /// <summary>
    /// Get sorted wanted lines
    /// </summary>
    public Dictionary<int, List<int>> GetWantedLines()
    {
        return WantedLines;
    }

    /// <summary>
    /// Broadcast new number
    /// </summary>
    public void AnnounceNumber(int Number)
    {
        if (NumberAnnouncedDelegate != null)
            NumberAnnouncedDelegate(Number);

        if (CheckBingoDelegate != null)
            CheckBingoDelegate();
    }

    /// <summary>
    /// Broadcast BINGO!!!
    /// </summary>
    public void AnnounceBingo()
    {
        if (BingoFoundDelegate != null)
            BingoFoundDelegate();
    }

    /// <summary>
    /// Get scribtableobjects wanted linedata
    /// </summary>
    private ArrayLayout[] GetLinesFromData()
    {
        return CurrentGameModeLineData.GetbingoLineData();
    }
}
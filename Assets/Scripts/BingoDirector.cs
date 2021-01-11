using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BingoBallData
{
    public int CurrentValue { get; private set; }
    public FBingoBallPrefixEnum BingoBallPrefixEnum { get; private set; }

    /// <summary>
    /// Constructor that adds right letter for the number
    /// </summary>
    public BingoBallData(int BallValue)
    {
        CurrentValue = BallValue;
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
    /// Constructor that creates number for the given letter in BINGO, wanted ignored numbers are given to handle dublicates.
    /// </summary>
    public BingoBallData(FBingoBallPrefixEnum bingoTicketTextEnum, List<int> IgnoredNumbers)
    {
        int minNumber = 0;
        int maxNumber = 0;
        BingoBallPrefixEnum = bingoTicketTextEnum;
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

        do
        {
            CurrentValue = (int)Random.Range(minNumber, maxNumber);
        } while (IgnoredNumbers.Contains(CurrentValue));
    }
}

public class BingoDirector : MonoBehaviour
{
    //Delegates/events
    public delegate void OnNumberAnnouncedDelegate(int number);
    public static event OnNumberAnnouncedDelegate NumberAnnouncedDelegate;

    public delegate void OnCheckBingoDelegate();
    public static event OnCheckBingoDelegate CheckBingoDelegate;

    //Lines
    public LinesData linesData;
    private Dictionary<int, List<int>> possibleLines = new Dictionary<int, List<int>>();

    private void Awake()
    {
        SortWantedLines();
    }

    //sort wanted lines from scribtableobjects data
    private void SortWantedLines()
    {
        ArrayLayout[] lineData = new ArrayLayout[0];
        lineData = GetLinesFromData();

        //loop data and add lines ball indexes to dictionary
        if (lineData != null)
        {
            int index = 0;
            for (int j = 0; j <= lineData.Length - 1; j++)
            {
                index = 0;

                List<int> temp = new List<int>();
                for (int i = 0; i <= lineData[j].rows.Length - 1; i++)
                {
                    for (int k = 0; k <= lineData[j].rows[i].row.Length - 1; k++)
                    {
                        if (index > 24)
                            break;

                        if (lineData[j].rows[i].row[k])
                        {
                            temp.Add(index);
                        }
                        index++;
                    }
                }
                possibleLines.Add(j, temp);
            }
        }
    }

    /// <summary>
    /// Get sorted wanted lines
    /// </summary>
    public Dictionary<int, List<int>> GetWantedLines()
    {
        return possibleLines;
    }

    //Broadcast new number
    public void AnnounceNumber(int Number)
    {
        if (NumberAnnouncedDelegate != null)
            NumberAnnouncedDelegate(Number);
    }

    //Broadcast 
    public void CheckBingo()
    {
        if (CheckBingoDelegate != null)
            CheckBingoDelegate();
    }

    //get wanted scribtableobject line data
    private ArrayLayout[] GetLinesFromData()
    {
        return linesData.GetbingoLineData();
    }
}

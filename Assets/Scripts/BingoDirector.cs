using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BingoBallData
{
    public int CurrentValue;
    public FBingoTicketTextEnum BingoTicketTextEnum;

    public BingoBallData(int BallValue)
    {
        CurrentValue = BallValue;
        if (CurrentValue <= 15 && CurrentValue > 0)
        {
            BingoTicketTextEnum = FBingoTicketTextEnum.B;
        }
        else if (CurrentValue <= 30 && CurrentValue > 15)
        {
            BingoTicketTextEnum = FBingoTicketTextEnum.I;
        }
        else if (CurrentValue <= 45 && CurrentValue > 30)
        {
            BingoTicketTextEnum = FBingoTicketTextEnum.N;
        }
        else if (CurrentValue <= 60 && CurrentValue > 45)
        {
            BingoTicketTextEnum = FBingoTicketTextEnum.G;
        }
        else if (CurrentValue <= 75 && CurrentValue > 60)
        {
            BingoTicketTextEnum = FBingoTicketTextEnum.O;
        }
        else
        {
            BingoTicketTextEnum = FBingoTicketTextEnum.None;
        }
    }

    public BingoBallData(FBingoTicketTextEnum bingoTicketTextEnum, List<int> IgnoredNumbers)
    {
        int minNumber = 0;
        int maxNumber = 0;
        BingoTicketTextEnum = bingoTicketTextEnum;
        switch (BingoTicketTextEnum)
        {
            case FBingoTicketTextEnum.B:
                minNumber = 1;
                maxNumber = 15;
                break;
            case FBingoTicketTextEnum.I:
                minNumber = 16;
                maxNumber = 30;
                break;
            case FBingoTicketTextEnum.N:
                minNumber = 31;
                maxNumber = 45;
                break;
            case FBingoTicketTextEnum.G:
                minNumber = 46;
                maxNumber = 60;
                break;
            case FBingoTicketTextEnum.O:
                minNumber = 61;
                maxNumber = 75;
                break;
            case FBingoTicketTextEnum.None:
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
    public LineData lineData;
    private Dictionary<int, List<int>> possibleLines = new Dictionary<int, List<int>>();

    private void Awake()
    {
        SortWantedLines();
    }

    //sort lines from scribtableobjects data
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
    /// Get sorted wanted lines from scribtableobject data
    /// </summary>
    /// <returns></returns>
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

    private ArrayLayout[] GetLinesFromData()
    {
        return lineData.bingoLineData;
    }
}

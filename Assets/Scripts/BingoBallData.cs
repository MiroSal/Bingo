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
        CurrentValue = possibleNumbers[(int)Random.Range(0, possibleNumbers.Count - 1)];//Create random number from PossibleNumbers.
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bingo
{
    //public static FCheckBingoResult bingoCheck(FcheckBingoParams bingoparams)
    //{
    //    bool bIsBingo = false;
    //    int numbersToBingo = 25;
    //    List<BingoBallData> bingoDataLine = new List<BingoBallData>();

    //    foreach (KeyValuePair<int, List<int>> WantedLine in bingoparams.wantedLines)//Loop through all WantedLines
    //    {
    //        bingoDataLine.Clear();//Clear last loop data
    //        bIsBingo = true; //Bingo is true untill non marked ball is found.
    //        int leftToBingo = 0;//count unmarked numbers to find amount left to Bingo

    //        foreach (int item in WantedLine.Value)//Check foreach item if it is marked or not.
    //        {
    //            BingoBallData ballData = bingoparams.ballDatas[item];

    //            if (!ballData.bIsMarked)
    //            {
    //                bIsBingo = false;
    //                leftToBingo++;
    //            }
    //            bingoDataLine.Add(ballData);//If marked add to list.
    //        }

    //        if (leftToBingo < numbersToBingo)
    //        {
    //            numbersToBingo = leftToBingo;
    //        }

    //        if (bIsBingo)
    //            break;
    //    }

    //    return new FCheckBingoResult(bIsBingo, numbersToBingo, bingoDataLine);
    //}
}
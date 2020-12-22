using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

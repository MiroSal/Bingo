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
    public delegate void OnBingoFoundDelegate(bool wasLastRound);
    public static event OnBingoFoundDelegate BingoFoundDelegate;
    public delegate void OnStartNewRoundDelegate();
    public static event OnStartNewRoundDelegate StartNewRoundDelegate;

    /// <summary>
    /// Current GameModes LineData.
    /// GameMode can have multiple rounds and on each round wantedlines are different
    /// </summary>
    [SerializeField]
    private List<LinesData> currentGameModeLineDatas = new List<LinesData>();

    /// <summary>
    /// AllWantedLines of current GameMode
    /// Each index contain current rounds wanted lines
    /// </summary>
    List<Dictionary<int, List<int>>> allWantedLines = new List<Dictionary<int, List<int>>>();

    /// <summary>
    /// Current round of bingo currently playing;
    /// </summary>
    private int Roundindex = 0;

    private void Awake()
    {
        SortWantedLines();
    }

    /// <summary>
    /// Sort wantedlines from scribtableobjects data
    /// </summary>
    private void SortWantedLines()
    {
        Dictionary<int, List<int>> Lines = new Dictionary<int, List<int>>();

        foreach (LinesData linesData in currentGameModeLineDatas)//every round are searching different lines for BINGO!
        {
            Lines.Clear();
            ArrayLayout[] lineData = new ArrayLayout[0];
            lineData = GetLinesFromData(linesData); //get all wantedline data from current scribtableobject/CurrentGameModeLineData

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
                            if (ballCount > 24)//bingoCard has only 25 balls
                                break;

                            if (lineData[j].rows[i].row[k])//if bool in current position in row/column is true add to bingoLine.
                            {
                                bingoLine.Add(ballCount);
                            }
                            ballCount++;
                        }
                    }
                    Lines.Add(j, bingoLine);//add sorted lineData to dictionary

                }
            }
            allWantedLines.Add(new Dictionary<int, List<int>>(Lines));//add dictionary from all current rounds wantedlines to list
        }
    }

    /// <summary>
    /// Get sorted wanted lines
    /// return empty if no more rounds
    /// </summary>
    public Dictionary<int, List<int>> GetWantedLines()
    {
        if (allWantedLines.Count > Roundindex)
        {
            return allWantedLines[Roundindex];
        }

        return new Dictionary<int, List<int>>();
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
        {
            if (allWantedLines.Count - 1 > Roundindex) { BingoFoundDelegate(false); }
            else { BingoFoundDelegate(true); } //if this round was the last
        }
    }

    /// <summary>
    /// Get scribtableobjects wanted linedata
    /// </summary>
    private ArrayLayout[] GetLinesFromData(LinesData LineData)
    {
        return LineData.GetbingoLineData();
    }

    /// <summary>
    /// Broadcast start of new round
    /// </summary>
    public void StartNewRound()
    {
        Roundindex++;

        if (StartNewRoundDelegate != null)
        {
            StartNewRoundDelegate();
        }
    }


    public void EndGameMode()
    {
        Roundindex = 0;
    }

    /// <summary>
    /// Pause Game
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// UnPauseGame
    /// </summary>
    public void UnPauseGame()
    {
        Time.timeScale = 1;
    }
}
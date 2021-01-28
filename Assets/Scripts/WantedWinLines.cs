using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WantedWinLines : MonoBehaviour
{
    //Ball Image shown in UI
    [Header("Prefab for the ball in Bingocard")]
    public GameObject BallPrefab = null;

    //Interval to show next wanted line
    public float LoopingInterval = 5;

    //BingoDirector
    private BingoDirector bingoDirector = null;

    //UI balls to demonstrate wanted line
    private GameObject[] balls = new GameObject[25];

    //Current WantedLines for win
    private Dictionary<int, List<int>> wantedLines = new Dictionary<int, List<int>>();

    //timer for count interval
    private float timer = 0;

    //current index in wantedlines
    private int wantedLineIndex = 0;

    //Currently shown wantedline
    private List<int> currentWantedLine = new List<int>();


    private void Awake()
    {
        bingoDirector = FindObjectOfType<BingoDirector>();//get bingodirector

        if (bingoDirector != null)
            wantedLines = bingoDirector.GetWantedLines();//get wantedlines

        //Create UI balls to card
        for (int i = 0; i < 25; i++)
        {
            balls[i] = Instantiate(BallPrefab);
            if (balls[i] == null) { Debug.Log("instantiate was fail."); continue; }

            balls[i].transform.SetParent(this.transform, false);
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;

        //Change currently shown wanted line in UI
        if (timer <= 0)
        {
            List<int> lastWantedLine = new List<int>();
            
            if (!wantedLines.ContainsKey(wantedLineIndex))//if next index is not found go back to 0
            {
                wantedLineIndex = 0;
            }

            if (wantedLines.ContainsKey(wantedLineIndex))//if next index is found
            {

                lastWantedLine = currentWantedLine;
                currentWantedLine = wantedLines[wantedLineIndex];
            }

            for (int i = 0; i < balls.Length; i++)//loop through UI balls
            {
                if (lastWantedLine.Contains(i))//Change Lastwantedline back to default appearance
                {
                    Image image = balls[i].GetComponent<Image>();
                    if (image != null)
                        image.color = Color.yellow;
                }

                if (currentWantedLine.Contains(i))//change currentwantedLine to wantedline appearance
                {
                    Image image = balls[i].GetComponent<Image>();
                    if (image != null)
                        image.color = Color.blue;
                }
            }

            //prepair next wantedline
            wantedLineIndex++;
            timer = LoopingInterval;
        }
    }
}

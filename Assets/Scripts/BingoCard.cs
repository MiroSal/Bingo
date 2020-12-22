using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BingoCard : MonoBehaviour
{
    [Header("Prefab for the ball in Bingocard")]
    public GameObject BallPrefab;

    private BingoDirector bingoDirector;
    private Dictionary<int, List<GameObject>> possibleLines = new Dictionary<int, List<GameObject>>();

    private void Awake()
    {

        bingoDirector = FindObjectOfType<BingoDirector>();
        ArrayLayout[] lineData = new ArrayLayout[0];
        GameObject[] Balls = new GameObject[25];

        if (bingoDirector != null)
            lineData = bingoDirector.GetLines();

        for (int i = 0; i < 25; i++)
        {
            Balls[i] = Instantiate(BallPrefab);
            if (Balls[i] != null)
                Balls[i].transform.SetParent(this.transform, false);
        }


        if (lineData != null)
        {
            int index = 0;
            for (int j = 0; j <= lineData.Length - 1; j++)
            {
                index = 0;

                List<GameObject> temp = new List<GameObject>();
                for (int i = 0; i <= lineData[j].rows.Length - 1; i++)
                {
                    for (int k = 0; k <= lineData[j].rows[i].row.Length - 1; k++)
                    {
                        if (index > 24)
                            break;

                        if (lineData[j].rows[i].row[k])
                        {
                            GameObject ob = Balls[index];

                            temp.Add(ob);
                        }
                        index++;
                    }
                }
                possibleLines.Add(j, temp);
            }
        }

        BingoDirector.CheckBingoDelegate += CheckBingo;
    }

    void CheckBingo()
    {
        bool bIsBingo = false;
        List<GameObject> temp = new List<GameObject>();

        foreach (KeyValuePair<int, List<GameObject>> line in possibleLines)
        {
            temp = new List<GameObject>();
            bIsBingo = true;
            foreach (GameObject obj in line.Value)
            {
                BingoCardBall ball = obj.GetComponent<BingoCardBall>();
                if (ball == null)
                    break;

                if (!ball.bIsMarked)
                {
                    bIsBingo = false;
                    break;
                }

                temp.Add(obj);
            }
            if (bIsBingo)
                break;
        }


        if (bIsBingo)
        {

            foreach (GameObject item in temp)
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

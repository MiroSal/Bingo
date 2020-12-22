using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BingoCard : MonoBehaviour
{

    public GameObject BallPrefab;

    private BingoDirector bingoDirector;

    private GameObject[] Balls = new GameObject[25];

    ArrayLayout[] lineData;

    public Dictionary<int, List<GameObject>> possibleLines = new Dictionary<int, List<GameObject>>();

    private void Awake()
    {
        bingoDirector = FindObjectOfType<BingoDirector>();

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
                        Debug.Log(index);
                        if (index > 24)
                            break;

                        if (lineData[j].rows[i].row[k])
                        {
                            GameObject ob = Balls[index];

                            temp.Add(ob);
                            Image image = ob.GetComponent<Image>();
                            if (image != null)
                            {
                                image.color = Color.blue;
                            }
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
        List<GameObject> temp = new List<GameObject>(); ;

        foreach (KeyValuePair<int, List<GameObject>> line in possibleLines)
        {
            temp = new List<GameObject>();
            foreach (GameObject obj in line.Value)
            {
                bIsBingo = true;

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
                Image image = item.GetComponent<Image>();
                if (image != null)
                {
                    Debug.Log("Bingo: " + temp.Count);
                    image.color = Color.blue;
                }
            }
        }
    }
}

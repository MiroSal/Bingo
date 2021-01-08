using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberAnnouncer : MonoBehaviour
{
    private Text NumberText;
    private BingoDirector bingoDirector;

    public GameObject BallPrefap;

    List<int> numbers = new List<int>();

    private void Awake()
    {
        bingoDirector = FindObjectOfType<BingoDirector>();
        for (int i = 1; i <= 75; i++)
        {
            numbers.Add(i);
        }
        //NumberText = GetComponentInChildren<Text>();
    }

    public void GenerateNextNumber()
    {
        if (numbers.Count > 0)
        {
            int random = (int)Random.Range(0, numbers.Count - 1);
            BingoBallData nextBall = new BingoBallData(numbers[random]);
            numbers.RemoveAt(random);

            if (BallPrefap != null)
            {
                GameObject obj = Instantiate(BallPrefap);
                if (obj != null)
                {
                    obj.transform.SetParent(this.transform, false);
                    Text text = obj.GetComponentInChildren<Text>();
                    if (text != null)
                        text.text = nextBall.BingoTicketTextEnum.ToString()+ nextBall.CurrentValue.ToString();
                }
            }

            if (bingoDirector != null)
                bingoDirector.AnnounceNumber(nextBall.CurrentValue);

        }

        CheckBingo();
    }

    public void CheckBingo()
    {
        if (bingoDirector != null)
            bingoDirector.CheckBingo();
    }
}

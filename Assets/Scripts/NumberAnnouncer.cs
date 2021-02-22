using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberAnnouncer : MonoBehaviour
{
    //Bingo Director
    private BingoDirector bingoDirector;

    //All Numbers Announced
    private List<int> possibleNumbers = new List<int>();

    //Ball Image shown in stage Monitor
    [SerializeField]
    private GameObject ballPrefap;

    private void Awake()
    {
        bingoDirector = FindObjectOfType<BingoDirector>();

        //Add All possible numbers to able to announce
        for (int i = 1; i <= 75; i++)
        {
            possibleNumbers.Add(i);
        }
    }

    /// <summary>
    /// Generate next random number to Announce
    /// </summary>
    public void GenerateNextNumber()
    {
        if (possibleNumbers.Count > 0)
        {
            int random = (int)Random.Range(0, possibleNumbers.Count - 1);//Get random number from possible bingo numbers
            BingoBallData nextBall = new BingoBallData(possibleNumbers[random]);//Create next ball with given index

            if (ballPrefap == null) { Debug.Log("ballPrefap was null"); return; }

            GameObject ball = Instantiate(ballPrefap);//Create ball
            if (ball == null) { Debug.Log("objt was null"); return; }
            possibleNumbers.RemoveAt(random);//Remove used number from possibleNumbers

            int childCount = transform.childCount;
            if (transform.childCount >= 45)
            {
                GameObject child = transform.GetChild(0).gameObject;
                if (child)
                    GameObject.Destroy(child);
            }

            ball.transform.SetParent(this.transform, false);//Set ball to studio monitor
            Text text = ball.GetComponentInChildren<Text>();

            if (text == null) { Debug.Log("text was null"); return; }
            text.text = nextBall.BingoBallPrefixEnum.ToString() + nextBall.CurrentValue.ToString();//Add Number with prefix to balls textfield

            if (bingoDirector == null) { Debug.Log("bingoDirector was null"); return; }
            bingoDirector.AnnounceNumber(nextBall.CurrentValue);//Tell BingoDirector that new number was created
        }
    }
}
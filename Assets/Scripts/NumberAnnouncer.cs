using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberAnnouncer : MonoBehaviour
{
    private BingoDirector bingoDirector;
    private List<int> numbers = new List<int>();

    [SerializeField]
    private GameObject ballPrefap;

    private void Awake()
    {
        bingoDirector = FindObjectOfType<BingoDirector>();

        //All possible numbers
        for (int i = 1; i <= 75; i++)
        {
            numbers.Add(i);
        }
    }

    public void GenerateNextNumber()
    {
        if (numbers.Count > 0)
        {
            //get random from possible bingo numbers
            int random = (int)Random.Range(0, numbers.Count - 1);
            BingoBallData nextBall = new BingoBallData(numbers[random]);
            numbers.RemoveAt(random);

            //add to monitor screen
            if (ballPrefap == null) { Debug.Log("ballPrefap was null"); return; }

            GameObject obj = Instantiate(ballPrefap);
            if (obj == null) { Debug.Log("objt was null"); return; }

            obj.transform.SetParent(this.transform, false);
            Text text = obj.GetComponentInChildren<Text>();

            if (text == null) { Debug.Log("text was null"); return; }
            text.text = nextBall.BingoBallPrefixEnum.ToString() + nextBall.CurrentValue.ToString();

            if (bingoDirector == null) { Debug.Log("bingoDirector was null"); return; }
            bingoDirector.AnnounceNumber(nextBall.CurrentValue);
        }

        CheckBingo();
    }

    public void CheckBingo()
    {
        if (bingoDirector == null) { Debug.Log("bingoDirector was null"); return; }
        bingoDirector.CheckBingo();
    }
}

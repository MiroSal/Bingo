using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BingoCardBall : MonoBehaviour
{
    //Balls number
    private Text numberText;

    //Is this ball announced
    public bool bIsMarked { get; private set; } = false;

    //This balls Data
    private BingoBallData ballData;

    private void Awake()
    {
        BingoDirector.NumberAnnouncedDelegate += CheckNumber; //Register to delegate.
        bIsMarked = false;
    }

    //Initialize with BallData
    public void Init(BingoBallData BallData)
    {
        ballData = BallData;
        numberText = GetComponentInChildren<Text>();

        if (numberText == null) { Debug.Log("numberText was null"); return; }
        numberText.text = ballData.CurrentValue.ToString();
    }

    //Check if this balls number was announced
    void CheckNumber(int number)
    {
        if (ballData.CurrentValue == number)
        {
            bIsMarked = true;

            Image image = GetComponent<Image>();
            if (image != null)
                image.color = Color.green;
        }
    }

    private void OnDisable()
    {
        BingoDirector.NumberAnnouncedDelegate -= CheckNumber; //Unregister from delegate
    }
}
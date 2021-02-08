using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BingoCardBall : MonoBehaviour
{
    //Balls number
    private Text numberText;

    //This balls Data
    public BingoBallData ballData;

    [SerializeField]
    bool changeIsVisual = false;

    private void Awake()
    {
        BingoDirector.NumberAnnouncedDelegate += CheckNumber; //Register to delegate.
    }

    //Initialize with BallData
    public void Init(BingoBallData BallData)
    {
        ballData = BallData;
        numberText = GetComponentInChildren<Text>();

        if (changeIsVisual)
        {
            if (numberText == null) { Debug.Log("numberText was null"); return; }
            numberText.text = ballData.CurrentValue.ToString();
        }
    }

    //Check if this balls number was announced
    void CheckNumber(int number)
    {
        if (ballData.CurrentValue == number)
        {
            ballData.bIsMarked = true;
            if (changeIsVisual)
            {
                Image image = GetComponent<Image>();
                if (image != null)
                    image.color = Color.green;
            }
        }
    }

    public void markAsBingoLine()
    {
        if (ballData.bIsMarked)
        {
            if (changeIsVisual)
            {
                Image image = GetComponent<Image>();
                if (image != null)
                    image.color = Color.blue;
            }
        }
    }

    private void OnDisable()
    {
        BingoDirector.NumberAnnouncedDelegate -= CheckNumber; //Unregister from delegate
    }
}
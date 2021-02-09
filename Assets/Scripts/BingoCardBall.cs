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

    //If this ball is going to be shown in UI
    [SerializeField]
    bool changeIsVisual = false;

    private void Awake()
    {
        BingoDirector.NumberAnnouncedDelegate += CheckNumber; //Bind to delegate.
    }

    /// <summary>
    /// Initialize this ball with data
    /// </summary>
    /// <param name="BallData">Data to Initialize with</param>
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

    /// <summary>
    /// Check if the number of this ball was announced
    /// </summary>
    /// <param name="number">Number to check</param>
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

    /// <summary>
    /// Mark the ball as found
    /// </summary>
    public void MarkBall()
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
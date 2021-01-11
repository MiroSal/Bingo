using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BingoCardBall : MonoBehaviour
{
    private Text numberText;
    private bool bIsMarked = false;
    private BingoBallData ballData;

    private void Awake()
    {
        BingoDirector.NumberAnnouncedDelegate += CheckNumber;
        bIsMarked = false;
    }

    public void Init(BingoBallData BallData)
    {
        ballData = BallData;
        numberText = GetComponentInChildren<Text>();

        if (numberText == null) { Debug.Log("numberText was null"); return; }
        numberText.text = ballData.CurrentValue.ToString();
    }

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
        BingoDirector.NumberAnnouncedDelegate -= CheckNumber;
    }

    public bool GetIsMarked() { return bIsMarked; }
}
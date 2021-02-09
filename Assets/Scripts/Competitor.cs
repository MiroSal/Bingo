using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Competitor : MonoBehaviour
{
    //Amount of BingoCards this competitor has
    [SerializeField]
    private int BingoCardsOwned = 3;

    //Text component to show number how many balls left to bingo
    private Text numbersLeftToBingo = null;

    private void Awake()
    {
        numbersLeftToBingo = gameObject.transform.Find("NumbersLeft").GetComponent<Text>();

        for (int i = 0; i < BingoCardsOwned; i++)//Create wanted amount of Cards for this Competitor
        {
            BingoCard card = gameObject.AddComponent<BingoCard>();
            card.Initialize();

            if (numbersLeftToBingo)
            {
                numbersLeftToBingo.text = "" + card.GetNumbersLeftToBingo();
            }
        }

        BingoDirector.CheckBingoDelegate += SetNumbersLeftToBingo; //Bind to delegate.
        BingoDirector.StartNewRoundDelegate += SetNumbersLeftToBingo;//Bind to delegate.
    }

    /// <summary>
    /// Set text in Text component to show how many balls is left to bingo.
    /// </summary>
    private void SetNumbersLeftToBingo()
    {
        if (numbersLeftToBingo)
        {
            BingoCard[] cards = GetComponents<BingoCard>();
            int ballsToBingo = 25;

            foreach (BingoCard card in cards)
            {
                if (card != null && ballsToBingo > card.GetNumbersLeftToBingo())
                {
                    ballsToBingo = card.GetNumbersLeftToBingo();
                }
            }
            numbersLeftToBingo.text = "" + ballsToBingo;
        }
    }

    private void OnDisable()
    {
        BingoDirector.CheckBingoDelegate -= SetNumbersLeftToBingo; //Unbind to delegate.
        BingoDirector.StartNewRoundDelegate -= SetNumbersLeftToBingo; //Unbind to delegate.
    }
}
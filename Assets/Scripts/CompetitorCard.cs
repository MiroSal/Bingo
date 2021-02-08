using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CompetitorCard : MonoBehaviour
{
    [SerializeField]
    private int BingoCardsOwned = 3;

    //Text copmponent to show number how many ball left to bingo
    private Text numbersLeftToBingo = null;

    private void Awake()
    {
        numbersLeftToBingo = gameObject.transform.Find("NumbersLeft").GetComponent<Text>();

        for (int i = 0; i < BingoCardsOwned; i++)
        {
            BingoCard card = gameObject.AddComponent<BingoCard>();
            card.Initialize();

            if (numbersLeftToBingo)
            {
                numbersLeftToBingo.text = "" + card.GetNumbersLeftToBingo();
            }
        }

        BingoDirector.CheckBingoDelegate += SetNumbersLeftToBingo;
        BingoDirector.StartNewRoundDelegate += SetNumbersLeftToBingo;
    }

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
}

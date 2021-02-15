using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Competitor : MonoBehaviour
{
    //BingoDirector
    protected BingoDirector bingoDirector = null;

    //Amount of BingoCards this competitor has
    [SerializeField]
    private int BingoCardsOwned = 3;

    //Text component to show number how many balls left to bingo
    private Text numbersLeftToBingo_Text = null;

    //Image Component for Competitor Icon
    public Image icon_Image { get; private set; } = null;

    //Text component for Competitor name
    private Text competitorName_Text = null;

    //Name of the Competitor
    public string competitorName { get; private set; } = "";

    private void Awake()
    {
        bingoDirector = FindObjectOfType<BingoDirector>();

        numbersLeftToBingo_Text = gameObject.transform.Find("NumbersLeft").GetComponent<Text>();
        competitorName_Text = gameObject.transform.Find("Competitor").gameObject.transform.Find("PlayerName").GetComponent<Text>();
        icon_Image = gameObject.transform.Find("Competitor").gameObject.transform.Find("CompetitorIcon").GetComponent<Image>();
      
        if (bingoDirector)
        {
            if (icon_Image != null && bingoDirector.competitorIcons != null)
            {
                icon_Image.sprite = bingoDirector.competitorIcons.GetRandomIcon();
            }
            else
            {
                Debug.LogWarning("icon_Image or competitorIcons scribtableObject in Competitor was null");
            }
        }

        if (competitorName_Text != null)
        {
            competitorName = CompetitorNames.GetRandomName();
            competitorName_Text.text = competitorName;
        }
        else
        {
            Debug.LogWarning("competitorName_Text was null in Competitor");
        }

        for (int i = 0; i < BingoCardsOwned; i++)//Create wanted amount of Cards for this Competitor
        {
            GameObject card = new GameObject("Card" + i);
            card.transform.SetParent(transform, false);
            BingoCard bingocard = card.gameObject.AddComponent<BingoCard>();
            bingocard.Initialize();

            if (numbersLeftToBingo_Text)
            {
                numbersLeftToBingo_Text.text = "" + bingocard.GetNumbersLeftToBingo();
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
        if (numbersLeftToBingo_Text)
        {
            BingoCard[] cards = GetComponentsInChildren<BingoCard>();
            int ballsToBingo = 25;

            foreach (BingoCard card in cards)
            {
                if (card != null && ballsToBingo > card.GetNumbersLeftToBingo())
                {
                    ballsToBingo = card.GetNumbersLeftToBingo();
                }
            }
            numbersLeftToBingo_Text.text = "" + ballsToBingo;
        }
    }

    private void OnDisable()
    {
        BingoDirector.CheckBingoDelegate -= SetNumbersLeftToBingo; //Unbind to delegate.
        BingoDirector.StartNewRoundDelegate -= SetNumbersLeftToBingo; //Unbind to delegate.
    }
}
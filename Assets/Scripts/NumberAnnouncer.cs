using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberAnnouncer : MonoBehaviour
{
    private Text NumberText;
    private BingoDirector bingoDirector;

    private void Awake()
    {
        bingoDirector = FindObjectOfType<BingoDirector>();
        NumberText = GetComponentInChildren<Text>();
    }

    public void GenerateNextNumber()
    {
        //TODO make sure that there is no duplicate number created
        int nextnumber = (int)Random.Range(0, 75);
        NumberText.text = nextnumber.ToString();

        if (bingoDirector != null)
            bingoDirector.AnnounceNumber(nextnumber);
    }

    public void CheckBingo()
    {
        if (bingoDirector != null)
            bingoDirector.CheckBingo();
    }
}

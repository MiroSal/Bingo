using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberAnnouncer : MonoBehaviour
{
    private Text NumberText;
    private BingoDirector bingoDirector;

    public GameObject BallPrefap;

    private void Awake()
    {
        bingoDirector = FindObjectOfType<BingoDirector>();
        //NumberText = GetComponentInChildren<Text>();
    }

    public void GenerateNextNumber()
    {
        //TODO make sure that there is no duplicate number created
        int nextnumber = (int)Random.Range(0, 75);

        if (BallPrefap != null)
        {
            GameObject obj = Instantiate(BallPrefap);
            if (obj != null)
            {
                obj.transform.SetParent(this.transform, false);
                Text text = obj.GetComponentInChildren<Text>();
                if(text= null)
                text.text = nextnumber.ToString();
            }
        }

        if (bingoDirector != null)
            bingoDirector.AnnounceNumber(nextnumber);

        CheckBingo();
    }

    public void CheckBingo()
    {
        if (bingoDirector != null)
            bingoDirector.CheckBingo();
    }
}

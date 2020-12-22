using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberAnnouncer : MonoBehaviour
{
    public Text NumberText;


    private BingoDirector bingoDirector;

    private void Awake()
    {
        bingoDirector = FindObjectOfType<BingoDirector>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateNextNumber()
    {
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

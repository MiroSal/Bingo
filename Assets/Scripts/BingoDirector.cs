using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BingoDirector : MonoBehaviour
{

    public delegate void OnNumberAnnouncedDelegate(int number);
    public static event OnNumberAnnouncedDelegate NumberAnnouncedDelegate;


    public delegate void OnCheckBingoDelegate();
    public static event OnCheckBingoDelegate CheckBingoDelegate;

    public LineData lineData;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AnnounceNumber(int Number)
    {
        if(NumberAnnouncedDelegate != null)
        NumberAnnouncedDelegate(Number);
    }

    public void CheckBingo()
    {
        if (CheckBingoDelegate != null)
            CheckBingoDelegate();
    }

    public ArrayLayout[] GetLines()
    {
        return lineData.bingoLineData;
    }

}
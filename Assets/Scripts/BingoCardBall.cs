using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BingoCardBall : MonoBehaviour
{
    public Text NumberText;

    //TODO name this properly
    public bool bIsMarked = false;

    int randomNumber;

    private void Awake()
    {
        BingoDirector.NumberAnnouncedDelegate += CheckNumber;
        bIsMarked = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        randomNumber = (int)Random.Range(0f, 75f);
        NumberText.text = randomNumber.ToString();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void CheckNumber(int number)
    {
        if (randomNumber == number)
        {
            bIsMarked = true;
            GetComponent<Image>().color = Color.green;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BingoCardBall : MonoBehaviour
{
    public Text NumberText;

    //TODO name this properly
    public bool bIsMarked = false;

    private BingoBallData ballData;

    private void Awake()
    {
        BingoDirector.NumberAnnouncedDelegate += CheckNumber;
        bIsMarked = false;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Init(BingoBallData BallData)
    {
        ballData = BallData;

        if (NumberText != null)
            NumberText.text = ballData.CurrentValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {

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
}
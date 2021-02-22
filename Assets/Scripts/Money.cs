using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    Text MoneyText = null;

    private void Awake()
    {
        MoneyText = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Money"))
        {
            double money = System.Math.Round(PlayerPrefs.GetFloat("Money"),1);            
            MoneyText.text = "" + money;
        }
        else
        {
            MoneyText.text = "" + 0;
            PlayerPrefs.SetFloat("Money", 0);
            PlayerPrefs.Save();
        }
    }
}

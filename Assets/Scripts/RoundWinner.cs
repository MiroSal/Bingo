using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundWinner : MonoBehaviour
{
    public Text text { get; private set; } = null;
    public Image icon { get; private set; } = null;

    /// <summary>
    /// Setup this round winner to be shown in UI
    /// </summary>
    /// <param name="Name">Name of the winner</param>
    /// <param name="AvatarIcon">Avatar of the winner</param>
    public void Setup(string Name, Sprite AvatarIcon)
    {
        text = GetComponentInChildren<Text>();
        if (text)
            text.text = Name;

        icon = GetComponentInChildren<Image>();
        if (icon)
            icon.sprite = AvatarIcon;
    }
}

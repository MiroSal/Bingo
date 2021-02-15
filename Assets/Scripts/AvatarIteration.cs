using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarIteration : MonoBehaviour
{
    //BingoDirector
    private BingoDirector bingoDirector = null;

    //Current Avatar
    private Image avatarIcon = null;

    //Current name
    private InputField InputField;

    private void Awake()
    {
        bingoDirector = FindObjectOfType<BingoDirector>();
    }

    void Start()
    {
        if (!bingoDirector)
            return;

        avatarIcon = GetComponent<Image>();

        //check if already has chosen avatar and name in playerprefs else create default
        if (PlayerPrefs.HasKey("AvatarIcon") & bingoDirector.competitorIcons.IconSprites[PlayerPrefs.GetInt("AvatarIcon")] != null)
        {
            avatarIcon.sprite = bingoDirector.competitorIcons.IconSprites[PlayerPrefs.GetInt("AvatarIcon")];
        }
        else
        {
            if (avatarIcon && bingoDirector.competitorIcons.IconSprites.Count > 0)
            {
                avatarIcon.sprite = bingoDirector.competitorIcons.IconSprites[0];
                PlayerPrefs.SetInt("AvatarIcon", 0);
                PlayerPrefs.Save();
            }
        }

        InputField = gameObject.transform.Find("InputField").GetComponent<InputField>();
        InputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

        if (InputField && PlayerPrefs.HasKey("AvatarName"))
        {
            InputField.text = PlayerPrefs.GetString("AvatarName");
        }
        else
        {
            if (InputField)
            {
                string name = CompetitorNames.GetRandomName();
                InputField.text = name;
                PlayerPrefs.SetString("AvatarName", name);
                PlayerPrefs.Save();
            }
        }
    }

    /// <summary>
    /// Create new "Random avatar" for the player
    /// binded to inputfield
    /// </summary>
    public void NewAvatar()
    {
        if (avatarIcon && bingoDirector.competitorIcons)
        {
            if (PlayerPrefs.HasKey("AvatarIcon"))
            {
                int Index = PlayerPrefs.GetInt("AvatarIcon");
                Index++;
                if (Index >= bingoDirector.competitorIcons.IconSprites.Count)
                    Index = 0;

                if (bingoDirector.competitorIcons.IconSprites.Count > Index)
                {
                    avatarIcon.sprite = bingoDirector.competitorIcons.IconSprites[Index];
                    PlayerPrefs.SetInt("AvatarIcon", Index);
                    PlayerPrefs.Save();
                }               
            }
        }

        if (InputField)
        {
            string name = CompetitorNames.GetRandomName();
            InputField.text = name;         
        }
    }

    /// <summary>
    /// listen when inputfields value is changed and save currentvalue to playerprefs
    /// </summary>
    public void ValueChangeCheck()
    {
        PlayerPrefs.SetString("AvatarName", InputField.text.ToString());
        PlayerPrefs.Save();
    }
}

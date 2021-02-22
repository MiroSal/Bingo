using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private BingoDirector bingoDirector;

    [SerializeField]
    private List<LinesData> BasicGameModeLineDatas = new List<LinesData>();

    [SerializeField]
    private List<LinesData> SunGameModeLineDatas = new List<LinesData>();

    [SerializeField]
    private List<LinesData> LetterGameModeLineDatas = new List<LinesData>();

    private void Awake()
    {
        bingoDirector = FindObjectOfType<BingoDirector>(); ;
    }

    public void StartBasicGamemode()
    {
        if (bingoDirector)
        {
            bingoDirector.SetNewGameMode(BasicGameModeLineDatas);
            SceneLoader.LoadScene(SceneLoader.Scene.BingoStudio);
        }
    }

    public void StartSunGamemode()
    {
        if (bingoDirector)
        {
            bingoDirector.SetNewGameMode(SunGameModeLineDatas);
            SceneLoader.LoadScene(SceneLoader.Scene.BingoStudio);
        }
    }

    public void StartLetterGamemode()
    {
        if (bingoDirector)
        {
            bingoDirector.SetNewGameMode(LetterGameModeLineDatas);
            SceneLoader.LoadScene(SceneLoader.Scene.BingoStudio);
        }
    }
}
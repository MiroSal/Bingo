using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject for Competitor Icons
/// </summary>
[System.Serializable]
[CreateAssetMenu(fileName = "CompetitorIcons", menuName = "ScriptableObjects/CompetitorIcons", order = 1)]
public class CompetitorIcons : ScriptableObject
{
    public List<Sprite> IconSprites = new List<Sprite>();

    public Sprite GetRandomIcon()
    {
        if (IconSprites.Count > 0)
        {
            return IconSprites[Random.Range(0, IconSprites.Count - 1)];
        }

        return null;
    }
}
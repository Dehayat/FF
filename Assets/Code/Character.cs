using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Frames/Character")]
public class Character : ScriptableObject
{
    public Sprite GetSprite
    {
        get
        {
            if (currentHeart <= 0)
            {
                return sadSprite;
            }
            else
            {
                return sprite;
            }
        }
    }

    public Sprite sprite;
    public Sprite sadSprite;
    public string characterName;
    public int heart;
    public Frame trustDialogue;
    public float avatarYOffset;

    [NonSerialized]
    public int currentHeart;
    [NonSerialized]
    public bool justBroke;
    [NonSerialized]
    public bool finish;

    public void Init()
    {
        currentHeart = heart;
        justBroke = false;
        finish = false;
    }
}

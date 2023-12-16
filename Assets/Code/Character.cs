using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Frames/Character")]
public class Character : ScriptableObject
{
    public Sprite sprite;
    public string characterName;
    public int heart;

    [NonSerialized]
    public int currentHeart;

    public void Init()
    {
        currentHeart = heart;
    }
}

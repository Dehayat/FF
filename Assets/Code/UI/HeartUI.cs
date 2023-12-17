using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct CharacterHeart
{
    public Character character;
    public TextMeshProUGUI nameText;
    public HeartIcon heartContainer;
}

public class HeartUI : MonoBehaviour
{
    public CharacterHeart[] characterHeartUIs;

    private void Start()
    {
        foreach (var charUI in characterHeartUIs)
        {
            charUI.nameText.text = charUI.character.characterName;
        }
    }

    private void Update()
    {
        foreach (var charUI in characterHeartUIs)
        {
            charUI.heartContainer.SetHearts(charUI.character.currentHeart);
        }
    }
}

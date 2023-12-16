using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public Character[] characters;
    private void Start()
    {
        foreach (Character character in characters)
        {
            character.Init();
        }
    }
}

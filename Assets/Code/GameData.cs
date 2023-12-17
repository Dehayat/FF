using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    public Character[] characters;
    public Dictionary<Character, WorldCharacter> characterToInstance;

    private void Awake()
    {
        instance = this;
        foreach (Character character in characters)
        {
            character.Init();
        }
        characterToInstance = new();
    }

    private void Start()
    {
        var allCharacterInstances = FindObjectsOfType<WorldCharacter>();
        foreach (WorldCharacter character in allCharacterInstances)
        {
            characterToInstance.Add(character.character, character);
        }
    }
    public WorldCharacter GetWorldCharacter(Character character)
    {
        return characterToInstance[character];
    }
}

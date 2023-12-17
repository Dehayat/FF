using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CharacterDialogue
{
    public Character character;
    public Frame dialogue;
}

public class SectionDialogues : MonoBehaviour
{
    public CharacterDialogue[] newDialogues;

    public void UpdateDialogues()
    {
        foreach (var charDialogue in newDialogues)
        {
            Debug.Log(charDialogue.character + " and " +  charDialogue.dialogue);
            GameData.instance.characterToInstance[charDialogue.character].frameTrigger.frame = charDialogue.dialogue;
        }
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{

    public Image characterImage;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public GameObject dialogueContainer;

    private Dialogue currentDialogue;
    private int currentTextIndex = 0;

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentTextIndex = 0;
        characterImage.sprite = currentDialogue.character.sprite;
        nameText.text = dialogue.character.characterName;
        dialogueText.text = currentDialogue.text[currentTextIndex];
        characterImage.gameObject.SetActive(true);
        dialogueContainer.SetActive(true);
        foreach (var action in dialogue.actions)
        {
            action.DoAction();
        }
    }
    public bool NextText()
    {
        currentTextIndex++;
        if (currentTextIndex >= currentDialogue.text.Length)
        {
            return false;
        }
        dialogueText.text = currentDialogue.text[currentTextIndex];
        return true;
    }
    public void StopDialogue()
    {
        dialogueContainer.SetActive(false);
    }
    public void HideCharacter()
    {
        characterImage.gameObject.SetActive(false);
    }
}
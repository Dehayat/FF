using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Image characterImage;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public GameObject dialogueContainer;

    private Dialogue currentDialogue;
    private int StringPos = 0;

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        StringPos = 0;
        characterImage.sprite = currentDialogue.character.sprite;
        nameText.text = dialogue.character.characterName;
        dialogueText.text = currentDialogue.text[StringPos];
        characterImage.gameObject.SetActive(true);
        dialogueContainer.SetActive(true);
    }
    public void NextText()
    {
        StringPos++;
        dialogueText.text = currentDialogue.text[StringPos];
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
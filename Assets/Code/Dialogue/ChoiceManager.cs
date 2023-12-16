using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    public GameObject[] choiceButtons;
    public GameObject choiceContainer;
    public DialogueTimeline timeline;

    private Choice currentChoice;
    public void StartChoice(Choice choice)
    {
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].SetActive(false);
            choiceButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
        }
        currentChoice = choice;
        for (int i = 0; i < currentChoice.options.Length; i++)
        {
            choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentChoice.options[i].choice;
            int x = i;
            choiceButtons[i].GetComponent<Button>().onClick.AddListener(() => { SetChoice(x); });
            choiceButtons[i].SetActive(true);
        }
        choiceContainer.SetActive(true);
    }
    public void SetChoice(int choice)
    {
        Debug.Log(choice + "Chosen");
        currentChoice.SetChoice(choice);
        timeline.NextFrame();
    }
}

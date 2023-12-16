using UnityEngine;

[System.Serializable]
public struct ChoiceOption
{
    public string choice;
    public Frame nextFrame;
}

[CreateAssetMenu(menuName = "Frames/Choice")]
public class Choice : Frame
{
    public ChoiceOption[] options;

    private int currentChoice = 0;

    public void SetChoice(int choice)
    {
        currentChoice = choice;
    }
    public override Frame GetNextFrame()
    {
        return options[currentChoice].nextFrame;
    }
}

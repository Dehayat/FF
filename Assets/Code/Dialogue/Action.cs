using System;

[System.Serializable]
public class Action
{
    public enum ActionType
    {
        ChangeHeart,
        ChangeDialogue
    }

    public ActionType actionType;
    public Character target;
    public int heartChange;
    public Frame nextDialogue;

    public void DoAction()
    {
        switch (actionType)
        {
            case ActionType.ChangeHeart:
                ChangeHeartAction();
                break;
            case ActionType.ChangeDialogue:
                ChangeDialogueAction();
                break;
        }
    }

    private void ChangeDialogueAction()
    {

    }

    private void ChangeHeartAction()
    {
        target.currentHeart += heartChange;
    }
}

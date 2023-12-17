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
        GameData.instance.GetWorldCharacter(target).frameTrigger.frame = nextDialogue;
    }

    private void ChangeHeartAction()
    {
        int prev = target.currentHeart;
        target.currentHeart += heartChange;
        target.currentHeart += 70;
        target.currentHeart %= 7;
        if (prev > 0 && target.currentHeart <= 0)
        {
            target.justBroke = true;
        }
        if (target.justBroke && target.currentHeart > 0)
        {
            target.justBroke = false;
        }
    }
}

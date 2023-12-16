[System.Serializable]
public class Action
{
    public enum ActionType
    {
        ChangeHeart,
    }

    public ActionType actionType;
    public Character target;
    public int heartChange;

    public void DoAction()
    {
        switch (actionType)
        {
            case ActionType.ChangeHeart:
                ChangeHeartAction();
                break;
        }
    }

    private void ChangeHeartAction()
    {
        target.currentHeart += heartChange;
    }
}

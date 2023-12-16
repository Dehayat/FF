using UnityEngine;
using UnityEngine.Events;

public class DialogueTimeline : MonoBehaviour
{
    public void StartFrame(Frame frame)
    {
        dialogueView.SetActive(true);
        currentFrame = frame;
        HandleDialogue(frame);
        HandleChoice(frame);
    }
    public void NextFrame()
    {
        if (currentFrame != null)
        {
            var nextFrame = currentFrame.GetNextFrame();
            if (nextFrame != null)
            {
                currentFrame = nextFrame;
                StartFrame(currentFrame);
            }
            else
            {
                dm.HideCharacter();
                dialogueView.SetActive(false);
                onFinish?.Invoke();
            }
        }
    }
    public void OnInput()
    {
        if (waitingForInput)
        {
            UpdateDialogue();
        }
    }

    private void HandleDialogue(Frame frame)
    {
        Dialogue dialogue = frame as Dialogue;
        if (dialogue != null)
        {
            dm.StartDialogue(dialogue);
            waitingForInput = true;
        }
        else
        {
            dm.StopDialogue();
        }
    }
    private void HandleChoice(Frame frame)
    {
        Choice choice = frame as Choice;
        if (choice != null)
        {
            cm.StartChoice(choice);
        }
        else
        {
            cm.StopChoice();
        }
    }
    public DialogueManager dm;
    public ChoiceManager cm;
    public GameObject dialogueView;
    public UnityEvent onFinish;

    private Frame currentFrame;
    private bool waitingForInput = false;

    private void UpdateDialogue()
    {
        Dialogue dialogue = currentFrame as Dialogue;
        if (dialogue != null)
        {
            if (!dm.NextText())
            {
                NextFrame();
                waitingForInput = false;
            }
        }
    }
}

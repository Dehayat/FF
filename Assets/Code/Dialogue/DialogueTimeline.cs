using System;
using System.Collections;
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
                if (HeartWasBroken())
                {
                    StartCoroutine(HeartBreakSequence());
                }
                else
                {
                    onFinish?.Invoke();
                }
            }
        }
    }

    private IEnumerator HeartBreakSequence()
    {
        heartBreakAnim.gameObject.SetActive(true);
        heartBreakAnim.Play("Break");
        yield return new WaitForSeconds(1.5f);
        heartBreakAnim.gameObject.SetActive(false);
        onFinish?.Invoke();
    }

    private bool HeartWasBroken()
    {
        bool result = false;
        foreach (var chara in GameData.instance.characters)
        {
            if (chara.justBroke)
            {
                result = true;
                chara.justBroke = false;
            }
        }
        return result;
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
    public Animator heartBreakAnim;

    private Frame currentFrame;
    private bool waitingForInput = false;

    private void UpdateDialogue()
    {
        Dialogue dialogue = currentFrame as Dialogue;
        if (dialogue != null)
        {
            if (!dm.NextText())
            {
                waitingForInput = false;
                NextFrame();
            }
        }
    }
}

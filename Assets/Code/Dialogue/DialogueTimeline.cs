using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTimeline : MonoBehaviour
{
    public void StartFrame(Frame frame)
    {
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
                StartFrame(nextFrame);
            }
            else
            {
                dm.HideCharacter();
            }
        }
    }

    private void HandleDialogue(Frame frame)
    {
        Dialogue dialogue = frame as Dialogue;
        if (dialogue != null)
        {
            dm.StartDialogue(dialogue);
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
    }
    public DialogueManager dm;
    public ChoiceManager cm;

    private Frame currentFrame;

}

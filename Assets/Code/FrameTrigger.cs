using UnityEngine;

public class FrameTrigger : MonoBehaviour
{
    public Frame frame;

    public bool CanUse()
    {
        return frame != null;
    }
}

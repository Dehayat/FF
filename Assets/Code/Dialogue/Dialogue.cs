using UnityEngine;

[CreateAssetMenu(menuName = "Frames/Dialogue")]
public class Dialogue : Frame
{
    public Character character;
    public Frame nextFrame;
    public string[] text;

    public override Frame GetNextFrame()
    {
        return nextFrame;
    }
}

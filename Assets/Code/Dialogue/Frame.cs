using UnityEngine;

public class Frame : ScriptableObject
{
    public virtual Frame GetNextFrame()
    {
        return null;
    }
}

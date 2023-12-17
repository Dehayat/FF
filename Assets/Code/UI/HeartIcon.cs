using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartIcon : MonoBehaviour
{
    public Sprite empty;
    public Sprite heart;
    public Image[] hearts;

    public void SetHearts(int count)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = empty;
        }
        for (int i = 0; i < count && i < hearts.Length; i++)
        {
            hearts[i].sprite = heart;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScreen : MonoBehaviour
{
    private bool canSkip = false;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void LetSkip()
    {
        canSkip = true;
    }
    public void ShutDown()
    {
        canSkip = false;
        anim.Play("Empty");
    }
    private void Update()
    {
        if (canSkip)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
            {
                ShutDown();
            }
        }
    }
}

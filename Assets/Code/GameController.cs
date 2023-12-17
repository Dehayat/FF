using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    enum Act
    {
        Act1,
        Act2,
        Act3,
    }

    public Character act1FinishCharacter;
    public Character act2FinishCharacter;
    public SectionDialogues ac1Sec1;
    public SectionDialogues ac1Sec2;
    public SectionDialogues ac2Sec1;
    public SectionDialogues ac2Sec2;
    public SectionDialogues ac3Sec1;
    public static bool isLastAct = false;

    private HashSet<Character> talkedToCharacters;
    private Player player;
    private Act currentAct;
    private int currentSection;

    private void Start()
    {
    }

    private void StartAct1()
    {
        Debug.Log("act1");
        talkedToCharacters.Clear();
        currentAct = Act.Act1;
        currentSection = 1;
        ac1Sec1.UpdateDialogues();
        isLastAct = false;
    }

    private void StartAct2()
    {
        Debug.Log("act2");
        talkedToCharacters.Clear();
        ac2Sec1.UpdateDialogues();
        currentAct = Act.Act2;
        currentSection = 1;
    }
    private void StartAct3()
    {
        Debug.Log("act3");
        talkedToCharacters.Clear();
        ac3Sec1.UpdateDialogues();
        currentAct = Act.Act3;
        currentSection = 1;
        isLastAct = true;
    }

    private void OnTalkedToCharacter(Character character)
    {
        talkedToCharacters.Add(character);
    }
    private void Update()
    {
        switch (currentAct)
        {
            case Act.Act1:
                Act1();
                break;
            case Act.Act2:
                Act2();
                break;
            case Act.Act3:
                Act3();
                break;
        }
    }

    private void Act1()
    {
        if (currentSection == 1)
        {
            if (talkedToCharacters.Count == GameData.instance.characters.Length)
            {
                StartAct1Section2();
            }
        }
        else if (currentSection == 2)
        {
            if (talkedToCharacters.Contains(act1FinishCharacter))
            {
                StartAct2();
            }
        }
    }
    private void Act2()
    {
        if (currentSection == 1)
        {
            if (talkedToCharacters.Count == GameData.instance.characters.Length)
            {
                StartAct2Section2();
            }
        }
        else if (currentSection == 2)
        {
            if (talkedToCharacters.Contains(act2FinishCharacter))
            {
                StartAct3();
            }
        }
    }

    private void Act3()
    {
    }

    private void StartAct1Section2()
    {
        Debug.Log("act1 Sec2");
        ac1Sec2.UpdateDialogues();
        talkedToCharacters.Clear();
        currentSection = 2;
    }
    private void StartAct2Section2()
    {
        Debug.Log("act2 Sec2");
        ac2Sec2.UpdateDialogues();
        talkedToCharacters.Clear();
        currentSection = 2;
    }

    internal void DoThing()
    {
        talkedToCharacters = new HashSet<Character>();
        player = FindObjectOfType<Player>();
        player.onTalkToCharacter.AddListener(OnTalkedToCharacter);
        StartAct1();
    }
}

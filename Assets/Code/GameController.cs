using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject gameOverScreen;
    public string winText;
    public TextMeshProUGUI overText;
    public string loseText;
    [Header("Acts intro")]
    public string act1Intro;
    public string act2Intro;
    public string act3Intro;
    public GameObject introScreen;

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
        introScreen.GetComponentInChildren<TextMeshProUGUI>().text = act1Intro;
        introScreen.GetComponent<Animator>().Play("ActStart");
    }

    private void StartAct2()
    {
        Debug.Log("act2");
        talkedToCharacters.Clear();
        ac2Sec1.UpdateDialogues();
        currentAct = Act.Act2;
        currentSection = 1;
        introScreen.GetComponentInChildren<TextMeshProUGUI>().text = act2Intro;
        introScreen.GetComponent<Animator>().Play("ActStart");
    }
    private void StartAct3()
    {
        Debug.Log("act3");
        talkedToCharacters.Clear();
        ac3Sec1.UpdateDialogues();
        currentAct = Act.Act3;
        currentSection = 1;
        isLastAct = true;
        introScreen.GetComponentInChildren<TextMeshProUGUI>().text = act3Intro;
        introScreen.GetComponent<Animator>().Play("ActStart");
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
        if (talkedToCharacters.Count == GameData.instance.characters.Length)
        {
            int trust = 0;
            foreach (var character in talkedToCharacters)
            {
                if (character.heart > 0)
                {
                    trust++;
                }
            }
            if (trust == 6)
            {
                overText.text = winText;
            }
            else
            {
                overText.text = loseText;
            }
            gameOverScreen.SetActive(true);
            player.CantMove();
        }
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

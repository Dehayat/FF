
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public void EnterDoor(string doorExit)
    {
        player.transform.position = exitToPosition[doorExit].position;
    }

    public static DoorManager instance;

    private Dictionary<string, Transform> exitToPosition;
    private Player player;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        player = FindObjectOfType<Player>();
        exitToPosition = new();
        var doorExits = FindObjectsOfType<DoorExit>();
        foreach (var door in doorExits)
        {
            exitToPosition.Add(door.exitName, door.transform);
        }
    }
}

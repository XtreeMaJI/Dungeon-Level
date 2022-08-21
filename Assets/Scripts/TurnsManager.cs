using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnsManager : MonoBehaviour
{
    private LevelManager levelManager;

    public enum Turn
    {
        Player = 0,
        Enemy = 1
    }

    public Turn currentTurn { get; private set; }

    private void Start()
    {
        currentTurn = Turn.Player;
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void SwitchTurn()
    {
        if (currentTurn == Turn.Player)
            currentTurn = Turn.Enemy;
        else
            currentTurn = Turn.Player;

        if(levelManager)
            levelManager.RestartTimer();
    }

}

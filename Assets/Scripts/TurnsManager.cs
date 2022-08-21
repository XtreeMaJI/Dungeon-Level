using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnsManager : MonoBehaviour
{
    public enum Turn
    {
        Player = 0,
        Enemy = 1
    }

    public Turn currentTurn { get; private set; }

    private void Start()
    {
        currentTurn = Turn.Player;
    }

    public void SwitchTurn()
    {
        if (currentTurn == Turn.Player)
            currentTurn = Turn.Enemy;
        else
            currentTurn = Turn.Player;
    }

}

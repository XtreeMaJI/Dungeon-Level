using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHandler : MonoBehaviour
{
    private Player playerObject;
    private TurnsManager turnsManager;

    void Start()
    {
        playerObject = GetComponent<Player>();
        turnsManager = FindObjectOfType<TurnsManager>();
    }

    public void InteractWithCell(HexCell cell)
    {
        if (turnsManager.currentTurn != TurnsManager.Turn.Player)
            return;

        if (cell && !cell.surroundingCells.Contains(playerObject.currentCell))
            return;

        if(playerObject.TryMoveToCell(cell))
            turnsManager.SwitchTurn();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHandler : MonoBehaviour
{
    private Player player;
    private TurnsManager turnsManager;
    private LevelManager levelManager;

    void Start()
    {
        player = GetComponent<Player>();
        turnsManager = FindObjectOfType<TurnsManager>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void InteractWithCell(HexCell cell)
    {
        if (turnsManager.currentTurn != TurnsManager.Turn.Player)
            return;

        if (cell && !cell.surroundingCells.Contains(player.currentCell))
            return;

        if(player.TryMoveToCell(cell))
            turnsManager.SwitchTurn();

        if (cell.objInCell is Enemy)
        {
            player.Attack((cell.objInCell as Enemy));
            turnsManager.SwitchTurn();
        }

        if (cell.objInCell is DungeonEntrance)
        {
            if(levelManager.IsWin())
            {
                (player.UI as PlayerUI).ShowWinPanel();
            }
            else
            {
                (player.UI as PlayerUI).ShowLeavePanel();
            }
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapAttachableObject))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHandler : MonoBehaviour
{
    private MapAttachableObject playerObject;

    void Start()
    {
        playerObject = GetComponent<MapAttachableObject>();
    }

    public void InteractWithCell(HexCell cell)
    {
        if (cell && !cell.surroundingCells.Contains(playerObject.currentCell))
            return;

        if (cell.isBlockedByWall)
            return;

        playerObject.TryMoveToCell(cell);
    }

}

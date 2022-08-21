using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAttachableObject : MonoBehaviour
{
    public HexCell currentCell { get; private set; }

    public bool TryMoveToCell(HexCell cell)
    {
        if (!cell)
            return false;

        if(cell.objInCell && cell.objInCell != this)
            return false;

        if (cell.isBlockedByWall)
            return false;

        Vector3 newPos = cell.transform.position;
        newPos.y = transform.position.y;
        transform.position = newPos;

        if(currentCell)
            currentCell.objInCell = null;

        currentCell = cell;
        currentCell.objInCell = this;

        return true;
    }

}

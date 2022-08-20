using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(HexCellConstructor))]
public class HexCell : MonoBehaviour
{
    public MapAttachableObject objInCell = null;
    public List<HexCell> surroundingCells;

    public void FillSurroundingCells(List<HexCell> allCells)
    {
        surroundingCells = new List<HexCell>();
        float radius = GetComponent<HexCellConstructor>().radius;

        foreach (HexCell cell in allCells)
        {
            if (cell == this)
                continue;

            Vector3 cellPos = cell.transform.position;

            if ((transform.position - cellPos).magnitude <= 2 * radius)
                surroundingCells.Add(cell);
        }

    }

}

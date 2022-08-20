using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(HexMapGenerator))]
public class HexMap : MonoBehaviour
{
    private List<HexCell> cellsList;
    private HexMapGenerator hexMapGenerator;

    private void Start()
    {
        cellsList = GetComponent<HexMapGenerator>().GetCellsList();

        foreach (HexCell cell in cellsList)
        {
            cell.FillSurroundingCells(cellsList);
        }
    }

    public HexCell GetClosestCellToPos(Vector3 pos)
    {
        HexCell closestCell = null;
        closestCell = cellsList.OrderBy(cell => (cell.transform.position - pos).magnitude).First();

        return closestCell;
    }

}

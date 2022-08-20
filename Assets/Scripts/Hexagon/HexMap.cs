using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(HexMapGenerator))]
public class HexMap : MonoBehaviour
{
    private List<HexCell> cellsList = new List<HexCell>();
    private HexMapGenerator hexMapGenerator;

    private void Start()
    {
        List<HexCell> cellsList = GetComponent<HexMapGenerator>().GetCellsList();

        foreach (HexCell cell in cellsList)
        {
            cell.FillSurroundingCells(cellsList);
        }
    }

    public HexCell GetClosestCellToPos(Vector3 pos)
    {
        List<HexCell> cellsList = GetComponent<HexMapGenerator>().GetCellsList();
        HexCell closestCell = null;
        if (cellsList.Count == 0)
            return null;

        closestCell = cellsList.OrderBy(cell => (cell.transform.position - pos).magnitude).First();

        return closestCell;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(HexCellConstructor))]
public class HexCell : MonoBehaviour
{
    public MapAttachableObject objInCell = null;
    public List<HexCell> surroundingCells;
    public bool isBlockedByWall = false;

    private void Start()
    {
        TestBlockByWall();
    }

    //Проверка, находится ли коллайдер стены внутри клетки
    public void TestBlockByWall()
    {
        float radius = GetComponent<HexCellConstructor>().radius;
        int maxColliders = 5;
        Collider[] colliders = new Collider[maxColliders];
        Physics.OverlapSphereNonAlloc(transform.position, radius / 2, colliders);
        foreach(var collider in colliders)
        {
            if (collider && collider.tag == "Wall")
                isBlockedByWall = true;
        }
    }

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

    public bool IsFree()
    {
        return objInCell == null && !isBlockedByWall;
    }

}

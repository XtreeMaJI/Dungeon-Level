using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
public class HexMapGenerator : MonoBehaviour
{
    private const float ANGLE_BETWEEN_HEX_RADIUS_AND_HEIGHT = 30f;
    private const float COLLIDER_HEIGHT = 0.25f;

    public HexCell hexCellTemplate;

    public float verticalBorderShift = 0f;
    public float horizontalBorderShift = 0f;

    public float mapWidth = 1f;
    public float mapLen = 1f;

    public float cellRadius = 0f;
    public float filledBorderPart = 0f; //??????? ??????? ? ?????????
    private float halfCellWidth = 0f;

    private float horizontalCellOffset = 0f;
    private float verticalCellOffset = 0f;

    private int currentRow = 1;

    public bool shouldRecalculateMap = false;

    private List<HexCell> cellsList = new List<HexCell>();

    private void Start()
    {
        shouldRecalculateMap = false;
    }

    void Update()
    {
        if (!shouldRecalculateMap)
            return;

        shouldRecalculateMap = false;

        RecalculateMap();
    }

    private void CreateCell(Vector3 pos)
    {
        HexCell newCell = Instantiate(hexCellTemplate, pos, Quaternion.identity);
        cellsList.Add(newCell);
        newCell.transform.parent = this.transform;

        var newCellConstr = newCell.GetComponent<HexCellConstructor>();
        newCellConstr.borderWidth = filledBorderPart;
        newCellConstr.radius = cellRadius;
        newCellConstr.UpdateMesh();
    }

    private bool CanCreateCell(out Vector3 pos)
    {
        if (cellsList.Count == 0)
        {
            pos = new Vector3(horizontalBorderShift, 0, verticalBorderShift);
            return true;
        }

        pos = cellsList.Last().gameObject.transform.position - this.transform.position;
        pos.x += horizontalCellOffset;
        //???????? ??????? ?????? ??????
        if (pos.x < mapWidth - horizontalBorderShift)
        {
            return true;
        }

        pos.z += verticalCellOffset;
        //???? ?? ??????????, ???????? ??????? ?? ????????? ???
        if (pos.z < mapLen - verticalBorderShift)
        {
            //???? ????? ???? ????????, ?? ?????? ?????? ? ???????? ?????? ??????
            if (currentRow % 2 == 0)
                pos.x = horizontalBorderShift;
            else
                pos.x = horizontalBorderShift + horizontalCellOffset / 2;

            currentRow++;
            return true;
        }

        return false;
    }

    private void DestroyAllCells()
    {
        cellsList.RemoveAll(x => x == null);
        foreach (var cell in cellsList)
            DestroyImmediate(cell.gameObject);

        cellsList.Clear();

        foreach (Transform child in transform)
            DestroyImmediate(child.gameObject);

        currentRow = 1;
    }

    private void RecalculateMap()
    {
        if (cellRadius <= 0f || filledBorderPart <= 0f || filledBorderPart >= 1f)
            return;

        DestroyAllCells();
        halfCellWidth = cellRadius * Mathf.Cos(Mathf.Deg2Rad * ANGLE_BETWEEN_HEX_RADIUS_AND_HEIGHT);
        float cellBorderWidth = filledBorderPart * cellRadius;
        float halfCellSideLen = cellRadius * Mathf.Sin(Mathf.Deg2Rad * ANGLE_BETWEEN_HEX_RADIUS_AND_HEIGHT);

        horizontalCellOffset = 2f * halfCellWidth - cellBorderWidth;
        verticalCellOffset = cellRadius + halfCellSideLen - cellBorderWidth;

        Vector3 cellPos;
        while (CanCreateCell(out cellPos))
        {
            cellPos += this.transform.position;
            CreateCell(cellPos);
        }

        //????????????? ??????? ??????????
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.size = new Vector3(mapWidth + cellRadius, COLLIDER_HEIGHT, mapLen + cellRadius);
        boxCollider.center = new Vector3((mapWidth - cellRadius) / 2, 0, (mapLen - cellRadius) / 2);
    }

    public List<HexCell> GetCellsList()
    {
        //????????, ??? ?????????? ? ?????? ????
        if (cellsList.Count == 0)
        {
            foreach(Transform child in transform)
            {
                HexCell hexCell = child.GetComponent<HexCell>();
                if(hexCell)
                    cellsList.Add(hexCell);
            }
        }

        return new List<HexCell>(cellsList);
    }

}

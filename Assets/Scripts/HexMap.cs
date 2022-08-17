using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    private const float ANGLE_BETWEEN_HEX_RADIUS_AND_HEIGHT = 30f;

    public HexCell hexCellTemplate; 

    public float verticalBorderShift = 0f;
    public float horizontalBorderShift = 0f;

    public float mapWidth = 1f;
    public float mapLen = 1f;

    private List<HexCell> cellsList = new List<HexCell>();

    private float cellRadius = 0f;
    private float halfCellWidth = 0f;

    private float horizontalCellOffset = 0f;
    private float verticalCellOffset = 0f;

    private int currentRow = 1;

    void Start()
    {
        var hexCellConstr = hexCellTemplate.GetComponent<HexCellConstructor>();
        cellRadius = hexCellConstr.radius;
        halfCellWidth = cellRadius * Mathf.Cos(Mathf.Deg2Rad * ANGLE_BETWEEN_HEX_RADIUS_AND_HEIGHT);
        float cellBorderWidth = hexCellConstr.borderWidth * cellRadius;
        float halfCellSideLen = cellRadius * Mathf.Sin(Mathf.Deg2Rad * ANGLE_BETWEEN_HEX_RADIUS_AND_HEIGHT);

        horizontalCellOffset = 2f * halfCellWidth - cellBorderWidth;
        verticalCellOffset = cellRadius + halfCellSideLen - cellBorderWidth;

        Vector3 cellPos; 
        while(CanCreateCell(out cellPos))
        {
            CreateCell(cellPos);
        }
    }

    void CreateCell(Vector3 pos)
    {
        cellsList.Add(Instantiate(hexCellTemplate, pos, Quaternion.identity));
    }

    bool CanCreateCell(out Vector3 pos)
    {
        if (cellsList.Count == 0)
        {
            pos = new Vector3(horizontalBorderShift, 0, verticalBorderShift);
            return true;
        }
            
        pos = cellsList.Last().gameObject.transform.position;
        pos.x += horizontalCellOffset;
        //Пытаемся создать ячейку справа
        if (pos.x < mapWidth - horizontalBorderShift)
        {
            return true;
        }

        pos.z += verticalCellOffset;
        //Если не получилось, пытаемся перейти на следующий ряд
        if (pos.z < mapLen - verticalBorderShift)
        {
            //Если номер ряда нечётный, то делаем отступ в половину ширины ячейки
            if (currentRow % 2 == 0)
                pos.x = horizontalBorderShift;
            else
                pos.x = horizontalBorderShift + horizontalCellOffset / 2;

            currentRow++;
            return true;
        }

        return false;
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(HexMapGenerator))]
[RequireComponent(typeof(HexMap))]
public class MapAttacher : MonoBehaviour
{
    public bool shouldAttachObjects = false;

    private void Start()
    {
        attachAllAttachableObjects();
    }

    void Update()
    {
        if (!shouldAttachObjects)
            return;

        shouldAttachObjects = false;
        attachAllAttachableObjects();
    }

    public void attachObject(MapAttachableObject obj)
    {
        if (!obj)
            return;

        HexMap hexMap = GetComponent<HexMap>();
        if (!hexMap)
            return;
        HexCell closestCell = hexMap.GetClosestCellToPos(obj.transform.position);
        obj.TryMoveToCell(closestCell);
    }

    public void attachAllAttachableObjects()
    {
        var objectsList = FindObjectsOfType<MapAttachableObject>();
        foreach(var obj in objectsList)
        {
            attachObject(obj);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(HexMapGenerator))]
public class MapAttacher : MonoBehaviour
{
    public bool shouldAttachObjects = false;

    void Update()
    {
        if (!shouldAttachObjects)
            return;

        shouldAttachObjects = false;
        attachAllAttachableObjects();
    }

    public void attachObject(Transform objTransform)
    {
        List<HexCell> cellsList = GetComponent<HexMapGenerator>().GetCellsList();
        HexCell closestCell = cellsList.OrderBy(cell => (cell.transform.position - objTransform.position).magnitude).First();

        Vector3 newObjPos = closestCell.transform.position;
        newObjPos.y = objTransform.position.y;
        objTransform.position = newObjPos;
    }

    public void attachAllAttachableObjects()
    {
        var objectsList = FindObjectsOfType<MapAttachableObject>();
        foreach(var obj in objectsList)
        {
            attachObject(obj.transform);
        }
    }

}

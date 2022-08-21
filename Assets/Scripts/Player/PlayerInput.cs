using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerInput : MonoBehaviour
{
    private float MAX_RAY_DISTANCE = 100f;
    private HexMap hexMap;
    private PlayerInputHandler inputHandler;

    void Start()
    {
        hexMap = FindObjectOfType<HexMap>();
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            HexCell intersectCell = FindIntersectCell();
            inputHandler.InteractWithCell(intersectCell);
        }
            
    }

    HexCell FindIntersectCell()
    {
        var mainCamera = Camera.main;
        Vector3 mousePos = Input.mousePosition;
        Ray cameraRay = mainCamera.ScreenPointToRay(mousePos);

        RaycastHit hit;
        LayerMask layer = LayerMask.GetMask("HexMap");
        if(Physics.Raycast(cameraRay, out hit, MAX_RAY_DISTANCE, layer))
            return hexMap.GetClosestCellToPos(hit.point);

        return null;
    }

}

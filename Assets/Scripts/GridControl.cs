using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControl : MonoBehaviour
{
    [SerializeField] Grid targetGrid;
    [SerializeField] LayerMask terrainLayermask;

    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("GridControl Update");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Debug.Log(ray);

            if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayermask))
            {
                Vector2Int gridPosition = targetGrid.GetGridPosition(hit.point);
                GridObject gridObject = targetGrid.GetPlacedObject(gridPosition);
                if (gridObject == null)
                {
                    Debug.Log("x=" + gridPosition.x + "y=" + gridPosition.y + " is empty");
                }
                else 
                {
                    Debug.Log("x=" + gridPosition.x + "y=" + gridPosition.y + gridObject.GetComponent<Character>().Name);
                }
            }
        }
    }
}

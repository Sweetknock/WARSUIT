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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, float.MaxValue, terrainLayermask))
            {
                Vector2Int gridPosition = targetGrid.GetGridPosition(hit.point);
                Debug.Log(gridPosition);
            }
        }
    }
}

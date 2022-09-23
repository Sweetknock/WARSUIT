using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    [SerializeField] Grid targetGrid;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        Debug.Log("Start Grid Object_Init");
        //Debug.Log(targetGrid);
        Vector2Int positionOnGrid = targetGrid.GetGridPosition(transform.position);
        Debug.Log("Start Grid Object_Init2");
        Debug.Log(targetGrid);
        Debug.Log(positionOnGrid);
        Debug.Log(this);
        targetGrid.PlaceObject(positionOnGrid, this);
        Debug.Log("Start Grid Object_Init3");
    }
}

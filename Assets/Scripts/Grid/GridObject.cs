using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    private Grid _targetGrid;
    public Grid targetGrid
    {
        get { return _targetGrid; }
        set { _targetGrid= GameObject.Find("Grid").GetComponent<Grid>();}
    }

    public Vector2Int positionOnGrid;



    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        targetGrid = GameObject.Find("Grid").GetComponent<Grid>();
        Debug.Log("Start Grid Object_Init");
        Debug.Log(targetGrid);
        positionOnGrid = targetGrid.GetGridPosition(transform.position);
        targetGrid.PlaceObject(positionOnGrid, this);
        Vector3 pos = targetGrid.GetWorldPosition(positionOnGrid.x, positionOnGrid.y, true);
        transform.position = pos;
    }   
}

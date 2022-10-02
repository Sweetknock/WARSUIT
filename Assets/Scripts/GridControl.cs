using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControl : MonoBehaviour
{
    [SerializeField] Grid targetGrid;
    [SerializeField] LayerMask terrainLayermask;

    PathFinding pathfinding;
    Vector2Int currentPosition = new Vector2Int();
    List<PathNode> path;
    private void Start()
    {
        Debug.Log("pathfinding");
        pathfinding = targetGrid.GetComponent<PathFinding>();
        Debug.Log(pathfinding);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("GridControl Update");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayermask))
            {
                Vector2Int gridPosition = targetGrid.GetGridPosition(hit.point);
                Debug.Log(currentPosition);
                Debug.Log(gridPosition);
                Debug.Log(pathfinding);
                path = pathfinding.FindPath(currentPosition.x, currentPosition.y, gridPosition.x, gridPosition.y);
                currentPosition = gridPosition;

                //Code for clicking on the grid and returning the coornates clicked on.
                /*
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
                */
            }
        }
    }

    private void OnDrawGizmos()
    {
        if( path ==null) { return; }
        if (path.Count == 0) { return; }

        for( int i = 0; i<path.Count-1; i++ )
                {
            Gizmos.DrawLine(targetGrid.GetWorldPosition(path[i].pos_x, path[i].pos_y, true),
                targetGrid.GetWorldPosition(path[i+1].pos_x, path[i+1].pos_y, true));
        }
    }
}

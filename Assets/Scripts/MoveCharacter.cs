using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    [SerializeField] Grid targetGrid;
    [SerializeField] LayerMask terrainLayermask;
    [SerializeField] GridObject targetCharacter;

    PathFinding pathfinding;
    List<PathNode> path;
    private void Start()
    {
        pathfinding = targetGrid.GetComponent<PathFinding>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayermask))
            {
                Vector2Int gridPosition = targetGrid.GetGridPosition(hit.point);

                Debug.Log(pathfinding);
                Debug.Log(targetCharacter);
                Debug.Log(targetCharacter.positionOnGrid.x);

                path = pathfinding.FindPath(targetCharacter.positionOnGrid.x, targetCharacter.positionOnGrid.y, gridPosition.x, gridPosition.y);

                if (path is null) { return; }
                if (path.Count == 0) { return; }

                Debug.Log(targetCharacter);
                targetCharacter.GetComponent<Movement>().Move(path);
            }
        }
    }
}

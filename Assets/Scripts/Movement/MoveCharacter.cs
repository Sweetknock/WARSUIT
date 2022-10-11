using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    [SerializeField] Grid targetGrid;
    [SerializeField] LayerMask terrainLayermask;
    GridObject targetCharacter;
    PathFinding pathfinding;
    List<PathNode> path;
    private void Start()
    {
        pathfinding = targetGrid.GetComponent<PathFinding>();
    }

    private void Update()
    {
        targetCharacter = GameObject.Find("Erza(Clone)").GetComponent<GridObject>();
        if (Input.GetKeyDown("m"))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayermask))
            {
                Vector2Int gridPosition = targetGrid.GetGridPosition(hit.point);
                Debug.Log("Moev Update");
                Debug.Log(targetCharacter);
                path = pathfinding.FindPath(targetCharacter.positionOnGrid.x, targetCharacter.positionOnGrid.y, gridPosition.x, gridPosition.y);

                if (path is null) { return; }
                if (path.Count == 0) { return; }

                targetCharacter.GetComponent<Movement>().Move(path);
            }
        }
    }
}

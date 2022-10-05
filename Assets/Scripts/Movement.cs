using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    GridObject gridObject;
    List<Vector3> pathWorldPositions;
    CharacterAnimator characterAnimator;
    [SerializeField] float moveSpeed = 1f;
    private void Awake()
    {
        gridObject = GetComponent<GridObject>();
        characterAnimator = GetComponentInChildren<CharacterAnimator>();
    }

    internal void Move(List<PathNode> path)
    {
        pathWorldPositions = gridObject.targetGrid.ConvertPathNodesToWorldPosition(path);
        gridObject.positionOnGrid.x = path[path.Count - 1].pos_x;
        gridObject.positionOnGrid.y = path[path.Count - 1].pos_y;

        RotateCharacter();

        characterAnimator.StartMoving();
    }

    private void RotateCharacter()
    {        
        Vector3 direction = (pathWorldPositions[0] - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void Update()
    {
        if(pathWorldPositions is null) { return; }
        if(pathWorldPositions.Count == 0) { return; }

        transform.position = Vector3.MoveTowards(transform.position, pathWorldPositions[0], moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, pathWorldPositions[0]) < 0.05f)
        { 
            pathWorldPositions.RemoveAt(0);
            if (pathWorldPositions.Count == 0) { characterAnimator.StopMoving(); }
            else {
                //Debug.Log(transform.position);
                //Debug.Log(transform.rotation);
                //Debug.Log(pathWorldPositions[0]);

                RotateCharacter();
                    }
        }
    }

}



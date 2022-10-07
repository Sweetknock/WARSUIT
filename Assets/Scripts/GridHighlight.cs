using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHighlight : MonoBehaviour
{
    Grid grid;
    [SerializeField] GameObject movePoint;
    List<GameObject> movePointGO;
    [SerializeField] List<Vector2Int> testTargetPosition;
    private void Start()
    {
        grid = GetComponent<Grid>();
        movePointGO = new List<GameObject>();
        Debug.Log("Highilight Test Inputs");
        Highlight(testTargetPosition);
    }

    private GameObject CreateMovePointHighlightObject()
    {
        GameObject go = Instantiate(movePoint);
        movePointGO.Add(go);
        return go;
    }

    private GameObject GetMovePointGO(int i)
    {
        if(movePointGO.Count < i)
        {
            return movePointGO[i];
        }

        GameObject newHighlighObject = CreateMovePointHighlightObject();
        return newHighlighObject;
    }

    public void Highlight(List<Vector2Int> positions)
    {

        Debug.Log(positions.Count);
        for (int i = 0; i < positions.Count; i++)
        {
            Highlight(positions[i].x, positions[i].y, GetMovePointGO(i));
        }
    }

    public void Highlight(int posX, int posY, GameObject highlightObject)
    {
        Debug.Log("highligh");
        Vector3 postion = grid.GetWorldPosition(posX, posY, true);
        Debug.Log(postion);
        postion += Vector3.up * 0.2f;
        Debug.Log(postion);
        highlightObject.transform.position = postion;    
    }



}

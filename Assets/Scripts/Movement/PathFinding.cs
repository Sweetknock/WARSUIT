using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public int pos_x;
    public int pos_y;

    public float gValue;
    public float hValue;
    public float fValue
    {
     get {return gValue + hValue;}
    }

    public PathNode parentNode;
    public PathNode(int xPos, int yPos)
    {
        pos_x = xPos;
        pos_y = yPos;
    }
}

[RequireComponent(typeof(Grid))]
public class PathFinding : MonoBehaviour
{
    Grid gridMap;
    PathNode[,] pathNodes;

    private void Awake()
    {
        Debug.Log("Path finding init!");
        Init();
    }

    private void Init()
    {
        if (gridMap is null) { gridMap = GetComponent<Grid>(); }
        gridMap = GetComponent<Grid>();
        pathNodes = new PathNode[gridMap.width, gridMap.length];

        for (int x =0; x < gridMap.width; x++ )
        {
            for (int y =0; y < gridMap.length; y++ )
            {
                pathNodes[x,y] = new PathNode(x,y);
            }
        }
    }
    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        Debug.Log("Start Findpath");
        Debug.Log(pathNodes);
        PathNode startNode = pathNodes[startX, startY];
        PathNode endNode = pathNodes[endX, endY];

        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();

        openList.Add(startNode);
        Debug.Log("Start of Loop");
        while (openList.Count > 0)
        {
            PathNode currentNode = openList[0];
            for (int i = 0; i < openList.Count; i++)
            {
                if (currentNode.fValue > openList[i].fValue)
                {
                    currentNode = openList[i];
                }

                if (currentNode.fValue == openList[i].fValue
                    && currentNode.hValue > openList[i].hValue)
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == endNode)
            {
                return RetracePath(startNode, endNode);
            }

            List<PathNode> neighborNodes = new List<PathNode>();
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if (x == 0 && y == 0) { continue; }
                    if (gridMap.CheckBoundery(currentNode.pos_x + x, currentNode.pos_y + y) == false) { continue; } 
                
                    neighborNodes.Add(pathNodes[currentNode.pos_x + x, currentNode.pos_y +y]);
                }
            }

            for (int i = 0; i < neighborNodes.Count; i++)
            {
                if (closedList.Contains(neighborNodes[i])) { continue; }
                if (gridMap.CheckWalkable(neighborNodes[i].pos_x, neighborNodes[i].pos_y) == false) { continue; }

                float movementCost = currentNode.gValue + CalculatedDistance(currentNode, neighborNodes[i]);
            
                if(openList.Contains(neighborNodes[i]) == false
                    || movementCost < neighborNodes[i].gValue)
                {
                    neighborNodes[i].gValue = movementCost;
                    neighborNodes[i].hValue = CalculatedDistance(neighborNodes[i], endNode);
                    neighborNodes[i].parentNode = currentNode;

                    if(openList.Contains(neighborNodes[i])==false)
                    {
                        openList.Add(neighborNodes[i]);
                    }
                }
            }
        }
        Debug.Log("Return Find Path Null");
        return null;
    }

    private int CalculatedDistance(PathNode currentNode, PathNode target)
    {
        int distX = Mathf.Abs(currentNode.pos_x - target.pos_x);
        int distY = Mathf.Abs(currentNode.pos_y - target.pos_y);

        if (distX > distY) { return 14 * distY + 10 * (distX - distY); }

        //Debug.Log($"Return Calculated Distance: {14 * distY + 10 * (distX - distY)}");
        return 14 * distY + 10 * (distX - distY);
    }

    private List<PathNode> RetracePath(PathNode startNode, PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();

        PathNode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }
        path.Reverse();

        Debug.Log("Return Retrace Path:");
        Debug.Log(path);
        return path;
    }
}

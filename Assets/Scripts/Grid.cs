using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This program is to control the behavior of the nodes. Includes collision detection and 
 * nodes will turn red when in contact with an object with obstacle layer.
*/

public class Grid : MonoBehaviour
{
	Node[,] grid;
	[SerializeField] int width = 25;
	[SerializeField] int length = 25;
	[SerializeField] float cellSize = 1f;
	[SerializeField] LayerMask obstacleLayer;

	private void Start()
	{
		GenerateGrid();
	}

	private void GenerateGrid()
	{
		grid = new Node[length, width];
		CheckPassableTerrain();
	}

	private void CheckPassableTerrain()
	{	
		for (int y = 0; y < width; y++)
			for (int x = 0; x < length; x++)
			{
				Vector3 worldPosition = GetWorldPosition(x, y);
				bool passable = !Physics.CheckBox(worldPosition, Vector3.one/2 * cellSize, Quaternion.identity, obstacleLayer);
				grid[x, y] = new Node();
				grid[x, y].passable = passable;
			}
	}

	public Vector2Int GetGridPosition(Vector3 worldPosition)
	{ 
		worldPosition -= transform.position;
		Vector2Int postionOnGrid = new Vector2Int((int)(worldPosition.x / cellSize), (int)(worldPosition.z / cellSize));
		return postionOnGrid;
	}


	private void OnDrawGizmos()
	{
		if (grid == null) { return; }
		for (int x = 0; x< length; x++)
		{
			for(int y = 0; y< length; y++)
			{
				Vector3 pos = GetWorldPosition(x, y);
				Gizmos.color = grid[x, y].passable ? Color.white : Color.red;
				Gizmos.DrawCube(pos, Vector3.one/4);
			}
		}
	}

	private Vector3 GetWorldPosition(int x, int y)
	{ 
		return new Vector3(transform.position.x + (x * cellSize), 0f, transform.position.z + (y * cellSize));
	}
}

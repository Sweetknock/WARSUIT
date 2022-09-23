using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This program is to control the behavior of the nodes. Includes collision detection and 
 * nodes will turn red when in contact with an object with obstacle layer.
*/

public class Grid : MonoBehaviour
{
	static Node[,] grid;
	[SerializeField] int width = 25;
	[SerializeField] int length = 25;
	[SerializeField] float cellSize = 1f;
	[SerializeField] LayerMask obstacleLayer;

	private void Awake()
	{
		Debug.Log("Start Grid Awake");
		Debug.Log(grid);
		GenerateGrid();
	}

	public void GenerateGrid()
	{
		Debug.Log("Start Grid Generate Grid");
		grid = new Node[length, width];
		for (int y = 0; y < width; y++)
		{
			for (int x = 0; x < length; x++)
			{
				grid[x, y] = new Node();
			}
		}
		CheckPassableTerrain();
	}

	private void CheckPassableTerrain()
	{
		Debug.Log("CheckPassableTerrain");
		for (int y = 0; y < width; y++)
			for (int x = 0; x < length; x++)
			{
				Vector3 worldPosition = GetWorldPosition(x, y);
				bool passable = !Physics.CheckBox(worldPosition, Vector3.one / 2 * cellSize, Quaternion.identity, obstacleLayer);
				grid[x, y] = new Node();
				grid[x, y].passable = passable;
			}
	}

	private void OnDrawGizmos()
	{
		if (grid is null) { return; }
		for (int x = 0; x < length; x++)
		{
			for (int y = 0; y < width; y++)
			{
				Vector3 pos = GetWorldPosition(x , y);
				Gizmos.color = grid[x, y].passable ? Color.white : Color.red;
				Gizmos.DrawCube(pos, Vector3.one / 2);
			}
		}
	}

	private Vector3 GetWorldPosition(int x, int y)
	{
		int new_x = x  - ((length - 1) / 2);
		int new_y = y - ((width - 1) / 2);
		return new Vector3(transform.position.x + (new_x * cellSize), 0f, transform.position.z + (new_y * cellSize));
	}


	public Vector2Int GetGridPosition(Vector3 worldPosition)
	{
		Debug.Log("Start Grid GetGridPostion");
		Debug.Log(grid);
		Debug.Log(length);
		worldPosition -= transform.position;
		Debug.Log(worldPosition);
		Vector2Int postionOnGrid = new Vector2Int((int)(worldPosition.x), (int)(worldPosition.z));
		//Vector2Int postionOnGrid = new Vector2Int((int)(worldPosition.x / cellSize), (int)(worldPosition.z / cellSize));
		Debug.Log(postionOnGrid);
		return postionOnGrid;
	}

	public void PlaceObject(Vector2Int positionOnGrid, GridObject gridObject)
	{
		Debug.Log("Start Grid PlaceObject");
		grid[positionOnGrid.x, positionOnGrid.y].gridObject1 = gridObject;
	}

	internal GridObject GetPlacedObject(Vector2Int gridPosition)
	{
		Debug.Log("Start Grid GetPlacedObject");
		Debug.Log(grid);
		GridObject gridObject = grid[gridPosition.x, gridPosition.y].gridObject1;
		return gridObject;
	}
}

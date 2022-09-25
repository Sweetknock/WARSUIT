using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This program is to control the behavior of the nodes. Includes collision detection and 
 * nodes will turn red when in contact with an object with obstacle layer.
*/

public class Grid : MonoBehaviour
{
	static Node[,] grid;
	[SerializeField] static int width = 25;
	[SerializeField] static int length = 25;
	[SerializeField] static float cellSize = 1.0f;
	[SerializeField] LayerMask obstacleLayer;
	[SerializeField] LayerMask terrainLayer;

	private void Awake()
	{
		Debug.Log("Start Grid Awake");
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
		CalculateElevation();
		CheckPassableTerrain();
	}

	private void CalculateElevation()
	{
		Debug.Log("Calculate Elevation");
		for (int y = 0; y < width; y++)
			for (int x = 0; x < length; x++)
			{
				Ray ray = new Ray(GetWorldPosition(x, y) + Vector3.up * 100f, Vector3.down);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayer))
				{
					grid[x, y].elevation = hit.point.y;
					Debug.Log(grid[x, y].elevation);
				}
			}
	}

	private void CheckPassableTerrain()
	{
		Debug.Log("CheckPassableTerrain");
		for (int y = 0; y < width; y++)
			for (int x = 0; x < length; x++)
			{
				Vector3 worldPosition = GetWorldPosition(x, y, true);
				bool passable = !Physics.CheckBox(worldPosition, Vector3.one / 2 * cellSize, Quaternion.identity, obstacleLayer);
				grid[x, y].passable = passable;
			}
	}

	private void OnDrawGizmos()
	{
		if (grid is null)
			for (int x = 0; x < length; x++)
			{
				for (int y = 0; y < width; y++)
				{
					Vector3 pos = GetWorldPosition(x, y);
					Gizmos.DrawCube(pos, Vector3.one / 2);
				}
			}
		else { 
			for (int x = 0; x < length; x++)
			{
				for (int y = 0; y < width; y++)
				{
					Vector3 pos = GetWorldPosition(x, y);
					Gizmos.color = grid[x, y].passable ? Color.white : Color.red;
					Gizmos.DrawCube(pos, Vector3.one / 2);
				}
			}
			}
	}

	private Vector3 GetWorldPosition(int x, int y, bool elevation = false)
	{
		return new Vector3(x * cellSize, elevation ==true ? grid[x,y].elevation : 0f, y * cellSize);
	}


	public Vector2Int GetGridPosition(Vector3 worldPosition)
	{
		Debug.Log("Start Grid GetGridPostion");
		Vector2Int postionOnGrid = new Vector2Int((int)(worldPosition.x / cellSize), (int)(worldPosition.z / cellSize));
		return postionOnGrid;
	}

	public void PlaceObject(Vector2Int positionOnGrid, GridObject gridObject)
	{
		Debug.Log("Start Grid PlaceObject");
		if (CheckBoundery(positionOnGrid) == true)
			{ 
			grid[positionOnGrid.x, positionOnGrid.y].gridObject1 = gridObject;
			Debug.Log(gridObject.GetComponent<Character>().Name + " placed successfully!");
			Debug.Log(positionOnGrid);
		}
		else
			Debug.Log(gridObject.GetComponent<Character>().Name + " is out of bounds!");
	}

	public bool CheckBoundery(Vector2Int positionOnGrid)
	{
        if (positionOnGrid.x<0 || positionOnGrid.x>=length)
			return false;
		if (positionOnGrid.y < 0 || positionOnGrid.y >= width)
			return false;

		return true;
	}

	internal GridObject GetPlacedObject(Vector2Int gridPosition)
	{
		if (CheckBoundery(gridPosition) == true)
		{
			Debug.Log("Start Grid GetPlacedObject");
			GridObject gridObject = grid[gridPosition.x, gridPosition.y].gridObject1;
			return gridObject;
		}
		return null;
	}
}

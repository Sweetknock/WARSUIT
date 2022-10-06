using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This program is to control the behavior of the nodes. Includes collision detection and 
 * nodes will turn red when in contact with an object with obstacle layer.
*/


//Main grid object
public class Grid : MonoBehaviour
{
	static Node[,] grid;

	[SerializeField] LayerMask obstacleLayer;
	[SerializeField] LayerMask terrainLayer;
	[SerializeField] static float cellSize = 1.667f;
	[SerializeField] static int _width = 30;
	[SerializeField] static int _length = 30;
	public int width
	{
		get
		{
			return _width;
		}
	}

	public int length
	{
		get
		{
			return _length;
		}
	}



	
	//Initialize the Grid based on width length and cell size.
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
	
	//Uses ray casting from the Main camera to calculate the elevation of the grid bases on the giving terrain. 
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
					Gizmos.DrawCube(pos, Vector3.one / 6);
				}
			}
		else { 
			for (int x = 0; x < length; x++)
			{
				for (int y = 0; y < width; y++)
				{
					Vector3 pos = GetWorldPosition(x, y, true);
					Gizmos.color = grid[x, y].passable ? Color.white : Color.red;
					Gizmos.DrawCube(pos, Vector3.one / 6);
				}
			}
			}
	}

	public Vector3 GetWorldPosition(int x, int y, bool elevation = false)
	{
		return new Vector3(x * cellSize, elevation ==true ? grid[x,y].elevation : 0f, y * cellSize);
	}

    public Vector2Int GetGridPosition(Vector3 worldPosition)
	{
		Debug.Log("Start Grid GetGridPostion");
		worldPosition.x -= cellSize / 2;
		worldPosition.z -= cellSize / 2;
		Vector2Int postionOnGrid = new Vector2Int((int)(worldPosition.x / cellSize), (int)(worldPosition.z / cellSize));
		return postionOnGrid;
	}

	public void PlaceObject(Vector2Int positionOnGrid, GridObject gridObject)
	{
		Debug.Log(length);
		if (CheckBoundery(positionOnGrid) == true)
			{ 
			grid[positionOnGrid.x, positionOnGrid.y].gridObject1 = gridObject;
			Debug.Log(gridObject.GetComponent<Character>().characterName + " placed successfully!");
			Debug.Log(positionOnGrid);
		}
		else
			Debug.Log(gridObject.GetComponent<Character>().characterName + " is out of bounds!");
	}


    public bool CheckBoundery(Vector2Int positionOnGrid)
	{
		Debug.Log(positionOnGrid);
		if (positionOnGrid.x<0 || positionOnGrid.x>=length)
			return false;
		if (positionOnGrid.y < 0 || positionOnGrid.y >= width)
			return false;

		return true;
	}

	public bool CheckBoundery(int posX, int posY)
	{
		if (posX < 0 || posX >= length)
			return false;
		if (posY < 0 || posY >= width)
			return false;

		return true;
	}

	public bool CheckWalkable(int pos_x, int pos_y)
	{
		return grid[pos_x, pos_y].passable;

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

	public List<Vector3> ConvertPathNodesToWorldPosition(List<PathNode> path)
	{
		List<Vector3> worldPositions = new List<Vector3>();
		for (int i = 0; i < path.Count; i++)
		{
			worldPositions.Add(GetWorldPosition(path[i].pos_x, path[i].pos_y, true));
		}

		return worldPositions;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Responsible for grid.
public class GridManager : MonoBehaviour
{
    // Grid size for example 3x3.
    [SerializeField] Vector2Int gridSize;
    [SerializeField] int unityGridSize = 10; 
    public int UnityGridSize{ get{ return unityGridSize; } }


    // Grid is a dictionary.
    // The KEY is the coordinates of the node.
    // The VALUE is the Node (fyi Node also has coordinates same as KEY).
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    // Just a property. It is responsible for returning grid. 
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    private void Awake()
    {
        CreateGrid();
    }


    // We are creating the grid according to the given grid size.
    // This grid has a KEY, VALUE pair.
    // KEY is the coordinates
    // VALUE is the node.
    private void CreateGrid()
    {
        for(int x = 0; x < gridSize.x; x++)
        {
            for(int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));
            }
        }
    }

    // Returning the node of the given coordinates.
    public Node GetNode(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
            return grid[coordinates];
        return null;
    }

    // Block the tile in the world.
    public void BlockMethod(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }


    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);

        return coordinates;
    }
    
    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.x = coordinates.x * unityGridSize;
        position.z = coordinates.y * unityGridSize;

        return position;
    }

    public void ResetNode()
    {
        foreach(KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }
    
}

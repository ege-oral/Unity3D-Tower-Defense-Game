using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Responsible for grid.
public class GridManager : MonoBehaviour
{
    // Grid size for example 3x3.
    [SerializeField] Vector2Int gridSize; 

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

    // Returning the node of the given coordinates.
    public Node GetNode(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
            return grid[coordinates];
        return null;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates; // Start coordinates of the startNode.
    public Vector2Int StartCoordinates { get { return startCoordinates; } }

    [SerializeField] Vector2Int destinationCoordinates; // Destination coordinates of the destinationNode.
    public Vector2Int DestinationCoordinates { get { return destinationCoordinates; } }

    Node startNode; // Start Node.
    Node destinationNode; // Destination Node.
    Node currentSearchNode; // Current Node.


    Queue<Node> frontier = new Queue<Node>(); // Creating a Queue for Nodes.
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    // We use directions in exploring. We will be exploring neighbors according to given directions order.
    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    private void Awake() 
    {
        gridManager = FindObjectOfType<GridManager>();   
        if(gridManager != null)
        {
            grid = gridManager.Grid;
            startNode = grid[startCoordinates];
            destinationNode = grid[destinationCoordinates];
        }        
    }

    void Start()
    {   
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoordinates);
    }

    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNode();
        BreadthFirstSearch(coordinates);
        return BuildPath();
    }

    void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;

        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(grid[coordinates]); // We add start node to queue.
        reached.Add(coordinates, grid[coordinates]);

        while(frontier.Count > 0 && isRunning)
        {
            // Make the current node the first element in the queue and remove that element.
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();

            // If we find our destination point then break.
            if(currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    private void ExploreNeighbors()
    {
        // Neighbors nodes to our current node.
        List<Node> neighbors = new List<Node>();

        foreach(Vector2Int direction in directions)
        {
            // We check every neighbor node in direction order (right, left, up, down).
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;

            // If it is a valid node then we add this node to our neighbor node list.
            if(grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
            }
        }

        // We add our neighbors to the queue.
        foreach(Node neighbor in neighbors)
        {
            // If the node not in reached dictionary and walkable.
            if(!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                // We add node in to the reached dictionary and the queue.
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
            
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while(currentNode.connectedTo != null)
        {
            currentNode.isPath = true;
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
        }
        currentNode.isPath = true;
        path.Reverse();
        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;
            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = previousState;
            
            if(newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }
        return false;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// Every walkable block in the world.
public class Node
{
    public Vector2Int coordinates; // Every block has a coordinate.
    public bool isWalkable;        // Whether is walkable or not.
    public bool isExplored;        // Whether is explored or not.
    public bool isPath;            // Whether is path or not.
    public Node connectedTo;       // And finally another block (node/nodes) that connect to this block.


    // Construction.
    public Node(Vector2Int coordinates, bool isWalkable)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
    }

}

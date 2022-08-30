using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// It will always execute the code in editor or in game.
[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);
    

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    
    GridManager gridManager;


    // Start is called before the first frame update
    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;

        gridManager = FindObjectOfType<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GetTileCoordinateAndDisplay();
        SetLabelColor();
        ToggleLabels();
    }

    private void GetTileCoordinateAndDisplay()
    {
        // Because it is 3D world we get z position instead of y position.
        // UnityEditor.EditorSnapSettings.move refer for snapping distance.
        if(gridManager == null) { return; }
        coordinates.x = (int) (transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = (int) (transform.parent.position.z /gridManager.UnityGridSize);
        label.text = $"{coordinates.x},{coordinates.y}";
    }

    private void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }

    private void SetLabelColor()
    {
        if(gridManager == null) { return; }
        
        Node node = gridManager.GetNode(coordinates);

        if(node == null) { return; }

        if(!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if(node.isPath)
        {
            label.color = pathColor;
        }
        else if(node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    private void ToggleLabels()
    {
        
        if(Input.GetKeyDown(KeyCode.C))
        {
            // Toggle the label value.
            label.enabled = !label.IsActive();
        }
    }
}

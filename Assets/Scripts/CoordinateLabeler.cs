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
    [SerializeField] Color blockColor = Color.gray;
    

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    Waypoint waypoint;


    // Start is called before the first frame update
    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        waypoint = GetComponentInParent<Waypoint>();
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
        coordinates.x = (int) (transform.parent.position.x / 10);
        coordinates.y = (int) (transform.parent.position.z / 10);
        label.text = $"{coordinates.x},{coordinates.y}";
    }

    private void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }

    private void SetLabelColor()
    {
        if(waypoint.IsPlaceable)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockColor;
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

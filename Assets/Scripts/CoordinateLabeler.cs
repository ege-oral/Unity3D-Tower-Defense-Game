using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// It will always execute the code in editor or in game.
[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();

    // Start is called before the first frame update
    void Awake()
    {
        label = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(!Application.isPlaying)
        GetTileCoordinateAndDisplay();
        UpdateObjectName();
    }

    private void GetTileCoordinateAndDisplay()
    {
        // Because it is 3D world we get z position instead of y position.
        // UnityEditor.EditorSnapSettings.move refer for snapping distance.
        coordinates.x = (int) (transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = (int) (transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        label.text = $"{coordinates.x},{coordinates.y}";
    }

    private void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
public class GridController : MonoBehaviour
{

    private Grid grid;
    [SerializeField] private Tilemap interactiveMap = null;
    [SerializeField] private Tile hoverTile = null;


    private Vector3Int previousMousePos = new Vector3Int();

    void Start()
    {
        grid = gameObject.GetComponent<Grid>();
    }

    void Update()
    {
        Vector3Int mousePos = GetMousePosition();
        if (!mousePos.Equals(previousMousePos))
        {
            interactiveMap.SetTile(previousMousePos, null);
            interactiveMap.SetTile(mousePos, hoverTile);
            previousMousePos = mousePos;
        }
    }

    Vector3Int GetMousePosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }

}

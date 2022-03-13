using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldToolManager : MonoBehaviour
{
    public Tilemap currentTilemap;

    public Grid currentGrid;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.125f);

        //Vector3Int tilemapPos = currentTilemap.WorldToCell(Camera.main.ScreenToWorldPoint(mousePos));

        Vector3Int gridPos = currentGrid.WorldToCell(Camera.main.ScreenToWorldPoint(mousePos));

        //Debug.Log("Tpos: " + tilemapPos);

        Debug.Log("Gpos: " + gridPos);

        //Tile tile = currentTilemap.GetTile<Tile>(tilemapPos);

        Tile tile2 = currentTilemap.GetTile<Tile>(gridPos);

        // Debug.Log(tile);
        Debug.Log(tile2);
    }
}

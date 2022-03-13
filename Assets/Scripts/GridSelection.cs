using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridSelection : MonoBehaviour
{
    public Tilemap tilemap;
    public Transform selector;

    void Update()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 2.125f;

        Vector3Int tilemapPos = tilemap.WorldToCell(worldPos);
        selector.position = tilemap.GetCellCenterWorld(tilemapPos);
        Debug.Log(selector.position);
    }
}

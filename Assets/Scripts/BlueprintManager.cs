using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class BlueprintManager : MonoBehaviour
{
    public static BlueprintManager current;

    public GridLayout GridLayout;
    public Tilemap CurrentLayer;
    public Tilemap TempTilemap;

    public static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();

    public Building temp;
    private Vector3 prevPos;

    // Unity Methods
    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        if(!temp)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {

            if (!temp.Placed)
            {
                Vector2 touchPos = Camera.main.WorldToScreenPoint(Input.mousePosition);
                Vector3Int cellPos = GridLayout.LocalToCell(touchPos);

                if (prevPos != cellPos)
                {
                    temp.transform.localPosition = GridLayout.CellToLocalInterpolated(cellPos + new Vector3(.3f, -0.1f, 0f));
                    prevPos = cellPos;
                }
            }    
        }
    }

    private void Update()
    {
        if (temp != null)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 1.0f;
            Vector3Int tilemapPos = WorldToolManager.current.tilemap.WorldToCell(worldPos);
            temp.transform.position = WorldToolManager.current.tilemap.GetCellCenterWorld(tilemapPos);

            SpriteRenderer SR = temp.GetComponent<SpriteRenderer>();

            SR.color = new Vector4 (0, 0, 1, 0.5f);

            if (Input.GetKeyDown(KeyCode.Q) && SR.flipX == false)
            {
                SR.flipX = true;
            } 
            else if (Input.GetKeyDown(KeyCode.Q) && SR.flipX == true)
            {
                SR.flipX = false;
            }

            if  (Input.GetKeyDown(KeyCode.E) && SR.flipX == false)
            {
                SR.flipX = true;
            } 
            else if (Input.GetKeyDown(KeyCode.E) && SR.flipX == true)
            {
                SR.flipX = false;
            }





        }
    }

    // Tilemap Management
    //
    // Helper methods

    public static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, v.z);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }

    public static void FillTiles(TileBase[] array, TileType type)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = tileBases[type];
        }
    }

    public static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
    }

    // Building Placement

    public void InitializeWithBuilding(GameObject building)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilemapPos = WorldToolManager.current.tilemap.WorldToCell(worldPos);
        tilemapPos.z = (int)1.0f;

        temp = Instantiate(building, tilemapPos, Quaternion.identity).GetComponent<Building>();
    }





    public enum TileType
    {
        Empty,
        White,
        Green,
        Red
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class BlueprintManager : MonoBehaviour
{
    public static BlueprintManager current;

    public GridLayout gridLayout;
    public Tilemap upperTilemap;
    public Tilemap lowerTilemap;

    public static Dictionary<TileType, Tile> tileBases = new Dictionary<TileType, Tile>();

    public Building temp;
    private Vector3 prevPos;

    public SpriteRenderer SR;
    public GameObject buildingToPlace;

    public bool cantBePlaced;

    // Unity Methods
    private void Awake()
    {
        // instatiate the singleton
        current = this;
    }

    private void Start()
    {
       tileBases.Add(TileType.Empty, null);
    }

    private void Update()
    {
        upperTilemap = WorldToolManager.current.tilemapList[WorldToolManager.current.currentLayer - 1];
        gridLayout = WorldToolManager.current.gridList[WorldToolManager.current.currentLayer - 1];
        lowerTilemap = WorldToolManager.current.tilemapList[WorldToolManager.current.currentLayer];


        if (temp != null)
        {
            Building building = buildingToPlace.GetComponent<Building>();

            building.Placed = false;

            if (building.Placed == false)
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPos.z = 1.0f;
                Vector3Int tilemapPos = WorldToolManager.current.tilemap.WorldToCell(worldPos);

                SR = temp.GetComponent<SpriteRenderer>();

                if (prevPos != tilemapPos)
                {
                    temp.transform.position = WorldToolManager.current.tilemap.GetCellCenterWorld(tilemapPos);
                    temp.transform.position += new Vector3(0.25f, 0.25f, 0);
                    prevPos = tilemapPos;

                    bool placementFailed = false;
                    for (int i = 1; i < 13; i++)
                    {
                        placementFailed = CheckBuildingArea(i);
                        if (placementFailed)
                        {
                            break;
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Q) && SR.flipX == false)
            {
                    SR.flipX = true;
            }
            else if (Input.GetKeyDown(KeyCode.Q) && SR.flipX == true)
            {
                SR.flipX = false;
            }

            if (Input.GetKeyDown(KeyCode.E) && SR.flipX == false)
            {
                    SR.flipX = true;
            }
            else if (Input.GetKeyDown(KeyCode.E) && SR.flipX == true)
            {
            SR.flipX = false;
            }
            if (Input.GetMouseButtonDown(1))
            {
               building.Placed = true;

                if (SR.flipX == true && cantBePlaced == false)
                {
                        Building finalBuilding = Instantiate(building, temp.transform.position, Quaternion.identity).GetComponent<Building>();
                        SpriteRenderer SR2 = finalBuilding.GetComponent<SpriteRenderer>();
                        SR2.flipX = true;
                        Destroy(temp.gameObject);
                }
                else if (SR.flipX == false && cantBePlaced == false)
                {
                        Instantiate(building, temp.transform.position, Quaternion.identity).GetComponent<Building>();
                        Destroy(temp.gameObject);
                }

            }

                //

        }
    }
    

    // Tilemap Management
    //
    // Helper methods

    public static Tile[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        Tile[] array = new Tile[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, v.z);
            array[counter] = (Tile)tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }

    public static void FillTiles(Tile[] array, TileType type)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = tileBases[type];
        }
    }

    public static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        Tile[] tileArray = new Tile[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
    }

    // Building Placement

    public void InitializeWithBuilding(GameObject building)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilemapPos = upperTilemap.WorldToCell(worldPos);
        tilemapPos.z = (int)1.0f;

        Debug.Log(tilemapPos);

        buildingToPlace = building;

        temp = Instantiate(building, tilemapPos, Quaternion.identity).GetComponent<Building>();

        SR = temp.GetComponent<SpriteRenderer>();

        SR.color = new Vector4(0, 0, 1, 0.5f);
    }

    // Building Placement
    //
    // Helper Methods

    public bool CheckBuildingArea(int level)
    {
        BoundsInt buildingArea = temp.BuildingSize;
        temp.BuildingSize.position = upperTilemap.WorldToCell(temp.gameObject.transform.position);
        temp.BuildingSize.position += new Vector3Int(0, 0, level);

        Debug.Log(buildingArea);

        Tile[] upperArray = GetTilesBlock(buildingArea, upperTilemap);

        foreach (Tile t in upperArray) 
        {
            Debug.Log(t);

            if (t != null)
            {
                Debug.Log("TileFound: " + t);
                SR.color = Color.red;
                break;
            }
            SR.color = new Vector4(0, 0, 1, 0.5f);
            return cantBePlaced = false;
        }

        return cantBePlaced = true;
   }


    public enum TileType
    {
        Empty,
        Filled
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class BlueprintManager : MonoBehaviour
{
    public static BlueprintManager current;

    public GridLayout GridLayout;
    public Tilemap UpperTilemap;
    public Tilemap LowerTilemap;

    public Building Temp;
    public Vector3Int TilemapPos;
    private Vector3 PrevPos;

    public SpriteRenderer SR;
    public GameObject BuildingToPlace;

    public bool CantBePlaced;
    public bool CantBePlaced2;
    public List<Tile> TilesUnderBuilding;
    public List<Vector3> TilesPosUnderBuilding;

    public Vector3 MOUSE_POSITION;
    public Vector3 TILEMAP_POSITION;

    public Building PrevBlueprint;
    public List<Building> BlueprintList;

    // Unity Methods
    private void Awake()
    {
        // instatiate the singleton
        current = this;
    }

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        if (Temp != null)
        {
            Building building = BuildingToPlace.GetComponent<Building>();

            building.Placed = false;

            if (building.Placed == false)
            {
                if (PrevPos != TilemapPos)
                {
                    CheckBuildingAreaColliders();
                }
            }
        }
    }


    private void Update()
    {
        UpperTilemap = WorldToolManager.current.tilemapList[WorldToolManager.current.currentLayer - 1];
        GridLayout = WorldToolManager.current.gridList[WorldToolManager.current.currentLayer - 1];
        LowerTilemap = WorldToolManager.current.tilemapList[WorldToolManager.current.currentLayer];

        if (Temp != null)
        {
            Building building = BuildingToPlace.GetComponent<Building>();

            if (building.Placed == false)
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPos.z = 1.0f;
                TilemapPos = WorldToolManager.current.tilemap.WorldToCell(worldPos);

                SR = Temp.GetComponent<SpriteRenderer>();
                
                if (PrevPos != TilemapPos)
                {

                    Temp.transform.position = WorldToolManager.current.tilemap.GetCellCenterWorld(TilemapPos);

                    bool placementFailed = false;

                    for (int i = 1; i < 13; i++)
                    {
                        placementFailed = CheckBuildingAreaTiles(i);

                        if (placementFailed)
                        {
                            break;
                        }
                    }

                    //temp.BuildingSize.position = new Vector3Int(temp.BuildingSize.position.x, temp.BuildingSize.position.y, 1);

                    PrevPos = TilemapPos;
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

                if (SR.flipX == true && CantBePlaced == false && CantBePlaced2 == false)
                {
                        PrevBlueprint = Instantiate(BuildingToPlace, new Vector3(Temp.transform.position.x, Temp.transform.position.y, 1), Quaternion.identity, GameObject.Find("Buildings").transform).GetComponent<Building>();
                        BlueprintList.Add(PrevBlueprint);
                        PrevBlueprint.BuildingWorldPosition.Set(PrevBlueprint.transform.position.x, PrevBlueprint.transform.position.y, PrevBlueprint.transform.position.z);
                        PolygonCollider2D PC2D2 = PrevBlueprint.GetComponent<PolygonCollider2D>();
                        PC2D2.isTrigger = true;
                        SpriteRenderer SR2 = PrevBlueprint.GetComponent<SpriteRenderer>();
                        SR2.flipX = true;
                        SR2.color = SR.color = new Vector4(0, 0, 1, 0.5f);
                        Destroy(Temp.gameObject);
                }
                else if (SR.flipX == false && CantBePlaced == false && CantBePlaced2 == false)
                {
                        PrevBlueprint = Instantiate(BuildingToPlace, new Vector3(Temp.transform.position.x, Temp.transform.position.y, 1), Quaternion.identity, GameObject.Find("Buildings").transform).GetComponent<Building>();
                        PrevBlueprint.BuildingWorldPosition.Set(PrevBlueprint.transform.position.x, PrevBlueprint.transform.position.y, PrevBlueprint.transform.position.z);
                        BlueprintList.Add(PrevBlueprint);
                        PolygonCollider2D PC2D2 = PrevBlueprint.GetComponent<PolygonCollider2D>();
                        PC2D2.isTrigger = true;
                        SpriteRenderer SR2 = PrevBlueprint.GetComponent<SpriteRenderer>();
                        SR2.color = SR.color = new Vector4(0, 0, 1, 0.5f);
                        Destroy(Temp.gameObject);

                }
            }
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

    // Building Placement

    public void InitializeWithBuilding(GameObject building)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilemapPos = UpperTilemap.WorldToCell(worldPos);
        tilemapPos.z = (int)1.0f;

        BuildingToPlace = building;

        Temp = Instantiate(building, tilemapPos, Quaternion.identity).GetComponent<Building>();

        Temp.gameObject.AddComponent<Blueprint>();

        SR = Temp.GetComponent<SpriteRenderer>();

        SR.color = new Vector4(0, 0, 1, 0.5f);
    }

    // Building Placement
    //
    // Helper Methods

    public bool CheckBuildingAreaTiles(int level)
    {
        BoundsInt buildingArea = Temp.BuildingSize;
        Temp.BuildingWorldPosition = Temp.gameObject.transform.position;
        Temp.BuildingSize.position = UpperTilemap.WorldToCell(Temp.BuildingWorldPosition);
        Temp.BuildingSize.position += new Vector3Int(-1, -1, level);

        //buildingArea.position = new Vector3Int((int)temp.transform.position.x, (int)temp.transform.position.y, 1);

        MOUSE_POSITION = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // magic number 2.125f is actually with in a range of 1.000f - 2.375f
        //  value correction 1
        MOUSE_POSITION.z = 1.0f;

        TILEMAP_POSITION = UpperTilemap.WorldToCell(MOUSE_POSITION);


        TileBase[] upperArray = UpperTilemap.GetTilesBlock(buildingArea);

        SR = Temp.GetComponent<SpriteRenderer>();


        foreach (Tile t in upperArray) 
        {
            Vector3 tilePos = buildingArea.allPositionsWithin.Current;

            if (t != null)
            {
                TilesUnderBuilding.Add(t);
                TilesPosUnderBuilding.Add(tilePos);
                SR.color = new Vector4(1, 0, 0, 0.5f);
                return CantBePlaced = true;
            }
       
        }
        TilesUnderBuilding.Clear();
        TilesPosUnderBuilding.Clear();
        SR.color = new Vector4(0, 0, 1, 0.5f);
        return CantBePlaced = false;
   }

    public bool CheckBuildingAreaColliders()
    {

        PolygonCollider2D PC2D = Temp.GetComponent<PolygonCollider2D>();
        PC2D.isTrigger = true;

        if (PC2D.IsTouchingLayers(LayerMask.GetMask("Building")))
        {
            SR.color = new Vector4(1, 0, 0, 0.5f);
            return CantBePlaced2 =  true;
        }
        else
        {
            return CantBePlaced2 = false;
        }
   }

}

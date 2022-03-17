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

    public Building temp;
    private Vector3 prevPos;

    public SpriteRenderer SR;
    public GameObject buildingToPlace;

    public bool cantBePlaced;
    public List<Tile> tilesUnderBuilding;

    public Vector3 MOUSE_POSITION;
    public Vector3 TILEMAP_POSITION;

    // Unity Methods
    private void Awake()
    {
        // instatiate the singleton
        current = this;
    }

    private void Start()
    {
        Physics2D.autoSyncTransforms = true;
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

                    bool placementFailed = false;

                    for (int i = 1; i < 13; i++)
                    {
                        placementFailed = CheckBuildingAreaTiles(i);

                        if (placementFailed)
                        {
                            break;
                        }
                    }
                   
                    bool placementFailed2 = false;

                    placementFailed2 = CheckBuildingAreaColliders();

                    if (placementFailed2)
                    {
                        SR.color = new Vector4(1, 0, 0, 0.5f);
                    } 
                    else if (!placementFailed2)
                    {
                        SR.color = new Vector4(0, 0, 1, 0.5f);
                    }

                    prevPos = tilemapPos;
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
                        Building Blueprint = Instantiate(building, new Vector3(temp.transform.position.x, temp.transform.position.y, 12), Quaternion.identity, GameObject.Find("Buildings").transform).GetComponent<Building>();
                        Blueprint.BuildingWorldPosition.Set(Blueprint.transform.position.x, Blueprint.transform.position.y, Blueprint.transform.position.z);
                        SpriteRenderer SR2 = Blueprint.GetComponent<SpriteRenderer>();
                        SR2.flipX = true;
                        SR2.color = SR.color = new Vector4(0, 0, 1, 0.5f);
                        Destroy(temp.gameObject);
                }
                else if (SR.flipX == false && cantBePlaced == false)
                {
                        Building Blueprint = Instantiate(building, new Vector3(temp.transform.position.x, temp.transform.position.y, 12), Quaternion.identity, GameObject.Find("Buildings").transform).GetComponent<Building>();
                        Blueprint.BuildingWorldPosition.Set(Blueprint.transform.position.x, Blueprint.transform.position.y, Blueprint.transform.position.z);
                        SpriteRenderer SR2 = Blueprint.GetComponent<SpriteRenderer>();
                        SR2.color = SR.color = new Vector4(0, 0, 1, 0.5f);
                        Destroy(temp.gameObject);

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
        Vector3Int tilemapPos = upperTilemap.WorldToCell(worldPos);
        tilemapPos.z = (int)1.0f;

        buildingToPlace = building;

        temp = Instantiate(building, tilemapPos, Quaternion.identity).GetComponent<Building>();

        SR = temp.GetComponent<SpriteRenderer>();

        SR.color = new Vector4(0, 0, 1, 0.5f);
    }

    // Building Placement
    //
    // Helper Methods

    public bool CheckBuildingAreaTiles(int level)
    {
        BoundsInt buildingArea = temp.BuildingSize;
        temp.BuildingWorldPosition = temp.gameObject.transform.position;
        temp.BuildingSize.position = upperTilemap.WorldToCell(temp.BuildingWorldPosition);
        temp.BuildingSize.position += new Vector3Int(-1, -1, level);

        //buildingArea.position = new Vector3Int((int)temp.transform.position.x, (int)temp.transform.position.y, 1);

        MOUSE_POSITION = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // magic number 2.125f is actually with in a range of 1.000f - 2.375f
        //  value correction 1
        MOUSE_POSITION.z = 1.0f;

        // find the tilemapPos and set the selectinBox.position
        TILEMAP_POSITION = upperTilemap.WorldToCell(MOUSE_POSITION);


        TileBase[] upperArray = upperTilemap.GetTilesBlock(buildingArea);


        foreach (Tile t in upperArray) 
        {
            if (t != null)
            {
                tilesUnderBuilding.Add(t);
                SR.color = new Vector4(1, 0, 0, 0.5f);
                return cantBePlaced = true;
            }
       
        }
        tilesUnderBuilding.Clear();
        SR.color = new Vector4(0, 0, 1, 0.5f);
        return cantBePlaced = false;
   }

    public bool CheckBuildingAreaColliders()
    {

        PolygonCollider2D PC2D = temp.GetComponent<PolygonCollider2D>();

        if (!PC2D.IsTouchingLayers(LayerMask.GetMask("Building")))
        {
            return cantBePlaced = false;
        }
        Debug.Log("touching");
        return cantBePlaced = true;
    }

}

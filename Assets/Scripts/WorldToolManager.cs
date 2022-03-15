using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class WorldToolManager : MonoBehaviour
{

    public List<Vector3Int> designatedTilePosList;

    // TileDesignationFlattening()
    public List<Vector3Int> flattenTilePosList;

    // SetTileDesignation
    public bool isTileDesignatorFlattenActive;
    public Toggle flattenButton;



    // TileDesignationMining()
    public Tilemap tilemap;
    public Transform selectionBoxPos;
    public Tile selectedTile;
    public Tile currentTile;
    public Tile noTile;
    public List<Vector3Int> miningTilePosList;
    public Vector3Int designatedTilePos;

    // SetTileDesignation()
    public bool isTileDesignatorMiningActive;
    public GameObject selectionBoxGO;
    public Toggle miningButton;

    // LayerSelection()
    public List<Tilemap> tilemapList;
    public int currentLayer;
    // TO DO
    //
    //

    // Start is called before the first frame update
    void Start()
    {
       tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
       selectionBoxGO = GameObject.FindGameObjectWithTag("SelectionBox");
       BuildLayerList();
       flattenButton = GameObject.FindGameObjectWithTag("Flatten Button").GetComponent<Toggle>();
       miningButton = GameObject.FindGameObjectWithTag("Mining Button").GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        // Flattening
        if (isTileDesignatorFlattenActive == true)
        {
            // if its true set to active
            // and designate tiles
            selectionBoxGO.SetActive(true);
            TileDesignationFlattening();
        }

        if (isTileDesignatorFlattenActive == false)
        {
            // if it false hide
            // the selection box
            selectionBoxGO.SetActive(false);
        }

        // Mining
        if (isTileDesignatorMiningActive == true)
        {
            // if its true set to active
            // and designate tiles
            selectionBoxGO.SetActive(true);
            TileDesignationMining();
        }

        if (isTileDesignatorMiningActive == false && isTileDesignatorFlattenActive == false)
        {
            // if it false hide
            // the selection box
            selectionBoxGO.SetActive(false);
        }

    }


    public void TileDesignationFlattening()
    {
        if (currentLayer == 1)
        {
            // get the mouse position and set it equal to worldPos
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // magic number 2.125f is actually with in a range of 1.000f - 2.250f
            //  value correction 1
            worldPos.z = 1.0f;

            // find the tilemapPos and set the selectinBox.position
            Vector3Int tilemapPos = tilemap.WorldToCell(worldPos);
            //selectionBoxPos.position = tilemapPos;


            // loop thru the z offsets to find the tile
            while (worldPos.z <= 2.375f)
            {
                // find the tilemapPos
                tilemapPos = tilemap.WorldToCell(worldPos);

                // print variables
                //Debug.Log("wP: " + worldPos);
                //Debug.Log("tmP: " + tilemapPos);

                // if theres a tile, do X
                if (selectedTile = (Tile)tilemap.GetTile(tilemapPos))
                {
                    designatedTilePos = tilemapPos;

                    while (Input.GetMouseButton(1))
                    {

                        if (!flattenTilePosList.Contains(tilemapPos) && !designatedTilePosList.Contains(tilemapPos))
                        {
                            tilemap.SetTileFlags(tilemapPos, TileFlags.None);
                            tilemap.SetColor(tilemapPos, Color.blue);
                            flattenTilePosList.Add(tilemapPos);
                            designatedTilePosList.Add(tilemapPos);
                        }
                        break;
                    }

                    while (Input.GetMouseButton(0))
                    {
                        if (flattenTilePosList.Contains(tilemapPos) && designatedTilePosList.Contains(tilemapPos) && !miningTilePosList.Contains(tilemapPos))

                        {
                            tilemap.SetTileFlags(tilemapPos, TileFlags.None);
                            tilemap.SetColor(tilemapPos, Color.white);
                            flattenTilePosList.Remove(tilemapPos);
                            designatedTilePosList.Remove(tilemapPos);
                            miningTilePosList.Remove(tilemapPos);
                        }
                        break;
                    }
                    break;
                }

                // incremement for
                // each offset level
                worldPos.z += .125f;
            }

        } else if (currentLayer == 2)
        {
            // parse layer

        }
        else if (currentLayer == 3)
        {
            // parse layer

        }
        else if (currentLayer == 4)
        {
            // parse layer

        }
        else if (currentLayer == 5)
        {
            // parse layer

        }
        else if (currentLayer == 6)
        {
            // parse layer

        }
    }

    public void SetTileDesignationFlattening()
    {
        if (isTileDesignatorFlattenActive == false && isTileDesignatorMiningActive == false)
        {
            isTileDesignatorFlattenActive = true;
            miningButton.interactable = false;

        }
        else if (isTileDesignatorFlattenActive == true)
        {
            isTileDesignatorFlattenActive = false;
            miningButton.interactable = true;
        }

    }

    public void TileDesignationMining()
    {
        if (currentLayer == 1)
        {
            // get the mouse position and set it equal to worldPos
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // magic number 2.125f is actually with in a range of 1.000f - 2.250f
            //  value correction 1
            worldPos.z = 1.0f;

            // find the tilemapPos and set the selectinBox.position
            Vector3Int tilemapPos = tilemap.WorldToCell(worldPos);
            //selectionBoxPos.position = tilemapPos;


            // loop thru the z offsets to find the tile
            while (worldPos.z <= 2.375f)
            {
                // find the tilemapPos
                tilemapPos = tilemap.WorldToCell(worldPos);

                // print variables
                //Debug.Log("wP: " + worldPos);
                //Debug.Log("tmP: " + tilemapPos);

                // if theres a tile, do X
                if (selectedTile = (Tile)tilemap.GetTile(tilemapPos))
                {
                    designatedTilePos = tilemapPos;

                    while (Input.GetMouseButton(1))
                    {
                        if (!miningTilePosList.Contains(tilemapPos) && !designatedTilePosList.Contains(tilemapPos))
                        {
                            tilemap.SetTileFlags(tilemapPos, TileFlags.None);
                            tilemap.SetColor(tilemapPos, Color.red);
                            miningTilePosList.Add(tilemapPos);
                            designatedTilePosList.Add(tilemapPos);
                        }
                        break;
                    }

                    while (Input.GetMouseButton(0))
                    {
                        if (miningTilePosList.Contains(tilemapPos) && designatedTilePosList.Contains(tilemapPos) && !flattenTilePosList.Contains(tilemapPos))
                        {
                            tilemap.SetTileFlags(tilemapPos, TileFlags.None);
                            tilemap.SetColor(tilemapPos, Color.white);
                            miningTilePosList.Remove(tilemapPos);
                            designatedTilePosList.Remove(tilemapPos);
                        }
                        break;
                    }
                    break;
                }

                // incremement for
                // each offset level
                worldPos.z += .125f;
            }
        } else if (currentLayer == 2)
        {
            // parse layer

        } else if (currentLayer == 3)
        {
            // parse layer

        }
        else if (currentLayer == 4)
        {
            // parse layer

        }
        else if (currentLayer == 5)
        {
            // parse layer

        }
        else if (currentLayer == 6)
        {
            // parse layer

        }
    }

    public void SetTileDesignationMining()
    {
        if (isTileDesignatorMiningActive == false && isTileDesignatorFlattenActive == false)
        {
            isTileDesignatorMiningActive = true;
            flattenButton.interactable = false;

        } else if (isTileDesignatorMiningActive == true)
        {
            isTileDesignatorMiningActive = false;
            flattenButton.interactable = true;
        }
    }
    public void LayerSelection()
    {
        if (Input.GetKey(KeyCode.Plus))
        {
            tilemap.color = Color.grey;
            tilemap = tilemapList[currentLayer + 1];
            Debug.Log(currentLayer);
            tilemap.color = Color.white;
        }

        if (Input.GetKey(KeyCode.Minus))
        {
            tilemap.color = Color.grey;
            tilemap = tilemapList[currentLayer - 1];
            Debug.Log(currentLayer);
            tilemap.color = Color.white;
        }
    }

    public void BuildLayerList() 
    {
        currentLayer = 1;

        tilemapList = new List<Tilemap>();

        tilemapList.Add(GameObject.Find("Tilemap 1").GetComponent<Tilemap>());
        tilemapList.Add(GameObject.Find("Tilemap").GetComponent<Tilemap>());
        tilemapList.Add(GameObject.Find("Tilemap (1)").GetComponent<Tilemap>());
        tilemapList.Add(GameObject.Find("Tilemap (2)").GetComponent<Tilemap>());
        tilemapList.Add(GameObject.Find("Tilemap (3)").GetComponent<Tilemap>());
        tilemapList.Add(GameObject.Find("Tilemap (4)").GetComponent<Tilemap>());
        tilemapList.Add(GameObject.Find("Tilemap (5)").GetComponent<Tilemap>());
    }

}

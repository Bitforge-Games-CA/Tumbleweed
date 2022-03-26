using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace Tumbleweed.Core.Managers
{

    // TODO
    // -Fix casing

    public class WorldToolManager : MonoBehaviour
    {
        public static WorldToolManager current;

        public List<Vector3Int> designatedTilePosList;

        // TileDesignationFlattening()
        public List<Vector3Int> flattenTilePosList;

        // SetTileFlattenDesignation
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

        // SetTileMiningDesignation()
        public bool isTileDesignatorMiningActive;
        public GameObject selectionBoxGO;
        public Toggle miningButton;

        // TileDesignationHarvesting()
        public bool isTileDesignatorHarvestingActive;
        public Toggle harvestButton;

        // SetTileDesignationHarvesting()
        public List<Vector3Int> harvestTilePosList;

        // LayerSelection()
        public List<Tilemap> tilemapList;
        public int currentLayer;
        public List<Grid> gridList;
        public Grid currentGrid;

        public Vector3Int AddedTilePos;
        public Vector3Int RemovedTilePos;

        // SetTileDesignationBuilding()
        public bool isTileDesignatorBuildingActive;
        public Toggle buildButton;

        // SetTileDesignationCancelBuilding()
        public bool isTileDesignatorCancelBuildingActive;
        public Toggle cancelBuildButton;

        // Start is called before the first frame update
        void Start()
        {
            // instatiate the singleton
            if (current == null)
            {
                current = this;
            }
            else if (current != this)
            {
                Destroy(current);
            }
            // find everything and build the lists too
            tilemap = GameObject.Find("Tilemap 1").GetComponent<Tilemap>();
            selectionBoxGO = GameObject.FindGameObjectWithTag("SelectionBox");
            BuildLayerList();
            BuildGridList();
            flattenButton = GameObject.FindGameObjectWithTag("Flatten Button").GetComponent<Toggle>();
            miningButton = GameObject.FindGameObjectWithTag("Mining Button").GetComponent<Toggle>();
            harvestButton = GameObject.FindGameObjectWithTag("Harvest Button").GetComponent<Toggle>();
            buildButton = GameObject.FindGameObjectWithTag("Build Button").GetComponent<Toggle>();
            cancelBuildButton = GameObject.FindGameObjectWithTag("Cancel Build Button").GetComponent<Toggle>();
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

            // Mining
            if (isTileDesignatorMiningActive == true)
            {
                // if its true set to active
                // and designate tiles
                selectionBoxGO.SetActive(true);
                TileDesignationMining();
            }

            // Harvesting
            if (currentLayer != 1)
            {
                harvestButton.interactable = false;

            }
            else if (currentLayer == 1 && isTileDesignatorFlattenActive == false && isTileDesignatorMiningActive == false && isTileDesignatorBuildingActive == false && isTileDesignatorCancelBuildingActive == false)
            {
                harvestButton.interactable = true;
            }

            if (isTileDesignatorHarvestingActive == true)
            {
                TileDesignationHarvesting();
            }

            // reset tools
            if (isTileDesignatorMiningActive == false && isTileDesignatorFlattenActive == false && isTileDesignatorHarvestingActive == false)
            {
                // if it false hide
                // the selection box
                selectionBoxGO.SetActive(false);
                tilemap = tilemapList[currentLayer];
            }

            // run layer selection
            LayerSelection();

        }


        public void TileDesignationFlattening()
        {

            // get the mouse position and set it equal to worldPos
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // magic number 2.125f is actually with in a range of 1.000f - 2.375f
            //  value correction 1
            worldPos.z = 1.0f;

            // find the tilemapPos and set the selectinBox.position
            Vector3Int tilemapPos = tilemap.WorldToCell(worldPos);
            //selectionBoxPos.position = tilemapPos;


            // loop thru the z offsets to find the tile
            while (worldPos.z <= 2.5f)
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

                            AddedTilePos = tilemapPos;
                            JobManager.current.ListUpdatedAdd = false;
                            JobManager.current.IsFlatten = true;
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

                            RemovedTilePos = tilemapPos;
                            JobManager.current.ListUpdatedRemove = false;
                        }
                        break;
                    }
                    break;
                }

                // incremement for
                // each offset level
                worldPos.z += .125f;
            }
        }

        public void SetTileDesignationFlattening()
        {
            if (isTileDesignatorFlattenActive == false && isTileDesignatorMiningActive == false && isTileDesignatorHarvestingActive == false)
            {
                isTileDesignatorFlattenActive = true;
                miningButton.interactable = false;
                harvestButton.interactable = false;
                buildButton.interactable = false;
                cancelBuildButton.interactable = false;

            }
            else if (isTileDesignatorFlattenActive == true)
            {
                isTileDesignatorFlattenActive = false;
                miningButton.interactable = true;
                harvestButton.interactable = true;
                buildButton.interactable = true;
                cancelBuildButton.interactable = true;
            }

        }

        public void TileDesignationMining()
        {

            // get the mouse position and set it equal to worldPos
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // magic number 2.125f is actually with in a range of 1.000f - 2.3275f
            //  value correction 1
            worldPos.z = 1.0f;

            // find the tilemapPos and set the selectinBox.position
            Vector3Int tilemapPos = tilemap.WorldToCell(worldPos);
            //selectionBoxPos.position = tilemapPos;


            // loop thru the z offsets to find the tile
            while (worldPos.z <= 2.5f)
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

                            AddedTilePos = tilemapPos;
                            JobManager.current.ListUpdatedAdd = false;
                            JobManager.current.IsMining = true;
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

                            RemovedTilePos = tilemapPos;
                            JobManager.current.ListUpdatedRemove = false;
                        }
                        break;
                    }
                    break;
                }

                // incremement for
                // each offset level
                worldPos.z += .125f;
            }
        }

        public void SetTileDesignationMining()
        {
            if (isTileDesignatorMiningActive == false && isTileDesignatorFlattenActive == false && isTileDesignatorHarvestingActive == false)
            {
                isTileDesignatorMiningActive = true;
                flattenButton.interactable = false;
                harvestButton.interactable = false;
                buildButton.interactable = false;
                cancelBuildButton.interactable = false;

            }
            else if (isTileDesignatorMiningActive == true)
            {
                isTileDesignatorMiningActive = false;
                flattenButton.interactable = true;
                harvestButton.interactable = true;
                buildButton.interactable = true;
                cancelBuildButton.interactable = true;
            }
        }

        public void TileDesignationHarvesting()
        {
            tilemap = tilemapList[0];
            // get the mouse position and set it equal to worldPos
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // magic number 2.125f is actually with in a range of 1.000f - 2.250f
            //  value correction 1
            worldPos.z = 1.0f;

            // find the tilemapPos and set the selectinBox.position
            Vector3Int tilemapPos = tilemap.WorldToCell(worldPos);
            selectionBoxPos.position = tilemap.GetCellCenterWorld(tilemapPos);

            // loop thru the z offsets to find the tile
            while (worldPos.z < 5.00f)
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
                        if (!designatedTilePosList.Contains(tilemapPos) && !harvestTilePosList.Contains(tilemapPos))
                        {
                            tilemap.SetTileFlags(tilemapPos, TileFlags.None);
                            tilemap.SetColor(tilemapPos, new Vector4(0.75f, 0, 0, 1));
                            designatedTilePosList.Add(tilemapPos);
                            harvestTilePosList.Add(tilemapPos);

                            AddedTilePos = tilemapPos;
                            JobManager.current.ListUpdatedAdd = false;
                            JobManager.current.IsHarvesting = true;
                        }
                        break;
                    }

                    while (Input.GetMouseButton(0))
                    {
                        if (designatedTilePosList.Contains(tilemapPos) && harvestTilePosList.Contains(tilemapPos))
                        {
                            tilemap.SetTileFlags(tilemapPos, TileFlags.None);
                            tilemap.SetColor(tilemapPos, Color.white);
                            designatedTilePosList.Remove(tilemapPos);
                            harvestTilePosList.Remove(tilemapPos);

                            RemovedTilePos = tilemapPos;
                            JobManager.current.ListUpdatedRemove = false;
                        }
                        break;
                    }
                    break;
                }

                // incremement for
                // each offset level
                worldPos.z += .125f;
            }
        }

        public void SetTileDesignationHarvesting()
        {
            if (isTileDesignatorHarvestingActive == false && isTileDesignatorMiningActive == false && isTileDesignatorFlattenActive == false)
            {
                isTileDesignatorHarvestingActive = true;
                flattenButton.interactable = false;
                miningButton.interactable = false;
                buildButton.interactable = false;
                cancelBuildButton.interactable = false;

            }
            else if (isTileDesignatorHarvestingActive == true)
            {
                isTileDesignatorHarvestingActive = false;
                flattenButton.interactable = true;
                miningButton.interactable = true;
                buildButton.interactable = true;
                cancelBuildButton.interactable = true;
            }
        }

        public void SetTileDesignationBuilding()
        {
            if (isTileDesignatorHarvestingActive == false && isTileDesignatorMiningActive == false && isTileDesignatorFlattenActive == false && isTileDesignatorBuildingActive == false && isTileDesignatorCancelBuildingActive == false)
            {
                isTileDesignatorBuildingActive = true;
                flattenButton.interactable = false;
                miningButton.interactable = false;
                harvestButton.interactable = false;
                cancelBuildButton.interactable = false;

            }
            else if (isTileDesignatorBuildingActive == true)
            {
                isTileDesignatorBuildingActive = false;
                flattenButton.interactable = true;
                miningButton.interactable = true;
                cancelBuildButton.interactable = true;
                harvestButton.interactable = true;
            }
        }

        public void SetTileDesignationCancelBuilding()
        {
            if (isTileDesignatorHarvestingActive == false && isTileDesignatorMiningActive == false && isTileDesignatorFlattenActive == false && isTileDesignatorBuildingActive == false && isTileDesignatorCancelBuildingActive == false)
            {
                isTileDesignatorCancelBuildingActive = true;
                flattenButton.interactable = false;
                miningButton.interactable = false;
                buildButton.interactable = false;
                harvestButton.interactable = false;

            }
            else if (isTileDesignatorCancelBuildingActive == true)
            {
                isTileDesignatorCancelBuildingActive = false;
                flattenButton.interactable = true;
                miningButton.interactable = true;
                harvestButton.interactable = true;
                buildButton.interactable = true;
            }
        }

        public void LayerSelection()
        {
            if (Input.GetKeyDown(KeyCode.KeypadPlus) && currentLayer >= 0)
            {
                tilemap.color = Color.gray;
                currentLayer = currentLayer + 1;
                tilemap = tilemapList[currentLayer];
                currentGrid = gridList[currentLayer];
                tilemap.color = Color.white;
                tilemapList[0].color = Color.white;

                if (currentLayer == 1)
                {
                    tilemapList[0].color = Color.white;
                }
            }

            if (Input.GetKeyDown(KeyCode.KeypadMinus) && currentLayer >= 1)
            {
                tilemap.color = Color.gray;
                currentLayer = currentLayer - 1;
                tilemap = tilemapList[currentLayer];
                currentGrid = gridList[currentLayer];
                tilemap.color = Color.white;

                if (currentLayer == 1)
                {
                    tilemapList[0].color = Color.white;
                }
            }
        }

        public void BuildLayerList()
        {
            currentLayer = 1;

            tilemapList = new List<Tilemap>();

            tilemapList.Add(GameObject.Find("Tilemap 0").GetComponent<Tilemap>());
            tilemapList.Add(GameObject.Find("Tilemap 1").GetComponent<Tilemap>());
            tilemapList.Add(GameObject.Find("Tilemap 2").GetComponent<Tilemap>());
            tilemapList.Add(GameObject.Find("Tilemap 3").GetComponent<Tilemap>());
            tilemapList.Add(GameObject.Find("Tilemap 4").GetComponent<Tilemap>());
            tilemapList.Add(GameObject.Find("Tilemap 5").GetComponent<Tilemap>());
            tilemapList.Add(GameObject.Find("Tilemap 6").GetComponent<Tilemap>());
        }

        public void BuildGridList()
        {

            gridList = new List<Grid>();

            gridList.Add(GameObject.Find("Grid 0").GetComponent<Grid>());
            gridList.Add(GameObject.Find("Grid 1").GetComponent<Grid>());
            gridList.Add(GameObject.Find("Grid 2").GetComponent<Grid>());
            gridList.Add(GameObject.Find("Grid 3").GetComponent<Grid>());
            gridList.Add(GameObject.Find("Grid 4").GetComponent<Grid>());
            gridList.Add(GameObject.Find("Grid 5").GetComponent<Grid>());
            gridList.Add(GameObject.Find("Grid 6").GetComponent<Grid>());
        }

    }

}
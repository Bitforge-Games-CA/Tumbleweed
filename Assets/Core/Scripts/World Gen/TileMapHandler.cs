using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using Tumbleweed.Core.UtilityAI;
using Tumbleweed.Core.UI;
using Tumbleweed.Core.Managers;
using Tumbleweed.Core.Utilities;

namespace Tumbleweed.Core.WorldGen
{

    public class TileMapHandler : MonoBehaviour
    {
        // Map Object
        public Tilemap tilemap1;
        public Grid grid1;
        // List with tiles
        public List<Tile> tileList = new List<Tile>();

        // Input data for our noise generator
        [SerializeField] public int width;
        [SerializeField] public int height;
        [SerializeField] public float scale;

        [SerializeField] public int octaves;
        [SerializeField] public float persistence;
        [SerializeField] public float lacunarity;

        private int seed;

        [SerializeField] public Vector2 offset;

        // Start is called before the first frame update
        void Start()
        {

            seed = Convert.ToInt32(NewGameMenu.Seed);

            // Generate a height map
            float[] noiseMap = NoiseMapGenerator.GenerateNoiseMap(width, height, seed, scale, octaves, persistence, lacunarity, offset);


            // Create Tiles
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < height; x++)
                {
                    // Noise level for the current tile
                    float noiseHeight = noiseMap[width * y + x];

                    // The levels of the generated surface are evenly distributed on the noise scale
                    // "Stretch" the noise scale to the size of the array of tiles
                    float colorHeight = noiseHeight * tileList.Count;
                    // Select a tile below the resulting value
                    int colorIndex = Mathf.FloorToInt(colorHeight);
                    // Consider addressing in arrays for maximum noise values
                    if (colorIndex == tileList.Count)
                    {
                        colorIndex = tileList.Count - 1;
                    }

                    // Tile assets allow you to use a height of 2z
                    // Therefore, we “stretch” the noise scale more than 2 times with color
                    float tileHeight = noiseHeight * tileList.Count * 2;
                    int tileHeightIndex = Mathf.FloorToInt(tileHeight);

                    // Take an asset tile depending on the converted noise level
                    Tile tile = tileList[colorIndex];

                    // Set the tile height depending on the converted noise level
                    Vector3Int p = new Vector3Int(x - width / 2, y - height / 2, tileHeightIndex);
                    tilemap1.SetTile(p, tile);

                    int i = WorldToolManager.current.tilemapList.IndexOf(tilemap1);

                    //Debug.Log(tilemap1 + ": " + p);
                    if (tilemap1 == WorldToolManager.current.tilemapList[i])
                    {

                        // A
                        Vector2 translatedCoords = Tools.current.TranslateToIso(p.x, p.y);

                        // B
                        //Vector3 tilePos = tilemap1.GetCellCenterWorld(p);
                        //Vector2 translatedCoords = TranslateToIso2(tilePos.x, tilePos.y);

                        // C
                        //Vector2Int translatedCoords = GridToWorld(p.x, p.y);


                        PathNode pathNodeNew = new PathNode(tilemap1, translatedCoords);
                        PathNodeManager pathNodeManager = GetComponent<PathNodeManager>();
                        pathNodeManager.pathNodesDict.Add(translatedCoords, pathNodeNew);
                        pathNodeNew.Tile = tile;
                        if (pathNodeNew.Tile.name != "Empty_Sprite")
                        {
                            pathNodeNew.IsNodeBlocked = true;

                            if (tilemap1.name == "Tilemap 0")
                            {
                                pathNodeNew.IsNodeBlocked = true;
                                Transform parent = GameObject.Find("Debug Nodes " + i).transform;
                                Transform transform = Tools.CreatePrimitiveRed(new Vector3(translatedCoords.x, translatedCoords.y, i));
                                transform.SetParent(parent);
                            }

                        }
                        else
                        {
                            if (tilemap1.name == "Tilemap 0")
                            {
                                Transform parent = GameObject.Find("Debug Nodes " + i).transform;
                                Transform transform = Tools.CreatePrimitiveGreen(new Vector3(translatedCoords.x, translatedCoords.y, i));
                                transform.SetParent(parent);
                            }

                        }

                    }

                }

            }

        }

    }

}
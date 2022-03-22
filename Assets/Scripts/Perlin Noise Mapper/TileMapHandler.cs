using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

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
        // Hide the object with the test texture
        NoiseMapRenderer mapRenderer = FindObjectOfType<NoiseMapRenderer>();
        mapRenderer.gameObject.SetActive(false);

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
                PathNode pathNodeNew = new PathNode(grid1, p);
                pathNodeNew.GridLocation = p;
            }
        }

    }

}

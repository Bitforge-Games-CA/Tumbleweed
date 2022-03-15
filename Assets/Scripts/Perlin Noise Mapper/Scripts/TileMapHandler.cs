using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class TileMapHandler : MonoBehaviour
{
    // Map Object
    public Tilemap tilemap1;
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

        seed = Convert.ToInt32(NewGameMenu.seed);

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
            }
        }
    }

    // Function for generating a test texture with the parameters specified for the noise generator
    // Used from the editor extension NoiseMapEditorGenerate
    public void GenerateMap()
    {
        // Generate a heightmap
        float[] noiseMap = NoiseMapGenerator.GenerateNoiseMap(width, height, seed, scale, octaves, persistence, lacunarity, offset);

        // Depending on the filling of the array with tile assets, we generate a uniformly distributed color dependence on the noise height
        List<NoiseMapRenderer.TerrainLevel> tl = new List<NoiseMapRenderer.TerrainLevel>();
        // The upper border of the range determines the color, so divide the scale into equal segments and shift it up
        float heightOffset = 1.0f / tileList.Count;
        for (int i = 0; i < tileList.Count; i++)
        {
            // Take the color from the texture of the asset tile
            Color color = tileList[i].sprite.texture.GetPixel(tileList[i].sprite.texture.width / 2, tileList[i].sprite.texture.height / 2);
            // Create a new color-noise level
            NoiseMapRenderer.TerrainLevel lev = new NoiseMapRenderer.TerrainLevel();
            lev.color = color;
            // Convert the index to a position on the scale with the range [0,1] and move up
            lev.height = Mathf.InverseLerp(0, tileList.Count, i) + heightOffset;
            // Save a new color-noise level
            tl.Add(lev);
        }

        // Apply a new color-noise scale and generate a texture based on it from the specified parameters
        NoiseMapRenderer mapRenderer = FindObjectOfType<NoiseMapRenderer>();
        mapRenderer.terrainLevel = tl;
        mapRenderer.RenderMap(width, height, noiseMap);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkHandler : MonoBehaviour
{
    public int ChunksNum = 5;
    public Dictionary<int, int> ChunkMap;


    public void CreateChunksFromTilemap(Tilemap tilemap)
    {
        BoundsInt cellBounds = tilemap.cellBounds;

        Vector3Int chunkSize = cellBounds.size / ChunksNum;

        Debug.Log(chunkSize);

    }




}

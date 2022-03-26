using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tumbleweed.Core.UtilityAI
{
    public class PathNode : ScriptableObject
    {
        private Tilemap PathfindingGrid;

        public int X;
        public int Y;
        public int Z;

        public bool IsNodeBlocked;

        public int GCost;
        public int HCost;
        public int FCost { get { return GCost + HCost; } }

        public PathNode PreviousNode;

        public Vector3Int GridLocation;
        public Tile Tile;

        public PathNode(Tilemap tilemap, Vector3Int xyz)
        {
            PathfindingGrid = tilemap;
            X = xyz.x;
            Y = xyz.y;
            Z = xyz.z;
            GridLocation = xyz;
        }

        public PathNode(Tilemap tilemap, Vector3Int xyz, Tile tile)
        {
            PathfindingGrid = tilemap;
            X = xyz.x;
            Y = xyz.y;
            Z = xyz.z;
            GridLocation = xyz;
            Tile = tile;
        }

        public override string ToString()
        {
            return X + "," + Y + "," + Z;
        }
    }
}

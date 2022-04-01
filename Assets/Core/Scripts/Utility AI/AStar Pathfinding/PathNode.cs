using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tumbleweed.Core.UtilityAI
{
    public class PathNode
    {
        private Tilemap PathfindingGrid;

        public float X;
        public float Y;

        public bool IsNodeBlocked;

        public int GCost; // distance from starting node
        public int HCost; // distance from end node
        public int FCost { get { return GCost + HCost; } } // lowest f cost determines path

        public PathNode PreviousNode;

        public Vector2 GridLocation;
        public Tile Tile;
        public Color color;

        public PathNode(Tilemap tilemap, Vector2 xy)
        {
            PathfindingGrid = tilemap;
            X = xy.x;
            Y = xy.y;
            GridLocation = xy;
        }

        public PathNode(Tilemap tilemap, Vector2 xy, Tile tile)
        {
            PathfindingGrid = tilemap;
            X = xy.x;
            Y = xy.y;
            Tile = tile;
            GridLocation = xy;
        }

        public override string ToString()
        {
            return X + "," + Y;
        }
    }
}

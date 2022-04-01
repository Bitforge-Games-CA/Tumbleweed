using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Tumbleweed.Core.Managers;
using Tumbleweed.Core.WorldGen;
using Tumbleweed.Core.XML.Data;

namespace Tumbleweed.Core.UtilityAI
{

    public class Pathfinder
    {

        List<PathNode> openList;
        List<PathNode> closedList;

        public List<PathNode> FindPath(PathNode startNode, PathNode endNode, CharacterWorldData npcData)
        {
            startNode.GCost = 0;
            startNode.HCost = GetDiagonalDistance(startNode, endNode);

            openList = new List<PathNode> { startNode };
            closedList = new List<PathNode>();

            while (openList.Count > 0)
            {
                PathNode currentPathNode = openList.OrderBy(x => x.FCost).First();

                openList.Remove(currentPathNode);
                closedList.Add(currentPathNode);

                if (currentPathNode == endNode)
                {
                    // finalize our path
                    return GetFinishedList(startNode, endNode);
                }

                foreach (var neighbour in GetNeighbourTiles(currentPathNode, npcData).Where(x => !x.IsNodeBlocked && !closedList.Contains(x) && x != null))
                {
                    neighbour.GCost = GetDiagonalDistance(startNode, neighbour);
                    neighbour.HCost = GetDiagonalDistance(neighbour, endNode);

                    neighbour.PreviousNode = currentPathNode;

                    if (!openList.Contains(neighbour))
                    {
                        openList.Add(neighbour);
                    }
                }
            }

            Debug.LogError("Couldn't Find Path");
            return new List<PathNode>();
        }

        private List<PathNode> GetNeighbourTiles(PathNode currentPathNode, CharacterWorldData npcData)
        {
            var map = WorldToolManager.current.tilemapList[npcData.CurrentLayer];

            List<PathNode> neighbours = new List<PathNode>();

            PathNodeManager pathNodeManager = GameObject.Find("Generator " + npcData.CurrentLayer).GetComponent<PathNodeManager>();

            // Top tile
            Vector2 locationToCheck = new Vector2(currentPathNode.X, currentPathNode.Y + 0.577f);

            if (map.cellBounds.Contains(new Vector3Int((int)locationToCheck.x, (int)locationToCheck.y, 0)))
            {
                //neighbours.Add(pathNodeManager.pathNodes.Find(x => x.GridLocation == locationToCheck));
                neighbours.Add(pathNodeManager.pathNodesDict.First(x => x.Key == locationToCheck).Value);

            }

            // Top right tile
            locationToCheck = new Vector2(currentPathNode.X + 0.5f, currentPathNode.Y + 0.2885f);

            if (map.cellBounds.Contains(new Vector3Int((int)locationToCheck.x, (int)locationToCheck.y, 0)))
            {
                neighbours.Add(pathNodeManager.pathNodesDict.First(x => x.Key == locationToCheck).Value);
            }

            // right tile
            locationToCheck = new Vector2(currentPathNode.X + 1, currentPathNode.Y);

            if (map.cellBounds.Contains(new Vector3Int((int)locationToCheck.x, (int)locationToCheck.y, 0)))
            {

                neighbours.Add(pathNodeManager.pathNodesDict.First(x => x.Key == locationToCheck).Value);
            }

            // Bottom right tile
            locationToCheck = new Vector2(currentPathNode.X + 0.5f, currentPathNode.Y - 0.2885f);

            if (map.cellBounds.Contains(new Vector3Int((int)locationToCheck.x, (int)locationToCheck.y, 0)))
            {

                neighbours.Add(pathNodeManager.pathNodesDict.First(x => x.Key == locationToCheck).Value);
            }

            // Bottom tile
            locationToCheck = new Vector2(currentPathNode.X, currentPathNode.Y - 0.577f);

            if (map.cellBounds.Contains(new Vector3Int((int)locationToCheck.x, (int)locationToCheck.y, 0)))
            {
                neighbours.Add(pathNodeManager.pathNodesDict.First(x => x.Key == locationToCheck).Value);
            }

            // Bottom left tile
            locationToCheck = new Vector2(currentPathNode.X - 0.5f, currentPathNode.Y - 0.2885f);

            if (map.cellBounds.Contains(new Vector3Int((int)locationToCheck.x, (int)locationToCheck.y, 0)))
            {
                neighbours.Add(pathNodeManager.pathNodesDict.First(x => x.Key == locationToCheck).Value);
            }

            // left tile
            locationToCheck = new Vector2(currentPathNode.X - 1, currentPathNode.Y);

            if (map.cellBounds.Contains(new Vector3Int((int)locationToCheck.x, (int)locationToCheck.y, 0)))
            {
                neighbours.Add(pathNodeManager.pathNodesDict.First(x => x.Key == locationToCheck).Value);
            }

            // top left tile
            locationToCheck = new Vector2(currentPathNode.X - 0.5f, currentPathNode.Y + 0.2885f);

            if (map.cellBounds.Contains(new Vector3Int((int)locationToCheck.x, (int)locationToCheck.y, 0)))
            {
                neighbours.Add(pathNodeManager.pathNodesDict.First(x => x.Key == locationToCheck).Value);
            }

            if (neighbours.Count > 0)
            {
                return neighbours;
            } 
            else
            {
                Debug.LogError("No neighbours found");
                return new List<PathNode>();
            }

        }

        public PathNode GeneratePathNode(Vector2Int xy)
        {
            PathNode pathNode = new PathNode(WorldToolManager.current.tilemapList[WorldToolManager.current.currentLayer], xy);

            return pathNode;
        }

        public int GetDiagonalDistance(PathNode start, PathNode end)
        {
            int d1 = 10;    // horizontal/vertical movement cost (1 x 10)
            int d2 = 14;    // diagonal axis movement cost (1.4 x 10)
            int dx = (int)Mathf.Abs(start.X - end.X);
            int dy = (int)Mathf.Abs(start.Y - end.X);

            if (dx > dy)
                return d2 * dy + d1 * (dx - dy);
            return d2 * dx + d1 * (dy - dx);
        }


        private List<PathNode> GetFinishedList(PathNode startNode, PathNode endNode)
        {
            List<PathNode> finishedList = new List<PathNode>();

            PathNode currentNode = endNode;

            while (currentNode != startNode)
            {
                finishedList.Add(currentNode);
                currentNode = currentNode.PreviousNode;
            }

            finishedList.Reverse();

            return finishedList;
        }

    }
}

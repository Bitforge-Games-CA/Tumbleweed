using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Tumbleweed.Core.Managers;
using Tumbleweed.Core.WorldGen;

namespace Tumbleweed.Core.UtilityAI
{

    public class Pathfinder
    {
        public List<PathNode> FindPath(PathNode startNode, PathNode endNode)
        {
            List<PathNode> openList = new List<PathNode>() { startNode };
            List<PathNode> closedList = new List<PathNode>();

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

                foreach (var neighbour in GetNeighbourTiles(currentPathNode))
                {
                    if (neighbour.IsNodeBlocked || closedList.Contains(neighbour) || Mathf.Abs(currentPathNode.GridLocation.z - neighbour.GridLocation.z) > 1)
                    {
                        continue;
                    }

                    //neighbour.GCost = GetDiagonalDistance(startNode, neighbour);
                    //neighbour.HCost = GetDiagonalDistance(endNode, neighbour);

                    neighbour.GCost = CalculateChebyshevDistance(startNode, neighbour);
                    neighbour.HCost = CalculateChebyshevDistance(endNode, neighbour);

                    neighbour.PreviousNode = currentPathNode;

                    if (!openList.Contains(neighbour))
                    {
                        openList.Add(neighbour);
                    }
                }
            }

            return new List<PathNode>();
        }

        private List<PathNode> GetNeighbourTiles(PathNode currentPathNode)
        {
            var map = WorldToolManager.current.tilemapList[WorldToolManager.current.currentLayer];

            List<PathNode> neighbours = new List<PathNode>();

            PathNodeManager pathNodeManager = GameObject.Find("Generator " + WorldToolManager.current.currentLayer).GetComponent<PathNodeManager>();

                // Top tile
                Vector3Int locationToCheck = new Vector3Int(currentPathNode.GridLocation.x + 1, currentPathNode.GridLocation.y + 1, currentPathNode.GridLocation.z);

                if (pathNodeManager.pathNodes.Contains(GeneratePathNode(locationToCheck)))
                {
                    neighbours.Add(GeneratePathNode(locationToCheck));

                }

                // Top right tile
                locationToCheck = new Vector3Int(currentPathNode.GridLocation.x + 1, currentPathNode.GridLocation.y, currentPathNode.GridLocation.z);

                if (pathNodeManager.pathNodes.Contains(GeneratePathNode(locationToCheck)))
                {
                    neighbours.Add(GeneratePathNode(locationToCheck));
                }

                // right tile
                locationToCheck = new Vector3Int(currentPathNode.GridLocation.x + 1, currentPathNode.GridLocation.y - 1, currentPathNode.GridLocation.z);

                if (pathNodeManager.pathNodes.Contains(GeneratePathNode(locationToCheck)))
                {
                    neighbours.Add(GeneratePathNode(locationToCheck));
                }

                // Bottom right tile
                locationToCheck = new Vector3Int(currentPathNode.GridLocation.x - 1, currentPathNode.GridLocation.y, currentPathNode.GridLocation.z);

                if (pathNodeManager.pathNodes.Contains(GeneratePathNode(locationToCheck)))
                {
                    neighbours.Add(GeneratePathNode(locationToCheck));
                }

                // Bottom tile
                locationToCheck = new Vector3Int(currentPathNode.GridLocation.x - 1, currentPathNode.GridLocation.y - 1, currentPathNode.GridLocation.z);

                if (pathNodeManager.pathNodes.Contains(GeneratePathNode(locationToCheck)))
                {
                    neighbours.Add(GeneratePathNode(locationToCheck));
                }

                // Bottom left tile
                locationToCheck = new Vector3Int(currentPathNode.GridLocation.x, currentPathNode.GridLocation.y - 1, currentPathNode.GridLocation.z);

                if (pathNodeManager.pathNodes.Contains(GeneratePathNode(locationToCheck)))
                {
                    neighbours.Add(GeneratePathNode(locationToCheck));
                }

                // left tile
                locationToCheck = new Vector3Int(currentPathNode.GridLocation.x - 1, currentPathNode.GridLocation.y + 1, currentPathNode.GridLocation.z);

                if (pathNodeManager.pathNodes.Contains(GeneratePathNode(locationToCheck)))
                {
                    neighbours.Add(GeneratePathNode(locationToCheck));
                }

                // top left tile
                locationToCheck = new Vector3Int(currentPathNode.GridLocation.x, currentPathNode.GridLocation.y + 1, currentPathNode.GridLocation.z);

                if (pathNodeManager.pathNodes.Contains(GeneratePathNode(locationToCheck)))
                {
                    neighbours.Add(GeneratePathNode(locationToCheck));
                }

                return neighbours;
        }   

        public PathNode GeneratePathNode(int x, int y, int z)
        {
            PathNode pathNode = new PathNode(WorldToolManager.current.tilemapList[WorldToolManager.current.currentLayer], new Vector3Int(x, y, z));

            return pathNode;
        }

        public PathNode GeneratePathNode(float x, float y, float z)
        {
            PathNode pathNode = new PathNode(WorldToolManager.current.tilemapList[WorldToolManager.current.currentLayer], new Vector3Int((int)x, (int)y, (int)z));

            return pathNode;
        }

        public PathNode GeneratePathNode(Vector3Int xyz)
        {
            PathNode pathNode = new PathNode(WorldToolManager.current.tilemapList[WorldToolManager.current.currentLayer], xyz);

            return pathNode;
        }

        public int GetDiagonalDistance(PathNode start, PathNode neighbour)
        {
            int d1 = 1;
            int d2 = 1;
            int dx = Mathf.Abs(start.X - neighbour.X);
            int dy = Mathf.Abs(start.Y - neighbour.Y);
            return d1 * (dx + dy) + (d2 - 2 * d1) * Mathf.Min(dx, dy);
        }

        public int CalculateChebyshevDistance(PathNode start, PathNode neighbour)
        {
            var dx = Math.Abs(neighbour.X - start.X);
            var dy = Math.Abs(neighbour.Y - start.Y);
            return (dx + dy) - Math.Min(dx, dy);
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


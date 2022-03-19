using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Pathfinder
{
    public List<PathNode> FindPath(PathNode startNode, PathNode endNode)
    {
        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();

        openList.Add(startNode);

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

            var neighbourTiles = GetNeighbourTiles(currentPathNode);

            foreach (var neighbour in neighbourTiles)
            {
                if (neighbour.IsNodeBlocked || closedList.Contains(neighbour) || Mathf.Abs(currentPathNode.GridLocation.z - neighbour.GridLocation.z) > 1)
                {
                    continue;
                }

                neighbour.GCost = GetDiagonalDistance(startNode, neighbour);
                neighbour.HCost = GetDiagonalDistance(endNode, neighbour);

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
        var map = WorldToolManager.current.tilemap;

        List<PathNode> neighbours = new List<PathNode>();

        // Top tile
        PathNode locationToCheck = GeneratePathNode(currentPathNode.GridLocation.x, currentPathNode.GridLocation.y + 1, currentPathNode.GridLocation.z);

        if (map.cellBounds.Contains(locationToCheck.GridLocation))
        {
            neighbours.Add(locationToCheck);
        }

        // Top right tile
         locationToCheck = GeneratePathNode(currentPathNode.GridLocation.x + 1, currentPathNode.GridLocation.y + 1, currentPathNode.GridLocation.z);

        if (map.cellBounds.Contains(locationToCheck.GridLocation))
        {
            neighbours.Add(locationToCheck);
        }

        // right tile
        locationToCheck = GeneratePathNode(currentPathNode.GridLocation.x + 1, currentPathNode.GridLocation.y, currentPathNode.GridLocation.z);

        if (map.cellBounds.Contains(locationToCheck.GridLocation))
        {
            neighbours.Add(locationToCheck);
        }

        // Bottom right tile
        locationToCheck = GeneratePathNode(currentPathNode.GridLocation.x + 1, currentPathNode.GridLocation.y - 1, currentPathNode.GridLocation.z);

        if (map.cellBounds.Contains(locationToCheck.GridLocation))
        {
            neighbours.Add(locationToCheck);
        }

        // Bottom tile
        locationToCheck = GeneratePathNode(currentPathNode.GridLocation.x, currentPathNode.GridLocation.y - 1, currentPathNode.GridLocation.z);

        if (map.cellBounds.Contains(locationToCheck.GridLocation))
        {
            neighbours.Add(locationToCheck);
        }

        // Bottom left tile
        locationToCheck = GeneratePathNode(currentPathNode.GridLocation.x - 1, currentPathNode.GridLocation.y - 1, currentPathNode.GridLocation.z);

        if (map.cellBounds.Contains(locationToCheck.GridLocation))
        {
            neighbours.Add(locationToCheck);
        }

        // left tile
        locationToCheck = GeneratePathNode(currentPathNode.GridLocation.x - 1, currentPathNode.GridLocation.y, currentPathNode.GridLocation.z);

        if (map.cellBounds.Contains(locationToCheck.GridLocation))
        {
            neighbours.Add(locationToCheck);
        }

        // top left tile
        locationToCheck = GeneratePathNode(currentPathNode.GridLocation.x - 1, currentPathNode.GridLocation.y + 1, currentPathNode.GridLocation.z);

        if (map.cellBounds.Contains(locationToCheck.GridLocation))
        {
            neighbours.Add(locationToCheck);
        }

        return neighbours;

    }


    public PathNode GeneratePathNode(int x, int y, int z )
    {
        PathNode pathNode = new PathNode(WorldToolManager.current.currentGrid, new Vector3Int(x, y, z));

        pathNode.X = x;
        pathNode.Y = y;
        pathNode.Z = z;
            
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


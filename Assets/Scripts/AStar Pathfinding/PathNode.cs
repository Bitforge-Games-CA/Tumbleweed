using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{

    private Grid PathfindingGrid;
    public int X;
    public int Y;
    public int Z;
    

    public bool IsNodeBlocked;

    public int GCost;
    public int HCost;
    public int FCost { get { return GCost + HCost; } }

    public PathNode PreviousNode;

    public Vector3Int GridLocation;

    public PathNode(Grid grid, Vector3Int xyz)
    {
        PathfindingGrid = grid;
        this.X = xyz.x;
        this.Y = xyz.y;
        this.Z = xyz.z;
    }

    public override string ToString()
    {
        return X + "," + Y + "," + Z;
    }




}

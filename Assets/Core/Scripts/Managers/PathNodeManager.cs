using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tumbleweed.Core.UtilityAI;

public class PathNodeManager : MonoBehaviour
{
    public List<PathNode> pathNodes = new List<PathNode>();

    public Vector3Int GetXYZFromPathNode(PathNode nodeToFind, List<PathNode> pathNodes)
    {
       if (pathNodes.Contains(nodeToFind))
       {
            var x = nodeToFind.X;
            var y = nodeToFind.Y;
            var z = nodeToFind.Z;

            return new Vector3Int(x, y, z);
       } 
       else
       {
            Debug.Log("node not found");
            return new Vector3Int(0, 0, 0);
       }
    }

}

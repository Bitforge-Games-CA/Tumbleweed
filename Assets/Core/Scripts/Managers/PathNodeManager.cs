using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tumbleweed.Core.UtilityAI;

public class PathNodeManager : MonoBehaviour
{
    public List<PathNode> pathNodes = new List<PathNode>();

    public Dictionary<Vector2, PathNode> pathNodesDict = new Dictionary<Vector2, PathNode>();

    public Vector2 GetXYZFromPathNode(PathNode nodeToFind, List<PathNode> pathNodes)
    {
       if (pathNodes.Contains(nodeToFind))
       {
            var x = nodeToFind.X;
            var y = nodeToFind.Y;

            return new Vector2(x, y);
       } 
       else
       {
            Debug.Log("node not found");
            return new Vector2(0, 0);
       }
    }

}

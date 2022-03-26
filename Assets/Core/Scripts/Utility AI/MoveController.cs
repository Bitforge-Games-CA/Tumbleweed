using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tumbleweed.Core.Managers;

namespace Tumbleweed.Core.UtilityAI
{

    public class MoveController : MonoBehaviour
    {
        public float Speed;
        public bool IsMoving;
        public Vector3Int CurrentPosition;

        public Pathfinder Pathfinder;
        public List<PathNode> Path = new List<PathNode>();

        // Start is called before the first frame update
        void Start()
        {
             Pathfinder = new Pathfinder();
             Path = GetPath(new Vector3Int(-1, 1, 5), new Vector3Int(-6, -3, 3));
             foreach (PathNode p in Path)
             {
                 Debug.Log(p);
             }
             if (Path.Count == 0)
             {
                 Debug.Log("Path Empty");
             }

        }

        // Update is called once per frame
        void Update()
        {
          
        }

        public List<PathNode> GetPath(Vector3Int startPos, Vector3Int endPos)
        {
            PathNode startNode = Pathfinder.GeneratePathNode(startPos);
            PathNode endNode = Pathfinder.GeneratePathNode(endPos);

            List<PathNode> path = Pathfinder.FindPath(startNode, endNode);

            return path;
        }



    }

}
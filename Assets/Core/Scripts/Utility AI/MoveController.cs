using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Tumbleweed.Core.Managers;
using Tumbleweed.Core.XML.Data;
using Tumbleweed.Core.UtilityAI;
using System.Linq;

namespace Tumbleweed.Core.UtilityAI
{

    public class MoveController : MonoBehaviour
    {
        public float Speed;
        public float Timer;
        public bool IsMoving;
        public Vector3Int CurrentPosition;

        public Pathfinder Pathfinder;
        public List<PathNode> Path = new List<PathNode>();
        public WorldData WorldData;
        public PathNodeManager PathNodeManager;
        public Rigidbody2D RB2D;

        // Start is called before the first frame update
        void Start()
        {
             WorldData = gameObject.GetComponent<WorldData>();
             Pathfinder = new Pathfinder();
             PathNodeManager = GameObject.Find("Generator " + WorldData.CurrentLayer).GetComponent<PathNodeManager>();
             RB2D = gameObject.GetComponent<Rigidbody2D>();
             CurrentPosition = new Vector3Int(WorldData.WorldPositionX, WorldData.WorldPositionY, WorldData.WorldPositionZ);

             MoveTo(new Vector2(-1, 0.577f), new Vector2(-1, -6.924f));
             
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void MoveTo(Vector2 startPos, Vector2 endPos)
        {
            if (!TimeManager.current.PausedTime)
            {
                // get the path
                Path = GetPath(startPos, endPos);

                // null check
                if (Path.Count > 0)
                {
                    // debug
                    Debug.Log("Path Found");

                    foreach (PathNode p in Path)
                    {
                        // debug 2
                        Debug.Log(p);
                        Debug.DrawLine(new Vector3(p.PreviousNode.GridLocation.x, p.PreviousNode.GridLocation.y), new Vector3(p.GridLocation.x, p.GridLocation.y), Color.blue, 30);
                    }

                    // move logic
                    Debug.Log("Starting walk");
                    IsMoving = true;
                    InvokeRepeating("Move_Tick", 0.25f, 0.25f);

                }
                else if (Path.Count == 0)
                {
                    // debug 3
                    Debug.Log("ERROR: Path Not Found");
                }
            }
        }


        public List<PathNode> GetPath(Vector2 startPos, Vector2 endPos)
        {
            //PathNode startNode = PathNodeManager.pathNodes.Find(x => x.GridLocation == startPos);
            //PathNode endNode = PathNodeManager.pathNodes.Find(x => x.GridLocation == endPos);

            PathNode startNode = PathNodeManager.pathNodesDict.First(x => x.Key == startPos).Value;
            PathNode endNode = PathNodeManager.pathNodesDict.First(x => x.Key == endPos).Value;

            List<PathNode> path = Pathfinder.FindPath(startNode, endNode, WorldData);

            return path;
        }


        public void Move_Tick()
        {
            if (!TimeManager.current.PausedTime)
            {   // A
                RB2D.transform.position = Vector2.MoveTowards(new Vector2(RB2D.position.x, RB2D.position.y), Path[0].GridLocation, 1);

                // B
                //Vector2 velocity = Path[0].GridLocation - RB2D.position;
                //RB2D.transform.position = Vector2.SmoothDamp(new Vector2(RB2D.position.x, RB2D.position.y), Path[0].GridLocation, ref velocity, 0.20f);

                // C
                //RB2D.velocity = (Path[0].GridLocation - RB2D.position).normalized * Speed;

                Path.RemoveAt(0);

                if (Path.Count <= 0)
                {
                    IsMoving = false;
                    Debug.Log("Destination reached");
                    CancelInvoke();
                }

            }

        }



    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Tumbleweed.Core.Managers;
using Tumbleweed.Core.XML.Data;
using System.Linq;
using DG.Tweening;

namespace Tumbleweed.Core.UtilityAI
{
    // TO DO:
    // let NPC move around according to their considerations and actions



    public class MoveController : MonoBehaviour
    {
        public float Speed;
        public float Timer;
        public bool IsMoving;
        public Vector3Int CurrentPosition;

        public Pathfinder Pathfinder;
        public List<PathNode> Path = new List<PathNode>();
        public CharacterWorldData CharacterWorldData;
        public PathNodeManager PathNodeManager;
        public Rigidbody2D RB2D;

        // Start is called before the first frame update
        void Start()
        {
             CharacterWorldData = gameObject.GetComponent<CharacterWorldData>();
             Pathfinder = new Pathfinder();
             PathNodeManager = GameObject.Find("Generator " + CharacterWorldData.CurrentLayer).GetComponent<PathNodeManager>();
             RB2D = gameObject.GetComponent<Rigidbody2D>();
             CurrentPosition = new Vector3Int(CharacterWorldData.WorldPositionX, CharacterWorldData.WorldPositionY, CharacterWorldData.WorldPositionZ);

             // TEST MOVE
             MoveTo(new Vector2(-1, 0.577f), new Vector2(-1, -6.924f));
             
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
                    InvokeRepeating("Move_Tick", TimeManager.current.TimeScaleSeconds, TimeManager.current.TimeScaleSeconds);

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
            PathNode startNode = PathNodeManager.pathNodesDict.First(x => x.Key == startPos).Value;
            PathNode endNode = PathNodeManager.pathNodesDict.First(x => x.Key == endPos).Value;

            List<PathNode> path = Pathfinder.FindPath(startNode, endNode, CharacterWorldData);

            return path;
        }


        public void Move_Tick()
        {
            if (!TimeManager.current.PausedTime)
            {
                RB2D.DOMove(new Vector3(Path[0].GridLocation.x, Path[0].GridLocation.y, 1), TimeManager.current.TimeScaleSeconds * 0.5f, false);

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
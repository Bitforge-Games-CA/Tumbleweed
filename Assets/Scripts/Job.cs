using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Job : ScriptableObject
{
    public int JobNum { get; set; }
    public string JobType;
    public List<Vector3Int> JobPositions;

}
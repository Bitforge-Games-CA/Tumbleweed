using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Job : ScriptableObject
{
    public int JobNum;
    public Vector3 JobPos;
    public string JobSprite;

    public Job(Vector3 pos)
    {
        JobPos = pos;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager2 : MonoBehaviour
{

    public static JobManager2 current;

    public List<Job2> JobsListAll = new List<Job2>();
    public List<Job2> JobsListFlatten = new List<Job2>();
    public List<Job2> JobsListMine = new List<Job2>();
    public List<Job2> JobsListHarvest = new List<Job2>();

    public List<Vector3> PositionsList;

    public bool ListUpdatedAdd = true;
    public bool ListUpdatedRemove = true;

    public bool IsFlatten = false;
    public bool IsMining = false;
    public bool isHarvesting = false;

    public int passIndex = 0;
    public Job2 CurrentJob;
    public Job2 EmptyJob;


    // Start is called before the first frame update
    void Start()
    {
        // instatiate the singleton
        current = this;
        JobsListAll.Clear();
    }

    // Update is called once per frame
    void Update()
    {


        if (WorldToolManager.current.AddedTilePos != null && ListUpdatedAdd == false && Input.GetMouseButton(1))
        {
            Debug.Log("added tile to positions list");
            CurrentJob = ScriptableObject.CreateInstance<Job2>();

            ListUpdatedAdd = true;
        }

        if (WorldToolManager.current.AddedTilePos != null && ListUpdatedAdd == true && Input.GetMouseButtonUp(1))
        {
            JobsListAll.Add(CurrentJob);

        }

        if (WorldToolManager.current.RemovedTilePos != null && ListUpdatedRemove == false && Input.GetMouseButton(0))
        {




            ListUpdatedRemove = true;
        }



    }


}

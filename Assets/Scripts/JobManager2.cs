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

    public List<Vector3Int> PositionsList;
    public List<Vector3Int> PrevPositionsList;

    public bool ListUpdatedAdd = true;
    public bool ListUpdatedRemove = true;

    public bool IsFlatten = false;
    public bool IsMining = false;
    public bool isHarvesting = false;

    public Job2 CurrentJob;


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
            PositionsList.Add(WorldToolManager.current.AddedTilePos);
            ListUpdatedAdd = true;


        }

        if (WorldToolManager.current.AddedTilePos != null && ListUpdatedAdd == true && Input.GetMouseButtonUp(1))
        {
            Job2 CurrentJob = ScriptableObject.Instantiate<Job2>(ScriptableObject.CreateInstance<Job2>());
            CurrentJob.JobPositions = new List<Vector3Int>(PositionsList);

            JobsListAll.Add(CurrentJob);

            PositionsList.Clear();

        }

        if (WorldToolManager.current.RemovedTilePos != null && ListUpdatedRemove == false && Input.GetMouseButton(0))
        {

            foreach (Job2 job in JobsListAll)
            {
                if (job.JobPositions.Contains(WorldToolManager.current.RemovedTilePos))
                {
                    job.JobPositions.Remove(WorldToolManager.current.RemovedTilePos);
                }
            }

            ListUpdatedRemove = true;
        }

        CleanJobListAll();

    }

    // helper methods

    public void RemoveJobFromListAll(Job2 job)
    {
        if (job.JobPositions.Count == 0)
        {
            JobsListAll.Remove(job);
        }

    }

    public void CleanJobListAll()
    {
        foreach(Job2 job in JobsListAll)
        {
            if (job.JobPositions.Count == 0)
            {
                JobsListAll.Remove(job);
                break;
            }
        }
    }

    public Job2 JobFromPosList(List<Vector3Int> positions)
    {

        Job2 newJob = ScriptableObject.Instantiate<Job2>(ScriptableObject.CreateInstance<Job2>());
        newJob.JobPositions = positions;
        return newJob;
    }



}

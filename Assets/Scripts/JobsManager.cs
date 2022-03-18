using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobsManager : MonoBehaviour
{

    public static JobsManager current;

    public List<Job> JobsListAll = new List<Job>();
    public List<Job> JobsListFlatten = new List<Job>();
    public List<Job> JobsListMine = new List<Job>();
    public List<Job> JobsListHarvest = new List<Job>();

    public bool ListUpdatedAdd = true;
    public bool ListUpdatedRemove = true;
    public int AddsInARow = 0;

    public bool IsFlatten = false;
    public bool IsMining = false;
    public bool isHarvesting = false;

    public int AddsInARowFlatten = 0;
    public int AddsInARowMining = 0;
    public int AddsInARowHarvesting = 0;

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

        if (WorldToolManager.current.AddedTilePos != null && ListUpdatedAdd == false)
        {
            Job job = ScriptableObject.CreateInstance<Job>();
            job = JobFromPos(WorldToolManager.current.AddedTilePos);
            JobsListAll.Add(job);

            if (IsFlatten)
            {
                job.JobNum = AddsInARowFlatten;
                JobsListFlatten.Add(job);
                AddsInARowFlatten++;

                IsFlatten = false;
            }




            ListUpdatedAdd = true;
            AddsInARow++;
        }

        if (WorldToolManager.current.RemovedTilePos != null && ListUpdatedRemove == false)
        {
            AddsInARow = 0;

            Job job = ScriptableObject.CreateInstance<Job>();
            job = JobFromPos(WorldToolManager.current.RemovedTilePos);
            job.JobNum = AddsInARow;
            JobsListAll.RemoveAt(job.JobNum);

            if (IsFlatten)
            {
                job.JobNum = AddsInARowFlatten;
                JobsListFlatten.RemoveAt(job.JobNum);
                AddsInARowFlatten = 0;

                IsFlatten = false;
            }


            ListUpdatedRemove = true;
        }

    }


    Job JobFromPos(Vector3 pos)
    {
        Job newJob = ScriptableObject.CreateInstance<Job>();
        newJob.JobPos = pos;
        return newJob;
    }
}

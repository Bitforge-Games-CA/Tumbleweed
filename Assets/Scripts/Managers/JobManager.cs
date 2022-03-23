using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tumbleweed.Core.Managers
{

    public class JobManager : MonoBehaviour
    {

        public static JobManager current;

        public List<Job> JobsListAll = new List<Job>();
        public List<Job> JobsListFlatten = new List<Job>();
        public List<Job> JobsListMine = new List<Job>();
        public List<Job> JobsListHarvest = new List<Job>();

        public List<Vector3Int> PositionsList;
        public List<Vector3Int> PrevPositionsList;

        public bool ListUpdatedAdd = true;
        public bool ListUpdatedRemove = true;

        public bool IsFlatten = false;
        public bool IsMining = false;
        public bool IsHarvesting = false;

        public Job CurrentJob;


        // Start is called before the first frame update
        void Start()
        {
            // instatiate the singleton
            if (current == null)
            {
                current = this;
            }
            else if (current != this)
            {
                Destroy(current);
            }
            JobsListAll.Clear();
        }

        // Update is called once per frame
        void Update()
        {
            if (WorldToolManager.current.AddedTilePos != null && ListUpdatedAdd == false && Input.GetMouseButton(1))
            {
                PositionsList.Add(WorldToolManager.current.AddedTilePos);
                ListUpdatedAdd = true;


            }

            if (WorldToolManager.current.AddedTilePos != null && ListUpdatedAdd == true && Input.GetMouseButtonUp(1))
            {
                Job CurrentJob = ScriptableObject.Instantiate<Job>(ScriptableObject.CreateInstance<Job>());
                CurrentJob.JobPositions = new List<Vector3Int>(PositionsList);

                if (IsFlatten == true)
                {
                    CurrentJob.JobType = "Flattening";
                    IsFlatten = false;
                }


                if (IsMining == true)
                {
                    CurrentJob.JobType = "Mining";
                    IsMining = false;
                }


                if (IsHarvesting == true)
                {
                    CurrentJob.JobType = "Harvesting";
                    IsHarvesting = false;
                }

                JobsListAll.Add(CurrentJob);

                PositionsList.Clear();
            }

            if (WorldToolManager.current.RemovedTilePos != null && ListUpdatedRemove == false && Input.GetMouseButton(0))
            {

                foreach (Job job in JobsListAll)
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

        public void RemoveJobFromListAll(Job job)
        {
            if (job.JobPositions.Count == 0)
            {
                JobsListAll.Remove(job);
            }

        }

        public void CleanJobListAll()
        {
            var jobs = JobsListAll.ToList();
            for (int i = 0; i < jobs.Count; i++)
            {
                var job = jobs[i];
                if (job.JobPositions.Count == 0)
                    JobsListAll.Remove(job);
            }
        }

        public Job JobFromPosList(List<Vector3Int> positions)
        {

            Job newJob = ScriptableObject.Instantiate<Job>(ScriptableObject.CreateInstance<Job>());
            newJob.JobPositions = positions;
            return newJob;
        }

    }

}
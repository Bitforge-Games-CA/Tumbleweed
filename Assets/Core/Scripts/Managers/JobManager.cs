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

        public List<Job> JobsListHaul = new List<Job>();

        public List<Vector3Int> PositionsList;
        public List<Vector3Int> PrevPositionsList;

        public bool ListUpdatedAdd = true;
        public bool ListUpdatedRemove = true;

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
                CurrentJob.Positions = new List<Vector3Int>(PositionsList);

                if (WorldToolManager.current.isTileDesignatorFlattenActive == true)
                {
                    CurrentJob.JobType = "Flattening";
                    JobsListFlatten.Add(CurrentJob);
                }


                if (WorldToolManager.current.isTileDesignatorMiningActive == true)
                {
                    CurrentJob.JobType = "Mining";
                    JobsListMine.Add(CurrentJob);
                }


                if (WorldToolManager.current.isTileDesignatorHarvestingActive == true)
                {
                    CurrentJob.JobType = "Harvesting";
                    JobsListHarvest.Add(CurrentJob);
                }

                JobsListAll.Add(CurrentJob);
                

                PositionsList.Clear();
            }

            if (WorldToolManager.current.RemovedTilePos != null && ListUpdatedRemove == false && Input.GetMouseButton(0))
            {

                foreach (Job job in JobsListAll)
                {
                    if (job.Positions.Contains(WorldToolManager.current.RemovedTilePos))
                    {
                        job.Positions.Remove(WorldToolManager.current.RemovedTilePos);
                    }
                }

                foreach (Job job in JobsListFlatten)
                {
                    if (job.Positions.Contains(WorldToolManager.current.RemovedTilePos))
                    {
                        job.Positions.Remove(WorldToolManager.current.RemovedTilePos);
                    }
                }

                foreach (Job job in JobsListMine)
                {
                    if (job.Positions.Contains(WorldToolManager.current.RemovedTilePos))
                    {
                        job.Positions.Remove(WorldToolManager.current.RemovedTilePos);
                    }
                }

                foreach (Job job in JobsListHarvest)
                {
                    if (job.Positions.Contains(WorldToolManager.current.RemovedTilePos))
                    {
                        job.Positions.Remove(WorldToolManager.current.RemovedTilePos);
                    }
                }

                ListUpdatedRemove = true;
            }

            CleanJobListAll();
            CleanJobListFlatten();
            CleanJobListMine();
            CleanJobListHarvest();

        }

        // helper methods

        public void RemoveJobFromListAll(Job job)
        {
            if (job.Positions.Count == 0)
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
                if (job.Positions.Count == 0)
                    JobsListAll.Remove(job);
            }
        }

        public void CleanJobListFlatten()
        {
            var jobs = JobsListFlatten.ToList();
            for (int i = 0; i < jobs.Count; i++)
            {
                var job = jobs[i];
                if (job.Positions.Count == 0)
                    JobsListAll.Remove(job);
            }
        }

        public void CleanJobListMine()
        {
            var jobs = JobsListMine.ToList();
            for (int i = 0; i < jobs.Count; i++)
            {
                var job = jobs[i];
                if (job.Positions.Count == 0)
                    JobsListAll.Remove(job);
            }
        }

        public void CleanJobListHarvest()
        {
            var jobs = JobsListHarvest.ToList();
            for (int i = 0; i < jobs.Count; i++)
            {
                var job = jobs[i];
                if (job.Positions.Count == 0)
                    JobsListAll.Remove(job);
            }
        }

        public Job JobFromPosList(List<Vector3Int> positions)
        {

            Job newJob = ScriptableObject.Instantiate<Job>(ScriptableObject.CreateInstance<Job>());
            newJob.Positions = positions;
            return newJob;
        }

    }

}
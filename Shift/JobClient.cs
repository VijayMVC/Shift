﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Shift.Entities;
using Shift.DataLayer;
using Autofac;
using Autofac.Features.ResolveAnything;
using System.Threading.Tasks;

[assembly: CLSCompliant(true)]
namespace Shift
{
    public class JobClient
    {
        private IJobDAL jobDAL = null;

        ///<summary>
        /// Initializes a new instance of JobClient class, injects data layer with connection and configuration strings.
        /// Only three options are used for the client:
        /// * DBConnectionString
        /// * UseCache
        /// * CacheConfigurationString
        /// * EncryptionKey (optional)
        /// 
        /// If UseCache is true, the CacheConfigurationString is required, if false, then it is optional.
        ///</summary>
        ///<param name="config">Setup the database connection string, cache configuration.</param>
        ///
        public JobClient(ClientConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("Unable to start with no configuration.");
            }

            if (string.IsNullOrWhiteSpace(config.StorageMode))
            {
                throw new ArgumentNullException("The storage mode must not be empty.");

            }

            if (string.IsNullOrWhiteSpace(config.DBConnectionString))
            {
                throw new ArgumentNullException("Unable to run without DB storage connection string.");

            }

            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            RegisterAssembly.RegisterTypes(builder, config.StorageMode, config.DBConnectionString, config.EncryptionKey, config.DBAuthKey);
            var container = builder.Build();
            //Use lifetime scope to avoid memory leak http://docs.autofac.org/en/latest/resolve/
            using (var scope = container.BeginLifetimeScope())
            {
                jobDAL = container.Resolve<IJobDAL>();
            }
        }

        public JobClient(IJobDAL jobDAL)
        {
            this.jobDAL = jobDAL;
        }

        #region Add Expression<action>
        //Provides the clients to submit jobs or commands for jobs

        /// <summary>
        /// Add a method and parameters into the job table.
        /// Ref and out parameters are not supported.
        /// </summary>
        /// <param name="methodCall">Expression body for method call </param>
        /// <returns>JobID of the added job.</returns>
        public string Add(Expression<Action> methodCall)
        {
            return jobDAL.Add(null, null, null, null, methodCall);
        }

        public Task<string> AddAsync(Expression<Action> methodCall)
        {
            return jobDAL.AddAsync(null, null, null, null, methodCall);
        }

        /// <summary>
        /// Add a method and parameters into the job table.
        /// Ref and out parameters are not supported.
        /// </summary>
        /// <param name="appID">Client application ID</param>
        /// <param name="methodCall">Expression body for method call </param>
        /// <returns>JobID of the added job.</returns>
        public string Add(string appID, Expression<Action> methodCall)
        {
            return jobDAL.Add(appID, null, null, null, methodCall);
        }

        public Task<string> AddAsync(string appID, Expression<Action> methodCall)
        {
            return jobDAL.AddAsync(appID, null, null, null, methodCall);
        }

        /// <summary>
        /// Add a method and parameters into the job table.
        /// Job name defaults to class.method name.
        /// Ref and out parameters are not supported.
        /// </summary>
        /// <param name="appID">Client application ID</param>
        /// <param name="userID">User ID</param>
        /// <param name="jobType">Job type category/group</param>
        /// <param name="methodCall">Expression body for method call </param>
        /// <returns>JobID of the added job.</returns>
        public string Add(string appID, string userID, string jobType, Expression<Action> methodCall)
        {
            return jobDAL.Add(appID, userID, jobType, null, methodCall);
        }

        public Task<string> AddAsync(string appID, string userID, string jobType, Expression<Action> methodCall)
        {
            return jobDAL.AddAsync(appID, userID, jobType, null, methodCall);
        }

        /// <summary>
        /// Add a method and parameters into the job table with a custom name.
        /// Ref and out parameters are not supported.
        /// </summary>
        /// <param name="appID">Client application ID</param>
        /// <param name="userID">User ID</param>
        /// <param name="jobType">Job type category/group</param>
        /// <param name="jobName">Name for this job</param>
        /// <param name="methodCall">Expression body for method call </param>
        /// <returns>JobID of the added job.</returns>
        public string Add(string appID, string userID, string jobType, string jobName, Expression<Action> methodCall)
        {
            return jobDAL.Add(appID, userID, jobType, jobName, methodCall);
        }

        public Task<string> AddAsync(string appID, string userID, string jobType, string jobName, Expression<Action> methodCall)
        {
            return jobDAL.AddAsync(appID, userID, jobType, jobName, methodCall);
        }
        #endregion 

        #region Add Func<Task>
        //Provides the clients to submit jobs or commands for jobs

        /// <summary>
        /// Add a method and parameters into the job table.
        /// Ref and out parameters are not supported.
        /// </summary>
        /// <param name="methodCall">Expression body for method call </param>
        /// <returns>JobID of the added job.</returns>
        public string Add(Expression<Func<Task>> methodCall)
        {
            return jobDAL.Add(null, null, null, null, methodCall);
        }

        public Task<string> AddAsync(Expression<Func<Task>> methodCall)
        {
            return jobDAL.AddAsync(null, null, null, null, methodCall);
        }

        /// <summary>
        /// Add a method and parameters into the job table.
        /// Ref and out parameters are not supported.
        /// </summary>
        /// <param name="appID">Client application ID</param>
        /// <param name="methodCall">Expression body for method call </param>
        /// <returns>JobID of the added job.</returns>
        public string Add(string appID, Expression<Func<Task>> methodCall)
        {
            return jobDAL.Add(appID, null, null, null, methodCall);
        }

        public Task<string> AddAsync(string appID, Expression<Func<Task>> methodCall)
        {
            return jobDAL.AddAsync(appID, null, null, null, methodCall);
        }

        /// <summary>
        /// Add a method and parameters into the job table.
        /// Job name defaults to class.method name.
        /// Ref and out parameters are not supported.
        /// </summary>
        /// <param name="appID">Client application ID</param>
        /// <param name="userID">User ID</param>
        /// <param name="jobType">Job type category/group</param>
        /// <param name="methodCall">Expression body for method call </param>
        /// <returns>JobID of the added job.</returns>
        public string Add(string appID, string userID, string jobType, Expression<Func<Task>> methodCall)
        {
            return jobDAL.Add(appID, userID, jobType, null, methodCall);
        }

        public Task<string> AddAsync(string appID, string userID, string jobType, Expression<Func<Task>> methodCall)
        {
            return jobDAL.AddAsync(appID, userID, jobType, null, methodCall);
        }

        /// <summary>
        /// Add a method and parameters into the job table with a custom name.
        /// Ref and out parameters are not supported.
        /// </summary>
        /// <param name="appID">Client application ID</param>
        /// <param name="userID">User ID</param>
        /// <param name="jobType">Job type category/group</param>
        /// <param name="jobName">Name for this job</param>
        /// <param name="methodCall">Expression body for method call </param>
        /// <returns>JobID of the added job.</returns>
        public string Add(string appID, string userID, string jobType, string jobName, Expression<Func<Task>> methodCall)
        {
            return jobDAL.Add(appID, userID, jobType, jobName, methodCall);
        }

        public Task<string> AddAsync(string appID, string userID, string jobType, string jobName, Expression<Func<Task>> methodCall)
        {
            return jobDAL.AddAsync(appID, userID, jobType, jobName, methodCall);
        }


        #endregion

        #region Update Expression<Action>
        /// <summary>
        /// Update a job's method and parameters.
        /// Ref and out parameters are not supported.
        /// </summary>
        /// <param name="jobID">Existing job ID</param>
        /// <param name="methodCall">Expression body for method call </param>
        /// <returns>Number of successfully updated job</returns>
        public int Update(string jobID, Expression<Action> methodCall)
        {
            return jobDAL.Update(jobID, null, null, null, null, methodCall);
        }

        public Task<int> UpdateAsync(string jobID, Expression<Action> methodCall)
        {
            return jobDAL.UpdateAsync(jobID, null, null, null, null, methodCall);
        }

        /// <summary>
        /// Update a job's method and parameters.
        /// Ref and out parameters are not supported.
        /// </summary>
        /// <param name="jobID">Existing job ID</param>
        /// <param name="appID">Client application ID</param>
        /// <param name="methodCall">Expression body for method call </param>
        /// <returns>Number of successfully updated job</returns>
        public int Update(string jobID, string appID, Expression<Action> methodCall)
        {
            return jobDAL.Update(jobID, appID, null, null, null, methodCall);
        }

        public Task<int> UpdateAsync(string jobID, string appID, Expression<Action> methodCall)
        {
            return jobDAL.UpdateAsync(jobID, appID, null, null, null, methodCall);
        }

        /// <summary>
        /// Update a job's method and parameters.
        /// Ref and out parameters are not supported.
        /// </summary>
        /// <param name="jobID">Existing job ID</param>
        /// <param name="appID">Client application ID</param>
        /// <param name="userID">User ID</param>
        /// <param name="jobType">Job type category/group</param>
        /// <param name="jobName">Name for this job</param>
        /// <param name="methodCall">Expression body for method call </param>
        /// <returns>Number of successfully updated job</returns>
        public int Update(string jobID, string appID, string userID, string jobType, string jobName, Expression<Action> methodCall)
        {
            return jobDAL.Update(jobID, appID, userID, jobType, jobName, methodCall);
        }

        public Task<int> UpdateAsync(string jobID, string appID, string userID, string jobType, string jobName, Expression<Action> methodCall)
        {
            return jobDAL.UpdateAsync(jobID, appID, userID, jobType, jobName, methodCall);
        }
        #endregion

        #region Update Expression<Func<Task>>
        /// <summary>
        /// Update a job's method and parameters.
        /// Ref and out parameters are not supported.
        /// </summary>
        /// <param name="jobID">Existing job ID</param>
        /// <param name="methodCall">Expression body for method call </param>
        /// <returns>Number of successfully updated job</returns>
        public int Update(string jobID, Expression<Func<Task>> methodCall)
        {
            return jobDAL.Update(jobID, null, null, null, null, methodCall);
        }

        public Task<int> UpdateAsync(string jobID, Expression<Func<Task>> methodCall)
        {
            return jobDAL.UpdateAsync(jobID, null, null, null, null, methodCall);
        }

        /// <summary>
        /// Update a job's method and parameters.
        /// Ref and out parameters are not supported.
        /// </summary>
        /// <param name="jobID">Existing job ID</param>
        /// <param name="appID">Client application ID</param>
        /// <param name="methodCall">Expression body for method call </param>
        /// <returns>Number of successfully updated job</returns>
        public int Update(string jobID, string appID, Expression<Func<Task>> methodCall)
        {
            return jobDAL.Update(jobID, appID, null, null, null, methodCall);
        }

        public Task<int> UpdateAsync(string jobID, string appID, Expression<Func<Task>> methodCall)
        {
            return jobDAL.UpdateAsync(jobID, appID, null, null, null, methodCall);
        }

        /// <summary>
        /// Update a job's method and parameters.
        /// Ref and out parameters are not supported.
        /// </summary>
        /// <param name="jobID">Existing job ID</param>
        /// <param name="appID">Client application ID</param>
        /// <param name="userID">User ID</param>
        /// <param name="jobType">Job type category/group</param>
        /// <param name="jobName">Name for this job</param>
        /// <param name="methodCall">Expression body for method call </param>
        /// <returns>Number of successfully updated job</returns>
        public int Update(string jobID, string appID, string userID, string jobType, string jobName, Expression<Func<Task>> methodCall)
        {
            return jobDAL.Update(jobID, appID, userID, jobType, jobName, methodCall);
        }

        public Task<int> UpdateAsync(string jobID, string appID, string userID, string jobType, string jobName, Expression<Func<Task>> methodCall)
        {
            return jobDAL.UpdateAsync(jobID, appID, userID, jobType, jobName, methodCall);
        }
        #endregion

        ///<summary>
        /// Set "stop" command to already running or not running jobs.
        ///</summary>
        ///<returns>Number of affected jobs.</returns>
        public int SetCommandStop(IList<string> jobIDs)
        {
            if (jobIDs == null || jobIDs.Count == 0)
                return 0;

            return jobDAL.SetCommandStop(jobIDs);
        }

        public Task<int> SetCommandStopAsync(IList<string> jobIDs)
        {
            if (jobIDs == null || jobIDs.Count == 0)
                return Task.FromResult(0);

            return jobDAL.SetCommandStopAsync(jobIDs);
        }

        ///<summary>
        /// Set "pause" command to running jobs.
        ///</summary>
        ///<returns>Number of affected jobs.</returns>
        public int SetCommandPause(IList<string> jobIDs)
        {
            if (jobIDs == null || jobIDs.Count == 0)
                return 0;

            return jobDAL.SetCommandPause(jobIDs);
        }

        public Task<int> SetCommandPauseAsync(IList<string> jobIDs)
        {
            if (jobIDs == null || jobIDs.Count == 0)
                return Task.FromResult(0);

            return jobDAL.SetCommandPauseAsync(jobIDs);
        }

        ///<summary>
        /// Set "continue" command to paused jobs.
        ///</summary>
        ///<returns>Number of affected jobs.</returns>
        public int SetCommandContinue(IList<string> jobIDs)
        {
            if (jobIDs == null || jobIDs.Count == 0)
                return 0;

            return jobDAL.SetCommandContinue(jobIDs);
        }

        public Task<int> SetCommandContinueAsync(IList<string> jobIDs)
        {
            if (jobIDs == null || jobIDs.Count == 0)
                return Task.FromResult(0);

            return jobDAL.SetCommandContinueAsync(jobIDs);
        }

        ///<summary>
        /// Set "run-now" command to ready to run jobs.
        ///</summary>
        ///<returns>Number of affected jobs.</returns>
        public int SetCommandRunNow(IList<string> jobIDs)
        {
            if (jobIDs == null || jobIDs.Count == 0)
                return 0;

            return jobDAL.SetCommandRunNow(jobIDs);
        }

        public Task<int> SetCommandRunNowAsync(IList<string> jobIDs)
        {
            if (jobIDs == null || jobIDs.Count == 0)
                return Task.FromResult(0);

            return jobDAL.SetCommandRunNowAsync(jobIDs);
        }

        ///<summary>
        /// Gets the job instance based on a unique job ID.
        ///</summary>
        ///<param name="jobID"></param>
        ///<returns>Job</returns>
        public Job GetJob(string jobID)
        {
            return jobDAL.GetJob(jobID);
        }

        public Task<Job> GetJobAsync(string jobID)
        {
            return jobDAL.GetJobAsync(jobID);
        }

        ///<summary>
        /// Gets the job instance with progress info based on a unique job ID.
        ///</summary>
        ///<param name="jobID"></param>
        ///<returns>JobView</returns>
        public JobView GetJobView(string jobID)
        {
            return jobDAL.GetJobView(jobID);
        }

        public Task<JobView> GetJobViewAsync(string jobID)
        {
            return jobDAL.GetJobViewAsync(jobID);
        }

        /// <summary>
        /// Return job views based on page index and page size.
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Total count of job views and list of JobViews</returns>
        public JobViewList GetJobViews(int? pageIndex, int? pageSize)
        {
            return jobDAL.GetJobViews(pageIndex, pageSize);
        }

        public Task<JobViewList> GetJobViewsAsync(int? pageIndex, int? pageSize)
        {
            return jobDAL.GetJobViewsAsync(pageIndex, pageSize);
        }

        ///<summary>
        /// Resets non running jobs. Jobs will be ready for another run after a successful reset.
        ///</summary>
        ///<param name="jobIDs">Job IDs collection.</param>
        ///<returns>Number of affected jobs.</returns>
        public int ResetJobs(IList<string> jobIDs)
        {
            if (jobIDs == null || jobIDs.Count == 0)
                return 0;

            return jobDAL.Reset(jobIDs);
        }

        public Task<int> ResetJobsAsync(IList<string> jobIDs)
        {
            if (jobIDs == null || jobIDs.Count == 0)
                return Task.FromResult(0);

            return jobDAL.ResetAsync(jobIDs);
        }

        ///<summary>
        /// Deletes non running jobs. 
        ///</summary>
        ///<param name="jobIDs">Job IDs collection.</param>
        ///<returns>Number of affected jobs.</returns>
        public int DeleteJobs(IList<string> jobIDs)
        {
            if (jobIDs == null || jobIDs.Count == 0)
                return 0;

            return jobDAL.Delete(jobIDs);
        }

        public Task<int> DeleteJobsAsync(IList<string> jobIDs)
        {
            if (jobIDs == null || jobIDs.Count == 0)
                return Task.FromResult(0);

            return jobDAL.DeleteAsync(jobIDs);
        }

        ///<summary>
        /// Return counts of all job statuses (running, not running, completed, stopped, with errors).
        /// Useful for UI reporting of job statuses.
        ///</summary>
        ///<param name="appID">Client application ID, optional</param>
        ///<param name="userID">User ID, optional.</param>
        ///<returns>Collection of JobStatusCount</returns>
        public IReadOnlyCollection<JobStatusCount> GetJobStatusCount(string appID, string userID)
        {
            return jobDAL.GetJobStatusCount(appID, userID);
        }

        public Task<IReadOnlyCollection<JobStatusCount>> GetJobStatusCountAsync(string appID, string userID)
        {
            return jobDAL.GetJobStatusCountAsync(appID, userID);
        }

        ///<summary>
        /// Gets the current progress of job.
        /// Try to retrieve progress from cache first and then from DB if not available.
        ///</summary>
        ///<param name="jobID"></param>
        ///<returns>JobStatusProgress</returns>
        public JobStatusProgress GetProgress(string jobID)
        {
            return jobDAL.GetProgress(jobID);
        }

        public Task<JobStatusProgress> GetProgressAsync(string jobID)
        {
            return jobDAL.GetProgressAsync(jobID);
        }



    }
}

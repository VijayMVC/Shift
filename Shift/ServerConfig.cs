﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shift
{
    public class ServerConfig
    {
        [Required]
        public string ProcessID { get; set; }

        [Required]
        public string DBConnectionString { get; set; }
        public string DBAuthKey { get; set; }

        [Required]
        public string StorageMode { get; set; } = Shift.StorageMode.Redis; //mssql, redis, etc... 

        //Maximum jobs to run for each worker
        public int MaxRunnableJobs { get; set; } = 100;

        //Points to a text file with list of DLLs to load, one DLL per line. 
        public string AssemblyListPath { get; set; } //Optional
        public string AssemblyFolder { get; set; } //Optional

        public string EncryptionKey { get; set; } //optional, if set, then parameters will be encrypted/decrypted automatically during storage

        //Time interval when updating job's progress in JobServer DB. 
        //If UseCache is true, then server will cache progress in between the interval. 
        //If UseCache is false, possibly need to lower this interval value, instead of default to 10 sec.  
        //Avoid hitting the DB too much, since the Progress event update can be very chatty and rapid.
        public TimeSpan? ProgressDBInterval { get; set; } = new TimeSpan(0, 0, 10);

        public int ServerTimerInterval { get; set; } = 5000; //interval timer for server running jobs
        public int ServerTimerInterval2 { get; set; } = 10000; //interval timer2 for server running cleanup

        public int? AutoDeletePeriod { get; set; } //Default = null; hours before jobs are deleted 
        public IList<JobStatus?> AutoDeleteStatus { get; set; } = new List<JobStatus?> { JobStatus.Completed }; //Jobs with status to delete

        public bool PollingOnce { get; set; } = false; //Poll server only once, useful for debugging

        public int Workers { get; set; } = 1; //How many workers per server.
        public bool ForceStopServer { get; set; } = false;//If true, no waiting for all running jobs to stop before shutting down server 
        public int StopServerDelay { get; set; } = 30000; //Delay shut down
    }
}

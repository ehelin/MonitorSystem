using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.interfaces;
using BLL;
using System.Data.SqlClient;
using System.IO;
using Shared.misc;
using BLL.jobs;
using Shared.Helpers;

namespace BLL.implementations
{
    public class JobChecks : Default
    {
        public JobChecks()
        {
            Name = Constants.JOB_CHECKS;
            Jobs = new List<Job>();
            AddJobs();
        }

        private void AddJobs()
        {
            Jobs.Add(new MonitorWebsite(Constants.JOB_CHECKS, Constants.WEB_SITE_CHECK_JOB, Constants.WEBSITES_TO_CHECK));
            Jobs.Add(new DriveSizeCheckJob(Constants.JOB_CHECKS, Constants.DRIVE_SPACE_CHECK_JOB, Constants.DRIVES_TO_CHECK_FOR_SPACE));
            Jobs.Add(new GetDirectoryFileCounts(Constants.JOB_CHECKS, Constants.DIRECTORY_FILE_CHECKS_JOB, Constants.DIRECTORIES_TO_GET_COUNTS));
        }
    }
}

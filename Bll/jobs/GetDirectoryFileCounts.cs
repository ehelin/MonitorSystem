using System;
using System.Collections.Generic;
using Shared.data;
using Shared.Helpers;
using System.IO;
using BLL.interfaces;
using Shared.misc;

namespace BLL.jobs
{
    public class GetDirectoryFileCounts : Job, iSystemJob
    {
        private string appPathSettingName = string.Empty; 

        public GetDirectoryFileCounts(string pSystem,
                                      string pJobName,
                                      string pAppPathSettingName)
        {
            SystemName = pSystem;
            JobName = pJobName;
            appPathSettingName = pAppPathSettingName;
        }

        public override void JobAction()
        {
            try
            {
                GetCounts();
            }
            catch (Exception e)
            {
                dispOut.Events.Add(JobConstants.ERROR + e.Message + JobConstants.STACKTRACE + e.StackTrace.ToString());
            }
        }      

        private void GetCounts()
        {            
            IList<string> directories = GetDirectoriesForCounts();

            foreach (string directory in directories)
            {
                dispOut.Events.Add(Messages.GETTING_COUNTS + directory);

                IOStats stats = GetDirectoryCounts(directory);

                dispOut.Events.Add(Messages.FILE_COUNT_LABEL + stats.FileCounts.ToString());
                dispOut.Events.Add(Messages.DIRECTORY_COUNT_LABEL + stats.DirectoryCounts.ToString());
            }
        }        

        private IList<string> GetDirectoriesForCounts()
        {
            IList<string> directoriesForCounts = new List<string>();
            string directories = Utility.GetAppSetting(appPathSettingName);

            foreach(string directory in directories.Split(','))
            {
                string dir = directory.Replace(JobConstants.NEW_LINE, string.Empty);
                dir = dir.Trim();
                directoriesForCounts.Add(dir);
            }
            
            return directoriesForCounts;
        }
                
        private IOStats GetDirectoryCounts(string directory)
        {
            IOStats stats = new IOStats();
            IOStats subStats = null;
            string[] files = Directory.GetFiles(directory);
            string[] directories = Directory.GetDirectories(directory);

            stats.FileCounts = files.Length;
            stats.DirectoryCounts = directories.Length;

            foreach(string dir in directories)
            {
                subStats = GetDirectoryCounts(dir);
                
                stats.FileCounts += subStats.FileCounts;
                stats.DirectoryCounts += subStats.DirectoryCounts;
            }

            return stats;
        }
    }
}

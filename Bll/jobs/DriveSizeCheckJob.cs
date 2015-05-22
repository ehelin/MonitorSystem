using Shared.Helpers;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System;
using Shared.misc;

namespace BLL.jobs
{
    internal static class Win32
    {
        [DllImport(JobConstants.KERNAL32, CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool GetDiskFreeSpaceEx(string drive, out long freeBytesForUser, out long totalBytes, out long freeBytes);
    }

    public class DriveSizeCheckJob : Job, BLL.interfaces.iSystemJob
    {
        private string appPathSettingName = string.Empty; 

        public DriveSizeCheckJob(string pSystem,
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
                CheckDrivesForSpace();
            }
            catch (Exception e)
            {
                dispOut.Events.Add(JobConstants.ERROR + e.Message + JobConstants.STACKTRACE + e.StackTrace.ToString());
            }
        }      

        private void CheckDrivesForSpace()
        {            
            IList<string> drivesToCheck = GetDrivesToCheck();

            foreach (string drive in drivesToCheck)
                GetDriveFreeSpace(drive);
        }        

        private IList<string> GetDrivesToCheck()
        {
            IList<string> drivesToCheck = new List<string>();
            string drives = Utility.GetAppSetting(appPathSettingName);

            foreach(string driveStr in drives.Split(','))
            {
                string drive = driveStr.Replace(JobConstants.NEW_LINE, string.Empty);
                drive = drive.Trim();
                drivesToCheck.Add(drive);
            }
            
            return drivesToCheck;
        }
                
        private void GetDriveFreeSpace(string drive)
        {
            long freeBytesForUser = 0;
            long totalBytes = 0;
            long freeBytes = 0;

            dispOut.Events.Add(Messages.GETTING_DRIVE_SPECS + drive);

            if (Win32.GetDiskFreeSpaceEx(drive, out freeBytesForUser, out totalBytes, out freeBytes))
                dispOut.Events.Add(Messages.SPACE_LABEL + (freeBytes / 1000000).ToString() + JobConstants.FORWARD_SLASH + (totalBytes / 1000000).ToString());
            else
                dispOut.Events.Add(Errors.CANNOT_GET_DRIVE_SPECIFICATION + drive);
        }
    }
}

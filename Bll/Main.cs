using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using Shared.data;
using Shared.Helpers;
using Shared.misc;
using System.Diagnostics;
using BLL.factories;
using BLL.jobs;
using BLL.implementations;
using System.Security;

namespace BLL.classes
{
    public class Main
    {
        public DisplayOutput Run()
        {
            List<Default> systems = null;
            DisplayOutput dispOut = new DisplayOutput();

            systems = SystemFactory.GetAllSystems();

            foreach (Default system in systems)
            {
                foreach (Job jb in system.Jobs)
                {
                    jb.JobAction();

                    foreach(string display in jb.GetDisplayOutput().Events)
                        dispOut.Events.Add(display);
                }
            }
            
            EmailResults(dispOut);

            return dispOut;
        }

        private void EmailResults(DisplayOutput dispOut)
        {
            string smtp = Utility.GetAppSetting(Constants.SMTP_ADDRESS);
            string notificationEmail = Utility.GetAppSetting(Constants.NOTIFICATION_EMAIL);
            bool sendEmail = true;

            if (string.IsNullOrEmpty(smtp))
            {
                dispOut.Events.Add(Errors.SMTP_NOT_SET);
                sendEmail = false;
            }

            if (string.IsNullOrEmpty(notificationEmail))
            {
                dispOut.Events.Add(Errors.NOTIFICATION_EMAIL_NOT_SET);
                sendEmail = false;
            }
            
            if (sendEmail)
                Email.AlertEmail(dispOut, smtp, notificationEmail);     
        }
    }
}

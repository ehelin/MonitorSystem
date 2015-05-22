using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared.Helpers;
using Shared.misc;
using BLL.implementations;

namespace BLL.factories
{
    public class SystemFactory
    {
        public static List<Default> GetAllSystems()
        {
            List<Default> systems = new List<Default>();
            Default curClass = null;
            string curClassName = string.Empty;
            string systemsConfigList = System.Configuration.ConfigurationManager.AppSettings[Constants.SYSTEMS];

            foreach (string systemName in Utility.SplitString(',', systemsConfigList))
            {
                Type type = Type.GetType(Constants.CLASS_PREFIX + systemName);
                var obj = Activator.CreateInstance(type, null);

                curClass = (Default)obj;

                if (curClass != null && curClass.Jobs != null && curClass.Jobs.Count()>0)
                    systems.Add((Default)obj);
            }
            
            return systems;
        }
    }
}

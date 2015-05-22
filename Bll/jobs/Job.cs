using System;
using BLL.interfaces;
using Shared.data;

namespace BLL.jobs
{
    public class Job : iSystemJob
    {
        protected string SystemName = string.Empty;           
        protected string JobName = string.Empty;          
        protected DisplayOutput dispOut = new DisplayOutput();
        
        public DisplayOutput GetDisplayOutput()
        {
            return dispOut;
        }

        public string GetJobName()
        {
            return JobName;
        }

        public virtual void JobAction()
        {
            throw new NotImplementedException();
        }
    }
}

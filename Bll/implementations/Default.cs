using BLL.jobs;
using BLL.interfaces;
using System.Collections.Generic;

namespace BLL.implementations
{ 
    public class Default : iSystemMonitor
    {
        public List<Job> Jobs { get; set; }            
        public string Name { get; set; }
    }
}

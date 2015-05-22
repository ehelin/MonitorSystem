using System.Collections.Generic;
using BLL.jobs;

namespace BLL.interfaces
{
    public interface iSystemMonitor
    {
        List<Job> Jobs { get; set; }
    }
}

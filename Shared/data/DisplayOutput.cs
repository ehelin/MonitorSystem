using System.Collections.Generic;

namespace Shared.data
{
    public class DisplayOutput
    {
        public IList<string> Events {get;set;}

        public DisplayOutput()
        {
            Events = new List<string>();
        }
    }
}

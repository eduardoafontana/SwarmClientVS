using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwarmClientVS.Domain.IRepository;

namespace SwarmClientVS.Domain.DataModel
{
    public class SessionData : IData
    {
        public string Label { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }

        public List<BreakpointData> Breakpoints { get; set; } = new List<BreakpointData>();
        public List<EventData> Events { get; set; } = new List<EventData>();
        public List<PathNodeData> PathNodes { get; set; } = new List<PathNodeData>();
        public TaskData Task { get; set; }
        public DeveloperData Developer { get; set; }
    }
}
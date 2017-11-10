using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwarmClientVS.Domain.IRepository;

namespace SwarmClientVS.Domain.DataModel
{
    public class SessionData : ISessionData
    {
        public string Label { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }

        public List<IBreakpointData> Breakpoint { get; set; } = new List<IBreakpointData>();
        public List<IEventData> Event { get; set; } = new List<IEventData>();
        public List<IPathNodeData> PathNode { get; set; } = new List<IPathNodeData>();
        public List<IProjectData> Project { get; set; } = new List<IProjectData>();
        public List<ITaskData> Task { get; set; } = new List<ITaskData>();
        public List<IDeveloperData> Developer { get; set; } = new List<IDeveloperData>();
    }
}
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
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime Started { get; set; }
        public DateTime? Finished { get; set; }

        public List<BreakpointData> Breakpoints { get; set; } = new List<BreakpointData>();
        public List<EventData> Events { get; set; } = new List<EventData>();
        public List<PathNodeData> PathNodes { get; set; } = new List<PathNodeData>();

        public string DeveloperName { get; set; }
        public string TaskName { get; set; }
        public string TaskAction { get; set; }
        public string TaskDescription { get; set; }
        public DateTime? TaskCreated { get; set; }
        public string ProjectName { get; set; }

        public string GetCleanProjectName()
        {
            if (String.IsNullOrWhiteSpace(ProjectName))
                return String.Empty;

            return ProjectName.Replace(".sln", "");
        }
    }
}
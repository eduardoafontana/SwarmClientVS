using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwarmClientVS.Domain.IRepository
{
    public interface ISessionData : IData
    {
        string Label { get; set; }
        string Description { get; set; }
        string Purpose { get; set; }
        DateTime Started { get; set; }
        DateTime Finished { get; set; }
        List<IBreakpointData> Breakpoints { get; set; }
        List<IEventData> Events { get; set; }
        List<IPathNodeData> PathNodes { get; set; }
        ITaskData Task { get; set; }
        IDeveloperData Developer { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwarmClientVS.Domain.IRepository;

namespace SwarmClientVS.Domain.DataModel
{
    public enum EventKind
    {
        StepOut,
        StepInto,
        StepOver,
        Suspend,
        Resume,
        BreakpointAdd,
        BreakpointChange,
        BreakpointRemove,
        SuspendBreakpoint,
        InspectVariable,
        ModifyVariable,

        BreakpointHitted
    };

    public class EventData : IEventData
    {
        public string EventKind { get; set; }
        public string Detail { get; set; }
        public string Namespace { get; set; }
        public string Type { get; set; }
        public string TypeFullPath { get; set; }
        public string Method { get; set; }
        public string MethodKey { get; set; }
        public string MethodSignature { get; set; }
        public int CharStart { get; set; }
        public int CharEnd { get; set; }
        public int LineNumber { get; set; }
        public string LineOfCode { get; set; }
        public DateTime Created { get; set; }
    }
}

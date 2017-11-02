using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwarmClientVS.Domain.IRepository
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
        ModifyVariable
    };

    public interface IEventData : IData
    {
        EventKind EventKind { get; set; }
        string Detail { get; set; }
        string Namespace { get; set; }
        string Type { get; set; }
        string TypeFullPath { get; set; }
        string Method { get; set; }
        string MethodKey { get; set; }
        string MethodSignature { get; set; }
        int CharStart { get; set; }
        int CharEnd { get; set; }
        int LineNumber { get; set; }
        string LineOfCode { get; set; }
        DateTime Created { get; set; }
    }
}

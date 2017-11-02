using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwarmClientVS.Domain.IRepository
{
    public enum BreakpointKind
    {
        Line,
        Conditional,
        Exception
    };

    public interface IBreakpointData : IData
    {
        BreakpointKind BreakpointKind { get; set; }
        string Namespace { get; set; }
        string Type { get; set; }
        int LineNumber { get; set; }
        string LineOfCode { get; set; }
        DateTime Created { get; set; }
    }
}

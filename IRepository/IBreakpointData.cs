using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwarmClientVS.Domain.IRepository
{
    public interface IBreakpointData : IData
    {
        string BreakpointKind { get; set; }
        string Namespace { get; set; }
        string Type { get; set; }
        int LineNumber { get; set; }
        string LineOfCode { get; set; }
        string Origin { get; set; }
        DateTime Created { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwarmClientVS.Domain.IRepository;

namespace SwarmClientVS.Domain.DataModel
{
    public class BreakpointData : IBreakpointData
    {
        public BreakpointKind BreakpointKind { get; set; }
        public string Namespace { get; set; }
        public string Type { get; set; }
        public int LineNumber { get; set; }
        public string LineOfCode { get; set; }
        public DateTime Created { get; set; }
    }
}

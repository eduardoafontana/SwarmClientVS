using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;

namespace SwarmClientVS
{
    public class SCBreakpoint
    {
        public int HashCode { get; set; }
        public string Name { get; set; }
        public string FunctionName { get; set; }
        public int FileLine { get; set; }

        public SCBreakpoint(Breakpoint breakpoint)
        {
            Name = breakpoint.Name;
            FunctionName = breakpoint.FunctionName;
            FileLine = breakpoint.FileLine;
        }

        public override string ToString()
        {
            return String.Format("{0}|{1}|{2}", Name, FunctionName, FileLine);
        }
    }
}

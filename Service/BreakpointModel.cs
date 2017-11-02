using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwarmClientVS.Domain.Service
{
    public class BreakpointModel
    {
        public string Name { get; set; }
        public string FunctionName { get; set; }
        public int FileLine { get; set; }

        public BreakpointModel(string name, string functionName, int fileLine)
        {
            Name = name;
            FunctionName = functionName;
            FileLine = fileLine;
        }

        public override string ToString()
        {
            return String.Format("{0}|{1}|{2}", Name, FunctionName, FileLine);
        }
    }
}

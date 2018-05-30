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
        public int StartLineText { get; set; }
        public DocumentModel DocumentModel { get; set; }
    }
}

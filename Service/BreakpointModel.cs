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
        public DocumentModel DocumentModel { get; set; }

        //TODO : Removed because not used. Review later.
        //public override string ToString()
        //{
        //    return String.Format("{0}|{1}|{2}", Name, FunctionName, FileLine);
        //}
    }
}

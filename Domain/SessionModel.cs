using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SessionModel
    {
        public string StepName { get; set; }
        public string CurrentStackFrameFunctionName { get; set; }
        public string BreakpointLastHitName { get; set; }
        public string CurrentDocumentLine { get; set; }
    }
}

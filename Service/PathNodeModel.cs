using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwarmClientVS.Domain.Service
{
    public class PathNodeModel
    {
        public CurrentCommandStep CurrentCommandStep { get; set; }
        public List<PathNodeItemModel> StackTraceItems { get; set; }
    }
}

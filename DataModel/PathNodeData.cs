using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwarmClientVS.Domain.IRepository;

namespace SwarmClientVS.Domain.DataModel
{
    public class PathNodeData : IPathNodeData
    {
        public string Namespace { get; set; }
        public string Type { get; set; }
        public string Method { get; set; }
        public DateTime Created { get; set; }
    }
}

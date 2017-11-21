using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwarmClientVS.Domain.IRepository
{
    public interface IPathNodeData : IData
    {
        string Namespace { get; set; }
        string Type { get; set; }
        string Method { get; set; }
        string Parent { get; set; }
        DateTime Created { get; set; }
    }
}

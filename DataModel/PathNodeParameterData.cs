using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwarmClientVS.Domain.IRepository;

namespace SwarmClientVS.Domain.DataModel
{
    public class PathNodeParameterData : IData
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}

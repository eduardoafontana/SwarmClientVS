using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SwarmClientVS.Domain.IRepository;

namespace SwarmClientVS.Domain.DataModel
{
    public class ProjectInputData : IData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TaskData> Task { get; set; } = new List<TaskData>();
    }
}

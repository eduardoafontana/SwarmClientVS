using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SwarmClientVS.Domain.IRepository;

namespace SwarmClientVS.Domain.DataModel
{
    public class TaskData : ITaskData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Action { get; set; }

        [JsonConverter(typeof(ConcreteConverter<ProjectData>))]
        public IProjectData Project { get; set; }
    }
}

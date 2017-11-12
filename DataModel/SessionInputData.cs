using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SwarmClientVS.Domain.IRepository;

namespace SwarmClientVS.Domain.DataModel
{
    public class SessionInputData : ISessionInputData
    {
        [JsonConverter(typeof(ConcreteConverter<TaskData>))]
        public ITaskData Task { get; set; }

        [JsonConverter(typeof(ConcreteConverter<DeveloperData>))]
        public IDeveloperData Developer { get; set; }
    }
}

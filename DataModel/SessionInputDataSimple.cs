using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SwarmClientVS.Domain.IRepository;

namespace SwarmClientVS.Domain.DataModel
{
    public class SessionInputDataSimple : IData
    {
        public List<TaskData> Task { get; set; } = new List<TaskData>();
        public string Project { get; set; }
        public string Developer { get; set; }
    }
}

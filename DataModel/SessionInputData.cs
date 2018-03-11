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
    public class SessionInputData : IData
    {
        public List<TaskInputData> Task { get; set; } = new List<TaskInputData>();
        public string Project { get; set; }
        public string Developer { get; set; }
    }
}

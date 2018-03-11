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
    public class TaskInputData : IData
    {
        public string Name { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
    }
}

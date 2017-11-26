using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwarmClientVS.Domain.DataModel;
using SwarmClientVS.Domain.IRepository;

namespace SwarmClientVS.Domain.Service
{
    public class SessionGridViewItemModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskAction Action { get; set; }
        public DateTime Created { get; set; }
    }
}

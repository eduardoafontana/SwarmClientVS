using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwarmClientVS.Domain.IRepository;

namespace SwarmClientVS.Domain.Service
{
    public class SessionListBoxItemModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SessionListBoxItemModel> Task { get; set; } = new List<SessionListBoxItemModel>();        
    }
}

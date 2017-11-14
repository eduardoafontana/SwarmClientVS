using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwarmClientVS.Domain.IRepository;

namespace SwarmClientVS.Domain.Service
{
    public class SessionInputModel
    {
        public List<SessionListBoxItemModel> Project { get; set; } = new List<SessionListBoxItemModel>();
        public string Developer { get; set; }
    }
}

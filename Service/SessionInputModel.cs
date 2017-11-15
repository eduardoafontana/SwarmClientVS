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
        public SessionListBoxItemModel SelectedProject { get; set; }
        public SessionListBoxItemModel SelectedTask { get; set; }
        public string Developer { get; set; }
    }
}

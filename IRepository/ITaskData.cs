using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwarmClientVS.Domain.IRepository
{
    public interface ITaskData : IData
    {
        string Name { get; set; }
        string Description { get; set; }
        IProjectData Project { get; set; }
    }
}

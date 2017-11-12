using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwarmClientVS.Domain.IRepository
{
    public interface ISessionInputData : IData
    {
        ITaskData Task { get; set; }
        IDeveloperData Developer { get; set; }
    }
}

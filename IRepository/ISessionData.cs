using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwarmClientVS.Domain.IRepository
{
    public interface ISessionData : IData
    {
        string Label { get; set; }
        string Description { get; set; }
        string Purpose { get; set; }
        DateTime Started { get; set; }
        DateTime Finished { get; set; }
    }
}

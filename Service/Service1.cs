using SwarmClientVS.Domain.DataModel;
using SwarmClientVS.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwarmClientVS.Domain.Service
{
    public class Service1
    {
        public Service1(IRepository<IData> repository)
        {
            IBreakpointData bbDataModel = new BreakpointData
            {
                BreakpointKind = BreakpointKind.Exception
            };

            repository.Save(bbDataModel);
        }
    }
}

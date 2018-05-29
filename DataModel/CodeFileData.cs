using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwarmClientVS.Domain.IRepository;

namespace SwarmClientVS.Domain.DataModel
{
    public class CodeFileData : IData
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
    }
}

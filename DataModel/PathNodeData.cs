using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwarmClientVS.Domain.IRepository;

namespace SwarmClientVS.Domain.DataModel
{
    public enum PathNodeOrigin
    {
        Trace,
        Breakpoint,
        StepInto
    };

    public class PathNodeData : IData
    {
        public Guid Id { get; set; }
        public string Hash { get; set; }
        public string Namespace { get; set; }
        public string Type { get; set; }
        public string Method { get; set; }
        public string Parent { get; set; }
        public Guid Parent_Id { get; set; }
        public string Origin { get; set; }
        public string ReturnType { get; set; }
        public List<PathNodeParameterData> Parameters { get; set; } = new List<PathNodeParameterData>();
        public DateTime Created { get; set; }
        public CodeMetricData MethodCodeMetric { get; set; }

        public string GetStackTrace()
        {
            return String.Format("{0}.{1}.{2}", Namespace, Type, Method);
        }
    }
}

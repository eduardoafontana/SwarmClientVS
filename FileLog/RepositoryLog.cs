using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwarmClientVS.Domain.IRepository;
using System.IO;

namespace SwarmClientVS.DataLog.FileLog
{
    public class RepositoryLog : IRepository<IData>
    {
        public string FileName { get { return @".\SCLog.txt"; } }

        public void Save(IData dataModel)
        {
            string objJsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dataModel);

            using (StreamWriter file = new StreamWriter(FileName, true, Encoding.UTF8))
            {
                file.WriteLine(objJsonData);
            }
        }
    }
}

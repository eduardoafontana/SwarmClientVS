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
        private string Identifier { get; set; }
        private string DirectoryPath { get { return @".\SwarmData"; } }

        private string DefaultFilePath { get { return String.Format(@".\{0}\swarm-input-data.txt", DirectoryPath); } }
        private string IdentifierFilePath { get { return String.Format(@".\{0}\session-{1}.txt", DirectoryPath, Identifier); } }

        private string FileName
        {
            get
            {
                if (!Directory.Exists(DirectoryPath))
                    Directory.CreateDirectory(DirectoryPath);

                if (String.IsNullOrWhiteSpace(Identifier))
                    return DefaultFilePath;

                return IdentifierFilePath;
            }
        }

        public void GenerateIdentifier()
        {
            Identifier = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

        public void Save(IData dataModel)
        {
            string objJsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dataModel, Newtonsoft.Json.Formatting.Indented);

            using (StreamWriter file = new StreamWriter(FileName, false, Encoding.UTF8))
            {
                file.Write(objJsonData);
            }
        }

        public IData Get<IData>()
        {
            string objJsonData = String.Empty;

            using (StreamReader file = new StreamReader(FileName, Encoding.UTF8))
            {
                objJsonData = file.ReadToEnd();
            }

            return Newtonsoft.Json.JsonConvert.DeserializeObject<IData>(objJsonData); 
        }
    }
}

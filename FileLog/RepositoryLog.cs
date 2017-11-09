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

        private string DefaultFilePath { get { return String.Format(@".\{0}\json-default.txt", DirectoryPath); } }
        private string IdentifierFilePath { get { return String.Format(@".\{0}\{1}.txt", DirectoryPath, Identifier); } }

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
            string objJsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dataModel);

            using (StreamWriter file = new StreamWriter(FileName, true, Encoding.UTF8))
            {
                file.WriteLine(objJsonData);
            }
        }
    }
}

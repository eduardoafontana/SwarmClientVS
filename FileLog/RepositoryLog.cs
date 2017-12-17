using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwarmClientVS.Domain.IRepository;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SwarmClientVS.DataLog.FileLog
{
    public class RepositoryLog : IRepository<IData>
    {
        private string Identifier { get; set; }
        private string DirectoryPath { get { return @"C:\SwarmData"; } }

        private string DefaultFilePath { get { return String.Format(@"{0}\swarm-input-data.txt", DirectoryPath); } }
        private string IdentifierFilePath { get { return String.Format(@"{0}\session-{1}.txt", DirectoryPath, Identifier); } }

        private static readonly HttpClient httpClient = new HttpClient();

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

            SaveOnServer(dataModel);
        }

        private async Task SaveOnServer(IData dataModel)
        {
            try
            {
                string objJsonDataSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(dataModel, Newtonsoft.Json.Formatting.None);

                var buffer = Encoding.UTF8.GetBytes(objJsonDataSerialized);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await httpClient.PostAsync("http://swarmserver.azurewebsites.net/api/session", byteContent);

                var responseString = await response.Content.ReadAsStringAsync();

                if (!responseString.Equals("\"Object created or updated!\""))
                    throw new Exception(responseString);
            }
            catch(Exception ex)
            {
                if (!Directory.Exists(DirectoryPath))
                    Directory.CreateDirectory(DirectoryPath);

                if (!Directory.Exists(String.Format(@"{0}\ServerExceptions", DirectoryPath)))
                    Directory.CreateDirectory(String.Format(@"{0}\ServerExceptions", DirectoryPath));

                using (StreamWriter file = new StreamWriter(String.Format(@"{0}\ServerExceptions\ex-session-{1}.txt", DirectoryPath, Identifier), true, Encoding.UTF8))
                {
                    file.Write(Environment.NewLine + Environment.NewLine + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + Environment.NewLine + ex.ToString());
                }
            }
        }

        public IData Get<IData>()
        {
            if (!Directory.Exists(DirectoryPath))
                return default(IData);

            if (!File.Exists(FileName))
                return default(IData);

            string objJsonData = String.Empty;

            using (StreamReader file = new StreamReader(FileName, Encoding.UTF8))
            {
                objJsonData = file.ReadToEnd();
            }

            return Newtonsoft.Json.JsonConvert.DeserializeObject<IData>(objJsonData); 
        }
    }
}

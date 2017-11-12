using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SwarmClientVS.Domain.IRepository;
using SwarmClientVS.Domain.DataModel;

namespace SwarmClientVS.Domain.Service
{
    public class SessionInputService
    {
        private IRepository<IData> Repository { get; set; }

        public SessionInputService(IRepository<IData> repository)
        {
            Repository = repository;
        }

        public void GetTask()
        {
            TaskData task = Repository.Get<TaskData>();
        }

        public void SaveTask()
        {
            Repository.Save(new TaskData
            {
                Name = "TODO",
                Description = "TODO",
                Project = new ProjectData
                {
                    Name = "TODO",
                    Description = "TODO"
                }
            });
        }
    }
}
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

        public SessionModel GetInputDataState()
        {
            ISessionInputData inputData = Repository.Get<SessionInputData>();

            if (inputData == null)
                return new SessionModel { };

            return new SessionModel
            {
                Task = inputData.Task.Name,
                TaskDescription = inputData.Task.Description,
                Project = inputData.Task.Project.Name,
                ProjectDescription = inputData.Task.Project.Description,
                Developer = inputData.Developer.Name
            };
        }

        public void PersistInputDataState(SessionModel sessionModel)
        {
            Repository.Save(new SessionInputData
            {
                Task = new TaskData
                {
                    Name = sessionModel.Task,
                    Description = sessionModel.TaskDescription,
                    Project = new ProjectData
                    {
                        Name = sessionModel.Project,
                        Description = sessionModel.ProjectDescription
                    }
                },
                Developer = new DeveloperData
                {
                    Name = sessionModel.Developer
                }
            });
        }
    }
}
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
            SessionInputData inputData = Repository.Get<SessionInputData>();

            if (inputData == null)
            return new SessionModel { };

            return new SessionModel
            {
                Task = inputData.ProjectInput[0].Task[0].Name,
                TaskDescription = inputData.ProjectInput[0].Task[0].Description,
                Project = inputData.ProjectInput[0].Name,
                ProjectDescription = inputData.ProjectInput[0].Task[0].Description,
                Developer = inputData.Developer.Name
            };
        }

        public void PersistInputDataState(SessionModel sessionModel)
        {

            Repository.Save(new SessionInputData
            {
                ProjectInput = new List<ProjectInputData>() { new ProjectInputData
                {
                    Name = sessionModel.Project,
                    Description = sessionModel.ProjectDescription,
                    Task =  new List<TaskData>() { new TaskData { Name = sessionModel.Task, Description = sessionModel.TaskDescription } }
                }},
                Developer = new DeveloperData
                {
                    Name = sessionModel.Developer
                }
            });
        }
    }
}
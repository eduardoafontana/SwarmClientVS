using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SwarmClientVS.Domain.IRepository;
using SwarmClientVS.Domain.DataModel;
using System.Security.Principal;

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
                inputData = new SessionInputData { };

            if (inputData.Developer == null)
                inputData.Developer = new DeveloperData { };

            return new SessionModel
            {
                //Task = inputData.ProjectInput[0].Task[0].Name,
                //TaskDescription = inputData.ProjectInput[0].Task[0].Description,
                //Project = inputData.ProjectInput[0].Name,
                //ProjectDescription = inputData.ProjectInput[0].Task[0].Description,
                Developer = String.IsNullOrWhiteSpace(inputData.Developer.Name) ? WindowsIdentity.GetCurrent().Name : inputData.Developer.Name
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
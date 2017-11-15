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
        private string OpenedSolutionName;

        public SessionInputService(IRepository<IData> repository, string solutionName)
        {
            Repository = repository;
            OpenedSolutionName = solutionName;
        }

        public SessionInputModel GetInputDataState()
        {
            SessionInputData inputData = Repository.Get<SessionInputData>();

            if (inputData == null)
                inputData = new SessionInputData { };

            if (inputData.Developer == null)
                inputData.Developer = new DeveloperData { };

            SessionInputModel sessionInputModel = new SessionInputModel
            {
                Project = inputData.ProjectInput.Select(x => new SessionListBoxItemModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Task = x.Task.Select(p => new SessionListBoxItemModel
                    {
                        Name = p.Name,
                        Description = p.Description
                    }).ToList()
                }).ToList(),
                Developer = String.IsNullOrWhiteSpace(inputData.Developer.Name) ? WindowsIdentity.GetCurrent().Name : inputData.Developer.Name
            };

            sessionInputModel.SelectedProject = sessionInputModel.Project.Where(p => p.Name.Equals(OpenedSolutionName)).FirstOrDefault() ?? new SessionListBoxItemModel { Name = OpenedSolutionName };
            sessionInputModel.SelectedTask = sessionInputModel.SelectedProject.Task.LastOrDefault() ?? new SessionListBoxItemModel { };

            return sessionInputModel;
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
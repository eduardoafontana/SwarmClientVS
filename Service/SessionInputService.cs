using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SwarmClientVS.Domain.IRepository;
using SwarmClientVS.Domain.DataModel;
using System.Security.Principal;
using System.ComponentModel;

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

            sessionInputModel.SelectedProject = sessionInputModel.Project.Where(p => p.Name.Equals(inputData.SelectedProject.Name)).FirstOrDefault() 
                ?? sessionInputModel.Project.Where(p => p.Name.Equals(OpenedSolutionName)).FirstOrDefault() 
                ?? new SessionListBoxItemModel { Name = OpenedSolutionName };

            sessionInputModel.SelectedTask = sessionInputModel.SelectedProject.Task.Where(p => p.Name.Equals(inputData.SelectedTask.Name)).FirstOrDefault()
                ?? sessionInputModel.SelectedProject.Task.LastOrDefault() ?? new SessionListBoxItemModel { };

            return sessionInputModel;
        }

        public SessionInputModelSimple GetInputDataStateSimple()
        {
            SessionInputDataSimple inputData = Repository.Get<SessionInputDataSimple>();

            if (inputData == null)
                inputData = new SessionInputDataSimple { };

            SessionInputModelSimple sessionInputModel = new SessionInputModelSimple
            {
                Task = new BindingList<SessionGridViewItemModel>(inputData.Task.Select(x => new SessionGridViewItemModel
                {
                    Title = x.Name,
                    Description = x.Description
                }).ToList()),
                Project = OpenedSolutionName
            };

            return sessionInputModel;
        }

        //On persist, get developer and selectedTask=last

        public void PersistInputDataState(SessionInputModel sessionInputModel)
        {
            Repository.Save(new SessionInputData
            {
                ProjectInput = sessionInputModel.Project.Select(p => new ProjectInputData
                {
                    Name = p.Name, Description = p.Description, Task = p.Task.Select(t => new TaskData
                    {
                        Name = t.Name,
                        Description = t.Description
                    }).ToList()
                }).ToList(),
                SelectedProject = new ProjectData
                {
                    Name = sessionInputModel.SelectedProject.Name,
                    Description = sessionInputModel.SelectedProject.Description
                },
                SelectedTask = new TaskData
                {
                    Name = sessionInputModel.SelectedTask.Name,
                    Description = sessionInputModel.SelectedTask.Description
                },
                Developer = new DeveloperData
                {
                    Name = sessionInputModel.Developer
                }
            });
        }
    }
}
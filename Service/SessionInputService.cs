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
    public enum TaskAction
    {
        SearchingBug,
        ResolvingBug,
        AnalyzingCode,
        NewFeature,
        Refactoring
    }

    public class SessionInputService
    {
        private IRepository<IData> Repository { get; set; }
        private string OpenedSolutionName;

        public SessionInputService(IRepository<IData> repository, string solutionName)
        {
            Repository = repository;
            OpenedSolutionName = solutionName;
        }

        public SessionInputModelSimple GetInputDataStateSimple()
        {
            SessionInputData inputData = Repository.Get<SessionInputData>();

            if (inputData == null)
                inputData = new SessionInputData { };

            SessionInputModelSimple sessionInputModel = new SessionInputModelSimple
            {
                Task = new BindingList<SessionGridViewItemModel>(inputData.Task.Select(x => new SessionGridViewItemModel
                {
                    Title = x.Name,
                    Description = x.Description,
                    Action = (TaskAction)Enum.Parse(typeof(TaskAction), x.Action),
                    Created = x.Created
                }).ToList()),
                Project = OpenedSolutionName
            };

            return sessionInputModel;
        }

        public SessionInputData GetInputData()
        {
            SessionInputData inputData = Repository.Get<SessionInputData>();

            if (inputData == null)
                inputData = new SessionInputData { };

            return inputData;
        }

        public void PersistInputDataStateSimple(SessionInputModelSimple sessionInputModel)
        {
            Repository.Save(new SessionInputData
            {
                Task = sessionInputModel.Task.Select(t => new TaskData
                {
                    Name = t.Title,
                    Description = t.Description,
                    Action = t.Action.ToString(),
                    Created = t.Created == DateTime.MinValue ? DateTime.Now : t.Created
                }).ToList(),
                Project = sessionInputModel.Project,
                Developer = WindowsIdentity.GetCurrent().Name
            });
        }
    }
}
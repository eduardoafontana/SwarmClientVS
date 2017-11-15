using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SwarmClientVS.DataLog.FileLog;
using SwarmClientVS.Domain.IRepository;
using SwarmClientVS.Domain.Service;

namespace SwarmClientVS
{
    public partial class SessionInputForm : Form
    {
        private SessionService SessionService;
        private SessionInputService SessionInputService;
        private string OpenedSolutionName;

        public SessionInputForm(SessionService sessionService, string solutionName)
        {
            InitializeComponent();

            SessionService = sessionService;
            OpenedSolutionName = solutionName;
            SessionInputService = new SessionInputService(new RepositoryLog());

            LoadInputData();
        }

        private void LoadInputData()
        {
            SessionInputModel sessionModel = SessionInputService.GetInputDataState();

            lstProject.DataSource = sessionModel.Project;
            lstProject.DisplayMember = "Name";
            lstProject.ClearSelected();

            SessionListBoxItemModel openedSolutionBoxItem = sessionModel.Project.Where(p => p.Name.Equals(OpenedSolutionName)).FirstOrDefault();

            if (openedSolutionBoxItem != null)
            {
                lstProject.SelectedItem = openedSolutionBoxItem;
                PopulateProjectFields(openedSolutionBoxItem);
            }
            else
            {
                PopulateProjectFields(new SessionListBoxItemModel { Name = OpenedSolutionName });
            }

            //txtTaskTitle.Text = sessionModel.Task;
            //txtTaskDescription.Text = sessionModel.TaskDescription;
            txtDeveloper.Text = sessionModel.Developer;
        }

        private void PopulateProjectFields(SessionListBoxItemModel sessionListBoxItemModel)
        {
            if (sessionListBoxItemModel == null)
                return;

            txtProjectTitle.Text = sessionListBoxItemModel.Name;
            txtProjectDescription.Text = sessionListBoxItemModel.Description;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            SessionService.RegisterSessionInformation(new SessionModel
            {
                Project = txtProjectTitle.Text,
                ProjectDescription = txtProjectDescription.Text,
                Task = txtTaskTitle.Text,
                TaskDescription = txtTaskDescription.Text,
                Developer = txtDeveloper.Text
            });

            SessionInputService.PersistInputDataState(new SessionModel
            {
                Project = txtProjectTitle.Text,
                ProjectDescription = txtProjectDescription.Text,
                Task = txtTaskTitle.Text,
                TaskDescription = txtTaskDescription.Text,
                Developer = txtDeveloper.Text
            });

            Close();
        }
    }
}

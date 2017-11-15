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

        public SessionInputForm(SessionService sessionService, string solutionName)
        {
            InitializeComponent();

            SessionService = sessionService;
            SessionInputService = new SessionInputService(new RepositoryLog(), solutionName);

            LoadInputData();
        }

        private void LoadInputData()
        {
            SessionInputModel sessionInputModel = SessionInputService.GetInputDataState();

            lstProject.DataSource = sessionInputModel.Project;
            lstProject.DisplayMember = "Name";
            lstProject.ClearSelected();
            lstProject.SelectedItem = sessionInputModel.SelectedProject;

            txtProjectTitle.Text = sessionInputModel.SelectedProject.Name;
            txtProjectDescription.Text = sessionInputModel.SelectedProject.Description;

            lstTask.DataSource = sessionInputModel.SelectedProject.Task;
            lstTask.DisplayMember = "Name";
            lstTask.ClearSelected();
            lstTask.SelectedItem = sessionInputModel.SelectedTask;

            txtTaskTitle.Text = sessionInputModel.SelectedTask.Name;
            txtTaskDescription.Text = sessionInputModel.SelectedTask.Description;

            txtDeveloper.Text = sessionInputModel.Developer;
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

            SessionInputService.PersistInputDataState(new SessionInputModel
            {
                Project = lstProject.Items.Cast<SessionListBoxItemModel>().ToList(),
                Developer = txtDeveloper.Text
            });

            Close();
        }

        private void lstProject_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lstProject.SelectedItem == null)
                return;

            SessionListBoxItemModel selectedProject = (SessionListBoxItemModel)lstProject.SelectedItem;

            txtProjectTitle.Text = selectedProject.Name;
            txtProjectDescription.Text = selectedProject.Description;

            txtTaskTitle.Text = String.Empty;
            txtTaskDescription.Text = String.Empty;

            lstTask.DataSource = selectedProject.Task;
            lstTask.DisplayMember = "Name";
            lstTask.ClearSelected();
            lstTask.SelectedItem = selectedProject.Task.LastOrDefault() ?? new SessionListBoxItemModel { };
        }

        private void lstTask_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lstTask.SelectedItem == null)
                return;

            txtTaskTitle.Text = ((SessionListBoxItemModel)lstTask.SelectedItem).Name;
            txtTaskDescription.Text = ((SessionListBoxItemModel)lstTask.SelectedItem).Description;
        }
    }
}

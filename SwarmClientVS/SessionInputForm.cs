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

        public SessionInputForm(SessionService sessionService)
        {
            InitializeComponent();

            SessionService = sessionService;
            SessionInputService = new SessionInputService(new RepositoryLog());

            LoadInputData();
        }

        private void LoadInputData()
        {
            SessionModel sessionModel = SessionInputService.GetInputDataState();
           
            txtProjectTitle.Text = sessionModel.Project;
            txtTaskTitle.Text = sessionModel.Task;
            txtDeveloper.Text = sessionModel.Developer;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            SessionService.RegisterSessionInformation(new SessionModel
            {
                Project = txtProjectTitle.Text,
                Task = txtTaskTitle.Text,
                Developer = txtDeveloper.Text
            });

            SessionInputService.PersistInputDataState(new SessionModel
            {
                Project = txtProjectTitle.Text,
                Task = txtTaskTitle.Text,
                Developer = txtDeveloper.Text
            });

            Close();
        }
    }
}

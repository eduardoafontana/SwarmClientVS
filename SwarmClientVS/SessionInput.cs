using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SwarmClientVS.Domain.IRepository;
using SwarmClientVS.Domain.Service;

namespace SwarmClientVS
{
    public partial class SessionInput : Form
    {
        private SessionService SessionService;

        public SessionInput(SessionService sessionService)
        {
            InitializeComponent();

            SessionService = sessionService;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            SessionService.RegisterSessionInformation(new SessionModel
            {
                Project = txtProjectTitle.Text,
                Task = txtTaskTitle.Text,
                Developer = txtDeveloper.Text
            });

            Close();
        }
    }
}

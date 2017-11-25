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
    public partial class SessionInputFormSimple : Form
    {
        private SessionInputService SessionInputService;
        private SessionInputModelSimple SessionInputModelSimple;

        //public enum TaskActionOption
        //{
        //    SearchingBug,
        //    ResolvingBug
        //}

        public SessionInputFormSimple(string solutionName)
        {
            InitializeComponent();

            SessionInputService = new SessionInputService(new RepositoryLog(), solutionName);

            LoadInputData();
        }

        private void LoadInputData()
        {
            SessionInputModelSimple = SessionInputService.GetInputDataStateSimple();

            dgTask.AutoGenerateColumns = false;

            dgTask.Columns.Add(new DataGridViewTextBoxColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                Name = "TaskTitle",
                HeaderText = "Task Title",
                DataPropertyName = "Title",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                MaxInputLength = 75,
                Width = 150,
                DisplayIndex = 0,
            });

            dgTask.Columns.Add(new DataGridViewTextBoxColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                Name = "TaskDescription",
                HeaderText = "Task Description",
                DataPropertyName = "Description",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                MaxInputLength = 100,
                Width = 225,
                DisplayIndex = 2
            });

            dgTask.DataSource = SessionInputModelSimple.Task;

            lblProject.Text = SessionInputModelSimple.Project;

            ValidateStart();
        }

        private void dgTask_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == dgTask.NewRowIndex || e.RowIndex < 0)
                return;

            if (e.ColumnIndex == dgTask.Columns["TaskDelete"].Index)
            {
                DialogResult dialogResult = MessageBox.Show("Do you really want to delete this task row?", "Delete Task", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.No)
                    return;

                SessionInputModelSimple.Task.RemoveAt(e.RowIndex);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            SessionInputService.PersistInputDataStateSimple(SessionInputModelSimple);

            Close();
        }

        private void dgTask_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            ValidateStart();
        }

        private void dgTask_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            ValidateStart();
        }

        private void ValidateStart()
        {
            btnStart.Enabled = true;

            if (dgTask.Rows.Count != 1)
                return;

            if (String.IsNullOrWhiteSpace(Convert.ToString(dgTask.Rows[0].Cells["TaskTitle"].Value)))
                btnStart.Enabled = false;
        }
    }
}

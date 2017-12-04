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
        private SessionInputModel SessionInputModelSimple;

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

            dgTask.Columns.Add(new DataGridViewComboBoxColumn()
            {
                CellTemplate = new DataGridViewComboBoxCell(),

                DataSource = Enum.GetValues(typeof(TaskAction)),
                ValueType = typeof(TaskAction),

                Name = "TaskAction",
                HeaderText = "Task Action",
                DataPropertyName = "Action",
                Resizable = DataGridViewTriState.False,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Width = 100,
                DisplayIndex = 1
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
            if (!NooneTaskTitleEmpty())
                return;

            SessionInputService.PersistInputDataStateSimple(SessionInputModelSimple);

            Close();
        }

        private bool NooneTaskTitleEmpty()
        {
            MarkTaskTitleEmpty();

            if (dgTask.Rows.Count == 1)
                return false;

            return dgTask.Rows.Cast<DataGridViewRow>().Where(x => String.IsNullOrWhiteSpace(Convert.ToString(x.Cells["TaskTitle"].Value))).Count() == 1; ;
        }

        private void MarkTaskTitleEmpty()
        {
            foreach (DataGridViewRow item in dgTask.Rows)
            {
                if (dgTask.Rows.Count > 1 && item.IsNewRow)
                    continue;

                if (String.IsNullOrWhiteSpace(Convert.ToString(item.Cells["TaskTitle"].Value)))
                    item.Cells["TaskTitle"].Style.BackColor = Color.Tomato;
            }
        }

        private void dgTask_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (String.IsNullOrWhiteSpace(Convert.ToString(dgTask.Rows[e.RowIndex].Cells["TaskTitle"].Value)))
                dgTask.Rows[e.RowIndex].Cells["TaskTitle"].Style.BackColor = Color.Tomato;
            else
                dgTask.Rows[e.RowIndex].Cells["TaskTitle"].Style.BackColor = Color.White;
        }

        private void dgTask_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //TODO: review later. Try to disable delete button on newrow
            //dgTask.Rows[dgTask.NewRowIndex].Cells["TaskDelete"].ReadOnly = true;
        }
    }
}

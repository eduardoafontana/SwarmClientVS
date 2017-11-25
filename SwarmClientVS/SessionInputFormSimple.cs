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

        //BindingList<TaskGridItem> listData = new BindingList<TaskGridItem>();

        public enum TaskActionOption
        {
            SearchingBug,
            ResolvingBug
        }

        //public class TaskGridItem
        //{
        //    public string TaskName { get; set; }
        //    public TaskActionOption TaskAction { get; set; }
        //    public string TaskDescription { get; set; }
        //}

        public SessionInputFormSimple(string solutionName)
        {
            InitializeComponent();

            SessionInputService = new SessionInputService(new RepositoryLog(), solutionName);

            //listData.Add(new TaskGridItem { TaskName = "Task 1", TaskAction = TaskActionOption.SearchingBug, TaskDescription = "Task 1 Description" });

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
            //SessionInputModel.Developer = txtDeveloper.Text;

            //SessionInputService.PersistInputDataState(SessionInputModel);

            //Close();
        }
    }
}

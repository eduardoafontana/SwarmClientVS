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
        private SessionListBoxItemModel NewProject;
        private SessionListBoxItemModel NewTask;
        private SessionInputModel SessionInputModel;
        private bool CEElstProject = false;
        private bool CEEtxtProjectTitle = false;
        private bool CEEtxtProjectDescription = false;
        private bool CEElstTask = false;
        private bool CEEtxtTaskTitle = false;
        private bool CEEtxtTaskDescription = false;
        //CEE - can execute event - flag to blog events triggers by backend, allowing only trigger by user

        public SessionInputFormSimple(string solutionName)
        {
            InitializeComponent();

            //SessionInputService = new SessionInputService(new RepositoryLog(), solutionName);

            //LoadInputData();
        }

        //private void LoadInputData()
        //{
        //    DisableEvents();

        //    SessionInputModel = SessionInputService.GetInputDataState();

        //    //lstProject.DataSource = SessionInputModel.Project;
        //    //lstProject.DisplayMember = "Name";
        //    //lstTask.ClearSelected();
        //    //lstProject.SelectedItem = SessionInputModel.SelectedProject;

        //    //txtProjectTitle.Text = SessionInputModel.SelectedProject.Name;
        //    //txtProjectDescription.Text = SessionInputModel.SelectedProject.Description;

        //    //lstTask.DataSource = SessionInputModel.SelectedProject.Task;
        //    //lstTask.DisplayMember = "Name";
        //    //lstTask.ClearSelected();
        //    //lstTask.SelectedItem = SessionInputModel.SelectedTask;

        //    //txtTaskTitle.Text = SessionInputModel.SelectedTask.Name;
        //    //txtTaskDescription.Text = SessionInputModel.SelectedTask.Description;

        //    //txtDeveloper.Text = SessionInputModel.Developer;

        //    EnableEvents();
        //}

        //private void UpdateProjectListBox()
        //{
        //    DisableEvents();

        //    //lstProject.DataSource = null;
        //    //lstProject.DataSource = SessionInputModel.Project;
        //    //lstProject.DisplayMember = "Name";
        //    //lstProject.ClearSelected();
        //    //lstProject.SelectedItem = SessionInputModel.SelectedProject;

        //    EnableEvents();
        //}

        //private void ChangeSelectedProject(bool getLastTask = true)
        //{
        //    DisableEvents();

        //    //txtProjectTitle.Text = SessionInputModel.SelectedProject.Name;
        //    //txtProjectDescription.Text = SessionInputModel.SelectedProject.Description;

        //    if(getLastTask)
        //        SessionInputModel.SelectedTask = SessionInputModel.SelectedProject.Task.LastOrDefault() ?? new SessionListBoxItemModel { };

        //    //lstTask.DataSource = null;
        //    //lstTask.DataSource = SessionInputModel.SelectedProject.Task;
        //    //lstTask.DisplayMember = "Name";
        //    //lstTask.ClearSelected();
        //    //lstTask.SelectedItem = SessionInputModel.SelectedTask;

        //    ChangeSelectedTask();

        //    EnableEvents();
        //}

        //private void ChangeSelectedTask()
        //{
        //    DisableEvents();

        //    //txtTaskTitle.Text = SessionInputModel.SelectedTask.Name;
        //    //txtTaskDescription.Text = SessionInputModel.SelectedTask.Description;

        //    EnableEvents();
        //}

        //private void EnableEvents()
        //{
        //    CEElstProject = true;
        //    CEEtxtProjectTitle = true;
        //    CEEtxtProjectDescription = true;
        //    CEElstTask = true;
        //    CEEtxtTaskTitle = true;
        //    CEEtxtTaskDescription = true;
        //}

        //private void DisableEvents()
        //{
        //    CEElstProject = false;
        //    CEEtxtProjectTitle = false;
        //    CEEtxtProjectDescription = false;
        //    CEElstTask = false;
        //    CEEtxtTaskTitle = false;
        //    CEEtxtTaskDescription = false;
        //}

        private void lstProject_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (CEElstProject == false)
            //    return;

            //if (lstProject.SelectedItem == null)
            //    return;

            //SessionInputModel.SelectedProject = (SessionListBoxItemModel)lstProject.SelectedItem;

            //if (NewTask != null)
            //{
            //    SessionListBoxItemModel existentProjectItem = SessionInputModel.Project.FirstOrDefault(p => p.Name.Equals(txtProjectTitle.Text));

            //    if (existentProjectItem != null)
            //        existentProjectItem.Task.Remove(NewTask);

            //    NewTask = null;
            //}

            //if (NewProject != null && NewProject != SessionInputModel.SelectedProject)
            //{
            //    SessionInputModel.Project.Remove(NewProject);
            //    NewProject = null;
            //}

            //UpdateProjectListBox();
            //ChangeSelectedProject();
        }

        private void lstTask_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (CEElstTask == false)
            //    return;

            //if (lstTask.SelectedItem == null)
            //    return;

            //SessionInputModel.SelectedTask = (SessionListBoxItemModel)lstTask.SelectedItem;

            //if(NewTask != null && NewTask != SessionInputModel.SelectedTask)
            //{
            //    SessionListBoxItemModel existentProjectItem = SessionInputModel.Project.FirstOrDefault(p => p.Name.Equals(txtProjectTitle.Text));

            //    if (existentProjectItem != null)
            //        existentProjectItem.Task.Remove(NewTask);
                
            //    NewTask = null;
            //}

            //UpdateProjectListBox();
            //ChangeSelectedProject(false);
        }

        private void txtProjectTitle_TextChanged(object sender, EventArgs e)
        {
            //if (CEEtxtProjectTitle == false)
            //    return;

            //SessionListBoxItemModel existentProjectItem = SessionInputModel.Project.FirstOrDefault(p => p.Name.Equals(txtProjectTitle.Text));

            //if (existentProjectItem != null)
            //{
            //    if (existentProjectItem != NewProject)
            //    {
            //        if (NewProject != null)
            //        {
            //            SessionInputModel.Project.Remove(NewProject);
            //            NewProject = null;
            //        }

            //        SessionInputModel.SelectedProject = existentProjectItem;
            //    }
            //}
            //else
            //{
            //    if (NewProject == null)
            //    {
            //        NewProject = new SessionListBoxItemModel { Name = txtProjectTitle.Text };
            //        SessionInputModel.Project.Add(NewProject);
            //        SessionInputModel.SelectedProject = SessionInputModel.Project.Last();
            //    }

            //    NewProject.Name = txtProjectTitle.Text;
            //}

            //UpdateProjectListBox();
            //ChangeSelectedProject();
        }

        private void txtTaskTitle_TextChanged(object sender, EventArgs e)
        {
            //if (CEEtxtTaskTitle == false)
            //    return;

            //SessionListBoxItemModel existentProjectItem = SessionInputModel.Project.FirstOrDefault(p => p.Name.Equals(txtProjectTitle.Text));

            //if (existentProjectItem == null)
            //    return;

            //SessionListBoxItemModel existentTaskItem = existentProjectItem.Task.FirstOrDefault(t => t.Name.Equals(txtTaskTitle.Text));

            //if (existentTaskItem != null)
            //{
            //    if (existentTaskItem != NewTask)
            //    {
            //        if (NewTask != null)
            //        {
            //            existentProjectItem.Task.Remove(NewTask);
            //            NewTask = null;
            //        }

            //        SessionInputModel.SelectedTask = existentTaskItem;
            //    }
            //}
            //else
            //{
            //    if (NewTask == null)
            //    {
            //        NewTask = new SessionListBoxItemModel { Name = txtTaskTitle.Text };
            //        existentProjectItem.Task.Add(NewTask);
            //        SessionInputModel.SelectedTask = existentProjectItem.Task.Last();
            //    }

            //    NewTask.Name = txtTaskTitle.Text;
            //}

            //UpdateProjectListBox();
            //ChangeSelectedProject();
        }

        private void txtProjectDescription_TextChanged(object sender, EventArgs e)
        {
            //if (CEEtxtProjectDescription == false)
            //    return;

            //SessionInputModel.SelectedProject.Description = txtProjectDescription.Text;

            //SessionListBoxItemModel existentProjectItem = SessionInputModel.Project.FirstOrDefault(p => p.Name.Equals(txtProjectTitle.Text));

            //if (existentProjectItem == null)
            //    return;

            //existentProjectItem.Description = txtProjectDescription.Text;
        }

        private void txtTaskDescription_TextChanged(object sender, EventArgs e)
        {
            //if (CEEtxtTaskDescription == false)
            //    return;

            //SessionInputModel.SelectedTask.Description = txtTaskDescription.Text;

            //SessionListBoxItemModel existentProjectItem = SessionInputModel.Project.FirstOrDefault(p => p.Name.Equals(txtProjectTitle.Text));

            //if (existentProjectItem == null)
            //    return;

            //SessionListBoxItemModel existentTaskItem = existentProjectItem.Task.FirstOrDefault(p => p.Name.Equals(txtTaskTitle.Text));

            //if (existentTaskItem == null)
            //    return;

            //existentTaskItem.Description = txtTaskDescription.Text;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //SessionInputModel.Developer = txtDeveloper.Text;

            //SessionInputService.PersistInputDataState(SessionInputModel);

            //Close();
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



                ////Put some logic here, for example to remove row from your binding list.
                //yourBindingList.RemoveAt(e.RowIndex);
                //bindingSource.RemoveCurrent();

            }
        }
    }
}

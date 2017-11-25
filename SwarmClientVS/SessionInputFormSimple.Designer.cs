namespace SwarmClientVS
{
    partial class SessionInputFormSimple
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.gbTask = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lblProject = new System.Windows.Forms.Label();
            this.TaskName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaskAction = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.TaskDescripion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaskDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.gbTask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(497, 334);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 364);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(584, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // gbTask
            // 
            this.gbTask.Controls.Add(this.dataGridView1);
            this.gbTask.Location = new System.Drawing.Point(12, 49);
            this.gbTask.Name = "gbTask";
            this.gbTask.Size = new System.Drawing.Size(559, 279);
            this.gbTask.TabIndex = 5;
            this.gbTask.TabStop = false;
            this.gbTask.Text = "Task";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TaskName,
            this.TaskAction,
            this.TaskDescripion,
            this.TaskDelete});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(7, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 15;
            this.dataGridView1.Size = new System.Drawing.Size(546, 250);
            this.dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Project: ";
            // 
            // lblProject
            // 
            this.lblProject.AutoSize = true;
            this.lblProject.Location = new System.Drawing.Point(72, 20);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(10, 13);
            this.lblProject.TabIndex = 7;
            this.lblProject.Text = "-";
            // 
            // TaskName
            // 
            this.TaskName.HeaderText = "Task Name";
            this.TaskName.MaxInputLength = 75;
            this.TaskName.Name = "TaskName";
            this.TaskName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TaskName.Width = 150;
            // 
            // TaskAction
            // 
            this.TaskAction.HeaderText = "Task Action";
            this.TaskAction.Items.AddRange(new object[] {
            "Searching Bug",
            "Resolving Bug"});
            this.TaskAction.Name = "TaskAction";
            // 
            // TaskDescripion
            // 
            this.TaskDescripion.HeaderText = "Task Description";
            this.TaskDescripion.MaxInputLength = 100;
            this.TaskDescripion.Name = "TaskDescripion";
            this.TaskDescripion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TaskDescripion.Width = 225;
            // 
            // TaskDelete
            // 
            this.TaskDelete.HeaderText = "Delete";
            this.TaskDelete.Name = "TaskDelete";
            this.TaskDelete.Text = "X";
            this.TaskDelete.Width = 25;
            // 
            // SessionInputFormSimple
            // 
            this.AcceptButton = this.btnStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 386);
            this.Controls.Add(this.lblProject);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbTask);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SessionInputFormSimple";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visual Studio Swarm Debugger Monitor Client";
            this.TopMost = true;
            this.gbTask.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox gbTask;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblProject;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskName;
        private System.Windows.Forms.DataGridViewComboBoxColumn TaskAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskDescripion;
        private System.Windows.Forms.DataGridViewButtonColumn TaskDelete;
    }
}
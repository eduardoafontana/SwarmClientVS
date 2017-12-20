namespace SwarmClientVS
{
    partial class SessionInputForm
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
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.gbTask = new System.Windows.Forms.GroupBox();
            this.dgTask = new System.Windows.Forms.DataGridView();
            this.TaskDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.lblProject = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDeveloperNickName = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            this.gbTask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTask)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(497, 339);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "OK";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 369);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(584, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(206, 17);
            this.toolStripStatusLabel1.Text = "The last task on list is your active task.";
            // 
            // gbTask
            // 
            this.gbTask.Controls.Add(this.dgTask);
            this.gbTask.Location = new System.Drawing.Point(12, 54);
            this.gbTask.Name = "gbTask";
            this.gbTask.Size = new System.Drawing.Size(559, 279);
            this.gbTask.TabIndex = 5;
            this.gbTask.TabStop = false;
            this.gbTask.Text = "Task";
            // 
            // dgTask
            // 
            this.dgTask.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTask.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TaskDelete});
            this.dgTask.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgTask.Location = new System.Drawing.Point(7, 20);
            this.dgTask.Name = "dgTask";
            this.dgTask.RowHeadersWidth = 15;
            this.dgTask.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgTask.Size = new System.Drawing.Size(546, 250);
            this.dgTask.TabIndex = 0;
            this.dgTask.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgTask_CellClick);
            this.dgTask.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgTask_CellValueChanged);
            this.dgTask.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgTask_DataBindingComplete);
            // 
            // TaskDelete
            // 
            this.TaskDelete.HeaderText = "Delete";
            this.TaskDelete.Name = "TaskDelete";
            this.TaskDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TaskDelete.Text = "X";
            this.TaskDelete.Width = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Project: ";
            // 
            // lblProject
            // 
            this.lblProject.AutoSize = true;
            this.lblProject.Location = new System.Drawing.Point(132, 13);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(10, 13);
            this.lblProject.TabIndex = 7;
            this.lblProject.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Developer Nickname:";
            // 
            // txtDeveloperNickName
            // 
            this.txtDeveloperNickName.Location = new System.Drawing.Point(135, 32);
            this.txtDeveloperNickName.MaxLength = 150;
            this.txtDeveloperNickName.Name = "txtDeveloperNickName";
            this.txtDeveloperNickName.Size = new System.Drawing.Size(430, 20);
            this.txtDeveloperNickName.TabIndex = 9;
            this.txtDeveloperNickName.TextChanged += new System.EventHandler(this.txtDeveloperNickName_TextChanged);
            // 
            // SessionInputForm
            // 
            this.AcceptButton = this.btnStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 391);
            this.ControlBox = false;
            this.Controls.Add(this.txtDeveloperNickName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblProject);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbTask);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SessionInputForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visual Studio Swarm Debugger Monitor Client ";
            this.TopMost = true;
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gbTask.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgTask)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox gbTask;
        private System.Windows.Forms.DataGridView dgTask;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblProject;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.DataGridViewButtonColumn TaskDelete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDeveloperNickName;
    }
}
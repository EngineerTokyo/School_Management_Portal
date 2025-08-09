namespace StudentRecordApp
{
    partial class PrincipalForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.DataGridView dgvTeachers;
        private System.Windows.Forms.Button btnAddTeacher;
        private System.Windows.Forms.Button btnEditTeacher;
        private System.Windows.Forms.Button btnDeleteTeacher;
        private System.Windows.Forms.Button btnAssignSubject;
        private System.Windows.Forms.Button btnRefresh;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblWelcome = new System.Windows.Forms.Label();
            this.dgvTeachers = new System.Windows.Forms.DataGridView();
            this.btnAddTeacher = new System.Windows.Forms.Button();
            this.btnEditTeacher = new System.Windows.Forms.Button();
            this.btnDeleteTeacher = new System.Windows.Forms.Button();
            this.btnAssignSubject = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTeachers)).BeginInit();
            this.SuspendLayout();
            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(12, 9);
            this.lblWelcome.Text = "Welcome, Principal";
            // dgvTeachers
            this.dgvTeachers.Location = new System.Drawing.Point(12, 35);
            this.dgvTeachers.Size = new System.Drawing.Size(600, 250);
            // btnAddTeacher
            this.btnAddTeacher.Location = new System.Drawing.Point(12, 300);
            this.btnAddTeacher.Text = "Add Teacher";
            this.btnAddTeacher.Click += new System.EventHandler(this.btnAddTeacher_Click);
            // btnEditTeacher
            this.btnEditTeacher.Location = new System.Drawing.Point(110, 300);
            this.btnEditTeacher.Text = "Edit Teacher";
            this.btnEditTeacher.Click += new System.EventHandler(this.btnEditTeacher_Click);
            // btnDeleteTeacher
            this.btnDeleteTeacher.Location = new System.Drawing.Point(208, 300);
            this.btnDeleteTeacher.Text = "Delete Teacher";
            this.btnDeleteTeacher.Click += new System.EventHandler(this.btnDeleteTeacher_Click);
            // btnAssignSubject
            this.btnAssignSubject.Location = new System.Drawing.Point(306, 300);
            this.btnAssignSubject.Text = "Assign Subject";
            this.btnAssignSubject.Click += new System.EventHandler(this.btnAssignSubject_Click);
            // btnRefresh
            this.btnRefresh.Location = new System.Drawing.Point(420, 300);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // PrincipalForm
            this.ClientSize = new System.Drawing.Size(640, 350);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.dgvTeachers);
            this.Controls.Add(this.btnAddTeacher);
            this.Controls.Add(this.btnEditTeacher);
            this.Controls.Add(this.btnDeleteTeacher);
            this.Controls.Add(this.btnAssignSubject);
            this.Controls.Add(this.btnRefresh);
            this.Text = "Principal Dashboard";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTeachers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

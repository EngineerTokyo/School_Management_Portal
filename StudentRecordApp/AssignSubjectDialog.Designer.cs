namespace StudentRecordApp
{
    partial class AssignSubjectDialog
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTeacher;
        private System.Windows.Forms.Label lblSubjects;
        private System.Windows.Forms.CheckedListBox clbSubjects;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTeacher = new System.Windows.Forms.Label();
            this.lblSubjects = new System.Windows.Forms.Label();
            this.clbSubjects = new System.Windows.Forms.CheckedListBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            // layout
            this.lblTeacher.AutoSize = true; this.lblTeacher.Location = new System.Drawing.Point(12, 12); this.lblTeacher.Text = "Assigning subjects";
            this.lblSubjects.AutoSize = true; this.lblSubjects.Location = new System.Drawing.Point(12, 40); this.lblSubjects.Text = "Subjects:";
            this.clbSubjects.Location = new System.Drawing.Point(12, 60); this.clbSubjects.Size = new System.Drawing.Size(260, 120);
            this.btnSave.Location = new System.Drawing.Point(30, 190); this.btnSave.Text = "Save"; this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnCancel.Location = new System.Drawing.Point(150, 190); this.btnCancel.Text = "Cancel"; this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.ClientSize = new System.Drawing.Size(300, 230);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.lblTeacher, this.lblSubjects, this.clbSubjects, this.btnSave, this.btnCancel });
            this.Text = "Assign Subjects";
        }
    }
}

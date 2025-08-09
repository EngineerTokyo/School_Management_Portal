namespace StudentRecordApp
{
    partial class TeacherForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.ComboBox cmbSubjects;
        private System.Windows.Forms.DataGridView dgvStudents;
        private System.Windows.Forms.Button btnLoadStudents;
        private System.Windows.Forms.Button btnRecordAttendance;
        private System.Windows.Forms.Button btnEnterMark;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.TextBox txtMark;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblMark;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblWelcome = new System.Windows.Forms.Label();
            this.cmbSubjects = new System.Windows.Forms.ComboBox();
            this.dgvStudents = new System.Windows.Forms.DataGridView();
            this.btnLoadStudents = new System.Windows.Forms.Button();
            this.btnRecordAttendance = new System.Windows.Forms.Button();
            this.btnEnterMark = new System.Windows.Forms.Button();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.txtMark = new System.Windows.Forms.TextBox();
            this.lblSubject = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblMark = new System.Windows.Forms.Label();

            // layout
            this.lblWelcome.AutoSize = true; this.lblWelcome.Location = new System.Drawing.Point(12, 9); this.lblWelcome.Text = "Welcome, Teacher";
            this.lblSubject.AutoSize = true; this.lblSubject.Location = new System.Drawing.Point(12, 35); this.lblSubject.Text = "Subject:";
            this.cmbSubjects.Location = new System.Drawing.Point(70, 32); this.cmbSubjects.Size = new System.Drawing.Size(200, 21);
            this.btnLoadStudents.Location = new System.Drawing.Point(290, 30); this.btnLoadStudents.Text = "Load Students"; this.btnLoadStudents.Click += new System.EventHandler(this.btnLoadStudents_Click);
            this.dgvStudents.Location = new System.Drawing.Point(12, 65); this.dgvStudents.Size = new System.Drawing.Size(500, 250);
            this.lblDate.AutoSize = true; this.lblDate.Location = new System.Drawing.Point(12, 330); this.lblDate.Text = "Date:";
            this.dtpDate.Location = new System.Drawing.Point(50, 326); this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.btnRecordAttendance.Location = new System.Drawing.Point(200, 326); this.btnRecordAttendance.Text = "Mark Present"; this.btnRecordAttendance.Click += new System.EventHandler(this.btnRecordAttendance_Click);
            this.lblMark.AutoSize = true; this.lblMark.Location = new System.Drawing.Point(12, 360); this.lblMark.Text = "Mark:";
            this.txtMark.Location = new System.Drawing.Point(50, 357); this.txtMark.Size = new System.Drawing.Size(80, 20);
            this.btnEnterMark.Location = new System.Drawing.Point(150, 355); this.btnEnterMark.Text = "Save Mark"; this.btnEnterMark.Click += new System.EventHandler(this.btnEnterMark_Click);

            this.ClientSize = new System.Drawing.Size(640, 420);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.lblWelcome, this.lblSubject, this.cmbSubjects, this.btnLoadStudents, this.dgvStudents, this.lblDate, this.dtpDate, this.btnRecordAttendance, this.lblMark, this.txtMark, this.btnEnterMark });
            this.Text = "Teacher Dashboard";
        }
    }
}

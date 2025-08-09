namespace StudentRecordApp
{
    partial class StudentForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Button btnSaveProfile;
        private System.Windows.Forms.DataGridView dgvMarks;
        private System.Windows.Forms.DataGridView dgvAttendance;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.Label lblName, lblEmail, lblPhone, lblAddress, lblMarks, lblAttendance;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label(); this.txtName = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label(); this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label(); this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label(); this.txtAddress = new System.Windows.Forms.TextBox();
            this.btnSaveProfile = new System.Windows.Forms.Button();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.lblMarks = new System.Windows.Forms.Label();
            this.dgvMarks = new System.Windows.Forms.DataGridView();
            this.lblAttendance = new System.Windows.Forms.Label();
            this.dgvAttendance = new System.Windows.Forms.DataGridView();

            // layout
            this.lblWelcome.AutoSize = true; this.lblWelcome.Location = new System.Drawing.Point(12, 9); this.lblWelcome.Text = "Welcome, Student";
            this.lblName.AutoSize = true; this.lblName.Location = new System.Drawing.Point(12, 35); this.lblName.Text = "Name:";
            this.txtName.Location = new System.Drawing.Point(80, 32); this.txtName.Size = new System.Drawing.Size(220, 20);
            this.lblEmail.AutoSize = true; this.lblEmail.Location = new System.Drawing.Point(12, 65); this.lblEmail.Text = "Email:";
            this.txtEmail.Location = new System.Drawing.Point(80, 62); this.txtEmail.Size = new System.Drawing.Size(220, 20);
            this.lblPhone.AutoSize = true; this.lblPhone.Location = new System.Drawing.Point(12, 95); this.lblPhone.Text = "Phone:";
            this.txtPhone.Location = new System.Drawing.Point(80, 92); this.txtPhone.Size = new System.Drawing.Size(220, 20);
            this.lblAddress.AutoSize = true; this.lblAddress.Location = new System.Drawing.Point(12, 125); this.lblAddress.Text = "Address:";
            this.txtAddress.Location = new System.Drawing.Point(80, 122); this.txtAddress.Size = new System.Drawing.Size(220, 20);

            this.btnSaveProfile.Location = new System.Drawing.Point(80, 152); this.btnSaveProfile.Text = "Save Profile"; this.btnSaveProfile.Click += new System.EventHandler(this.btnSaveProfile_Click);
            this.btnLoadData.Location = new System.Drawing.Point(180, 152); this.btnLoadData.Text = "Load Marks & Attendance"; this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);

            this.lblMarks.AutoSize = true; this.lblMarks.Location = new System.Drawing.Point(12, 190); this.lblMarks.Text = "Marks:";
            this.dgvMarks.Location = new System.Drawing.Point(12, 210); this.dgvMarks.Size = new System.Drawing.Size(560, 140);

            this.lblAttendance.AutoSize = true; this.lblAttendance.Location = new System.Drawing.Point(12, 360); this.lblAttendance.Text = "Attendance:";
            this.dgvAttendance.Location = new System.Drawing.Point(12, 380); this.dgvAttendance.Size = new System.Drawing.Size(560, 140);

            this.ClientSize = new System.Drawing.Size(600, 540);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblWelcome, this.lblName, this.txtName, this.lblEmail, this.txtEmail, this.lblPhone, this.txtPhone, this.lblAddress, this.txtAddress,
                this.btnSaveProfile, this.btnLoadData, this.lblMarks, this.dgvMarks, this.lblAttendance, this.dgvAttendance
            });
            this.Text = "Student Dashboard";
        }
    }
}

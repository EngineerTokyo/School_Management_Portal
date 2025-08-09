namespace StudentRecordApp
{
    partial class StudentLoginDialog
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblDob;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.DateTimePicker dtpDob;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblId = new System.Windows.Forms.Label();
            this.lblDob = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.dtpDob = new System.Windows.Forms.DateTimePicker();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // lblId
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(12, 15);
            this.lblId.Text = "Student ID:";
            // txtId
            this.txtId.Location = new System.Drawing.Point(90, 12);
            this.txtId.Size = new System.Drawing.Size(150, 20);
            // lblDob
            this.lblDob.AutoSize = true;
            this.lblDob.Location = new System.Drawing.Point(12, 45);
            this.lblDob.Text = "Date of Birth:";
            // dtpDob
            this.dtpDob.Location = new System.Drawing.Point(90, 41);
            this.dtpDob.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            // btnOk
            this.btnOk.Location = new System.Drawing.Point(45, 80);
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(135, 80);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // Dialog
            this.ClientSize = new System.Drawing.Size(260, 120);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.lblDob);
            this.Controls.Add(this.dtpDob);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Text = "Student Login";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

namespace StudentRecordApp
{
    partial class AddEditTeacherDialog
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            // layout simple
            this.lblUsername.AutoSize = true; this.lblUsername.Location = new System.Drawing.Point(12, 15); this.lblUsername.Text = "Username:";
            this.txtUsername.Location = new System.Drawing.Point(100, 12); this.txtUsername.Size = new System.Drawing.Size(180, 20);
            this.lblFullName.AutoSize = true; this.lblFullName.Location = new System.Drawing.Point(12, 45); this.lblFullName.Text = "Full Name:";
            this.txtFullName.Location = new System.Drawing.Point(100, 42); this.txtFullName.Size = new System.Drawing.Size(180, 20);
            this.lblEmail.AutoSize = true; this.lblEmail.Location = new System.Drawing.Point(12, 75); this.lblEmail.Text = "Email:";
            this.txtEmail.Location = new System.Drawing.Point(100, 72); this.txtEmail.Size = new System.Drawing.Size(180, 20);
            this.lblPhone.AutoSize = true; this.lblPhone.Location = new System.Drawing.Point(12, 105); this.lblPhone.Text = "Phone:";
            this.txtPhone.Location = new System.Drawing.Point(100, 102); this.txtPhone.Size = new System.Drawing.Size(180, 20);
            this.lblPassword.AutoSize = true; this.lblPassword.Location = new System.Drawing.Point(12, 135); this.lblPassword.Text = "Password:";
            this.txtPassword.Location = new System.Drawing.Point(100, 132); this.txtPassword.Size = new System.Drawing.Size(180, 20);
            this.txtPassword.UseSystemPasswordChar = true;
            this.btnOk.Location = new System.Drawing.Point(60, 170); this.btnOk.Text = "OK"; this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            this.btnCancel.Location = new System.Drawing.Point(160, 170); this.btnCancel.Text = "Cancel"; this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.ClientSize = new System.Drawing.Size(300, 210);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblUsername, this.txtUsername, this.lblFullName, this.txtFullName, this.lblEmail, this.txtEmail,
                this.lblPhone, this.txtPhone, this.lblPassword, this.txtPassword, this.btnOk, this.btnCancel
            });
            this.Text = "Add/Edit Teacher";
        }
    }
}

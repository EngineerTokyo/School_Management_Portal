namespace StudentRecordApp
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnStudentLogin;
        private System.Windows.Forms.Label lblInfo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblUser = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnStudentLogin = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // lblUser
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(22, 22);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(58, 13);
            this.lblUser.Text = "Username:";
            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(22, 52);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.Text = "Password:";
            // txtUsername
            this.txtUsername.Location = new System.Drawing.Point(100, 19);
            this.txtUsername.Size = new System.Drawing.Size(180, 20);
            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(100, 49);
            this.txtPassword.Size = new System.Drawing.Size(180, 20);
            this.txtPassword.UseSystemPasswordChar = true;
            // btnLogin
            this.btnLogin.Location = new System.Drawing.Point(100, 80);
            this.btnLogin.Size = new System.Drawing.Size(80, 25);
            this.btnLogin.Text = "Login";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // btnStudentLogin
            this.btnStudentLogin.Location = new System.Drawing.Point(200, 80);
            this.btnStudentLogin.Size = new System.Drawing.Size(80, 25);
            this.btnStudentLogin.Text = "Student Login";
            this.btnStudentLogin.Click += new System.EventHandler(this.btnStudentLogin_Click);
            // lblInfo
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(22, 115);
            this.lblInfo.Size = new System.Drawing.Size(320, 30);
            this.lblInfo.Text = "Use 'admin' / 'admin123' to login as Principal. \nTeachers: tahira/teacher123, ali/teacher456. Students login by ID + DOB.";
            // LoginForm
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnStudentLogin);
            this.Controls.Add(this.lblInfo);
            this.Name = "LoginForm";
            this.Text = "Login - School Management";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

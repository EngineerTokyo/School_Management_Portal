using System;
using System.Windows.Forms;

namespace StudentRecordApp
{
    public partial class AddEditTeacherDialog : Form
    {
        public bool IsEdit { get; set; } = false;
        public int TeacherId { get; set; } = 0;
        public string Username { get => txtUsername.Text.Trim(); set => txtUsername.Text = value; }
        public string FullName { get => txtFullName.Text.Trim(); set => txtFullName.Text = value; }
        public string Email { get => txtEmail.Text.Trim(); set => txtEmail.Text = value; }
        public string Phone { get => txtPhone.Text.Trim(); set => txtPhone.Text = value; }
        public string Password { get => txtPassword.Text; set => txtPassword.Text = value; }

        public AddEditTeacherDialog()
        {
            InitializeComponent();
            if (IsEdit) txtUsername.Enabled = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!IsEdit && string.IsNullOrWhiteSpace(Username))
            {
                MessageBox.Show("Enter username.");
                return;
            }
            if (string.IsNullOrWhiteSpace(FullName))
            {
                MessageBox.Show("Enter full name.");
                return;
            }
            if (!IsEdit && string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Enter password for new teacher.");
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();
    }
}

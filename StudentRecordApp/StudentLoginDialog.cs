using System;
using System.Windows.Forms;

namespace StudentRecordApp
{
    public partial class StudentLoginDialog : Form
    {
        public int StudentId { get; private set; }
        public DateTime DOB { get; private set; }

        public StudentLoginDialog()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text.Trim(), out int id))
            {
                MessageBox.Show("Enter valid student ID number.");
                return;
            }
            StudentId = id;
            DOB = dtpDob.Value.Date;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

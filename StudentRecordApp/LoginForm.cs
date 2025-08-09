using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentRecordApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        // Principal/Teacher login by username + password
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Enter username and password.");
                return;
            }

            // Quick fallback: allow hardcoded admin for testing if DB not seeded
            if (username.Equals("admin", StringComparison.OrdinalIgnoreCase) && password == "admin123")
            {
                var form = new PrincipalForm();
                form.LoggedInPrincipalName = "Administrator";
                this.Hide();
                form.ShowDialog();
                this.Show();
                return;
            }

            // 1) check Principals table
            DataTable dtP = DbHelper.GetDataTable(
                "SELECT PrincipalId, Username, PasswordHash, PasswordSalt, FullName FROM Principals WHERE Username=@u",
                new SqlParameter("@u", username)
            );

            if (dtP.Rows.Count == 1)
            {
                var row = dtP.Rows[0];

                try
                {
                    byte[] salt = CryptoHelper.ToBytes(row["PasswordSalt"]);
                    byte[] hash = CryptoHelper.ToBytes(row["PasswordHash"]);
                    byte[] calc = CryptoHelper.ComputeHash(salt, password);

                    if (CryptoHelper.CompareBytes(hash, calc))
                    {
                        // principal login success
                        var form = new PrincipalForm();
                        form.LoggedInPrincipalName = row["FullName"].ToString();
                        this.Hide();
                        form.ShowDialog();
                        this.Show();
                        return;
                    }
                }
                catch
                {
                    // If hash/salt conversion fails, fall back to plain text compare
                    if (row["PasswordHash"].ToString() == password)
                    {
                        var form = new PrincipalForm();
                        form.LoggedInPrincipalName = row["FullName"].ToString();
                        this.Hide();
                        form.ShowDialog();
                        this.Show();
                        return;
                    }
                }

                MessageBox.Show("Invalid credentials.");
                return;
            }

            // 2) check Teachers table
            DataTable dtT = DbHelper.GetDataTable(
                "SELECT TeacherId, Username, PasswordHash, PasswordSalt, FullName FROM Teachers WHERE Username=@u",
                new SqlParameter("@u", username)
            );

            if (dtT.Rows.Count == 1)
            {
                var row = dtT.Rows[0];

                try
                {
                    byte[] salt = CryptoHelper.ToBytes(row["PasswordSalt"]);
                    byte[] hash = CryptoHelper.ToBytes(row["PasswordHash"]);
                    byte[] calc = CryptoHelper.ComputeHash(salt, password);

                    if (CryptoHelper.CompareBytes(hash, calc))
                    {
                        // teacher login
                        int teacherId = Convert.ToInt32(row["TeacherId"]);
                        var form = new TeacherForm(teacherId);
                        form.LoggedInTeacherName = row["FullName"].ToString();
                        this.Hide();
                        form.ShowDialog();
                        this.Show();
                        return;
                    }
                }
                catch
                {
                    if (row["PasswordHash"].ToString() == password)
                    {
                        int teacherId = Convert.ToInt32(row["TeacherId"]);
                        var form = new TeacherForm(teacherId);
                        form.LoggedInTeacherName = row["FullName"].ToString();
                        this.Hide();
                        form.ShowDialog();
                        this.Show();
                        return;
                    }
                }

                MessageBox.Show("Invalid credentials.");
                return;
            }

            MessageBox.Show("User not found. Use teacher or principal credentials.");
        }

        // Student login: prompt for StudentID and DOB
        private void btnStudentLogin_Click(object sender, EventArgs e)
        {
            using (var dlg = new StudentLoginDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    int studentId = dlg.StudentId;
                    DateTime dob = dlg.DOB;

                    DataTable dt = DbHelper.GetDataTable(
                        "SELECT StudentId, FullName FROM Students WHERE StudentId=@id AND DOB=@dob",
                        new SqlParameter("@id", studentId),
                        new SqlParameter("@dob", dob.Date)
                    );

                    if (dt.Rows.Count == 1)
                    {
                        var form = new StudentForm(studentId);
                        form.LoggedInStudentName = dt.Rows[0]["FullName"].ToString();
                        this.Hide();
                        form.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        MessageBox.Show("Student not found or DOB mismatch.");
                    }
                }
            }
        }
    }
}

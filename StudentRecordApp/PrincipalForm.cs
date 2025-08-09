using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentRecordApp
{
    public partial class PrincipalForm : Form
    {
        public string LoggedInPrincipalName { get; set; }

        public PrincipalForm()
        {
            InitializeComponent();
            LoadTeachers();
        }

        private void PrincipalForm_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = "Welcome, " + (LoggedInPrincipalName ?? "Principal");
        }

        // Load teachers into grid
        private void LoadTeachers()
        {
            string sql = "SELECT TeacherId, Username, FullName, Email, Phone, CreatedAt FROM Teachers ORDER BY TeacherId";
            dgvTeachers.DataSource = DbHelper.GetDataTable(sql);
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadTeachers();

        private void btnAddTeacher_Click(object sender, EventArgs e)
        {
            using (var dlg = new AddEditTeacherDialog())
            {
                dlg.IsEdit = false;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // call stored procedure sp_AddTeacher
                    try
                    {
                        var p = new SqlParameter[]
                        {
                            new SqlParameter("@Username", dlg.Username),
                            new SqlParameter("@FullName", dlg.FullName),
                            new SqlParameter("@Email", dlg.Email),
                            new SqlParameter("@Phone", dlg.Phone),
                            new SqlParameter("@Password", dlg.Password)
                        };
                        DbHelper.ExecuteNonQuery("EXEC sp_AddTeacher @Username, @FullName, @Email, @Phone, @Password", p);
                        MessageBox.Show("Teacher added.");
                        LoadTeachers();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void btnEditTeacher_Click(object sender, EventArgs e)
        {
            if (dgvTeachers.CurrentRow == null) { MessageBox.Show("Select a teacher."); return; }
            int id = Convert.ToInt32(dgvTeachers.CurrentRow.Cells["TeacherId"].Value);
            string username = dgvTeachers.CurrentRow.Cells["Username"].Value.ToString();
            string fullname = dgvTeachers.CurrentRow.Cells["FullName"].Value.ToString();
            string email = dgvTeachers.CurrentRow.Cells["Email"].Value.ToString();
            string phone = dgvTeachers.CurrentRow.Cells["Phone"].Value.ToString();

            using (var dlg = new AddEditTeacherDialog())
            {
                dlg.IsEdit = true;
                dlg.TeacherId = id;
                dlg.Username = username;
                dlg.FullName = fullname;
                dlg.Email = email;
                dlg.Phone = phone;
                // For update we won't change password unless provided
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // update teacher profile (username unique check skipped)
                    string sql = "UPDATE Teachers SET FullName=@FullName, Email=@Email, Phone=@Phone WHERE TeacherId=@Id";
                    DbHelper.ExecuteNonQuery(sql,
                        new SqlParameter("@FullName", dlg.FullName),
                        new SqlParameter("@Email", dlg.Email),
                        new SqlParameter("@Phone", dlg.Phone),
                        new SqlParameter("@Id", dlg.TeacherId)
                    );
                    // If password provided, update salt+hash using SQL method: generate salt in app then update
                    if (!string.IsNullOrEmpty(dlg.Password))
                    {
                        // create salt and hash same as SQL: salt + password, SHA256
                        byte[] salt = new byte[16];
                        using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
                            rng.GetBytes(salt);
                        byte[] hash = CryptoHelper.ComputeHash(salt, dlg.Password);
                        string sql2 = "UPDATE Teachers SET PasswordHash=@hash, PasswordSalt=@salt WHERE TeacherId=@Id";
                        DbHelper.ExecuteNonQuery(sql2,
                            new SqlParameter("@hash", hash),
                            new SqlParameter("@salt", salt),
                            new SqlParameter("@Id", dlg.TeacherId)
                        );
                    }

                    MessageBox.Show("Teacher updated.");
                    LoadTeachers();
                }
            }
        }

        private void btnDeleteTeacher_Click(object sender, EventArgs e)
        {
            if (dgvTeachers.CurrentRow == null) { MessageBox.Show("Select a teacher."); return; }
            int id = Convert.ToInt32(dgvTeachers.CurrentRow.Cells["TeacherId"].Value);
            if (MessageBox.Show("Delete this teacher?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DbHelper.ExecuteNonQuery("DELETE FROM Teachers WHERE TeacherId=@id", new SqlParameter("@id", id));
                LoadTeachers();
            }
        }

        private void btnAssignSubject_Click(object sender, EventArgs e)
        {
            if (dgvTeachers.CurrentRow == null) { MessageBox.Show("Select a teacher."); return; }
            int teacherId = Convert.ToInt32(dgvTeachers.CurrentRow.Cells["TeacherId"].Value);
            using (var dlg = new AssignSubjectDialog(teacherId))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    LoadTeachers();
                }
            }
        }
    }
}

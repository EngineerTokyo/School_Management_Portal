using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentRecordApp
{
    public partial class StudentForm : Form
    {
        private int studentId;
        public string LoggedInStudentName { get; set; }

        public StudentForm(int studentId)
        {
            this.studentId = studentId;
            InitializeComponent();
            lblWelcome.Text = "Welcome, " + (LoggedInStudentName ?? "Student");
            LoadProfile();
            LoadMarks();
            LoadAttendance();
        }

        private void LoadProfile()
        {
            DataTable dt = DbHelper.GetDataTable("SELECT FullName, Email, Phone, Address FROM Students WHERE StudentId=@id",
                new SqlParameter("@id", studentId));
            if (dt.Rows.Count == 1)
            {
                txtName.Text = dt.Rows[0]["FullName"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtPhone.Text = dt.Rows[0]["Phone"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
            }
        }

        private void btnSaveProfile_Click(object sender, EventArgs e)
        {
            DbHelper.ExecuteNonQuery("UPDATE Students SET FullName=@n, Email=@e, Phone=@p, Address=@a WHERE StudentId=@id",
                new SqlParameter("@n", txtName.Text.Trim()),
                new SqlParameter("@e", txtEmail.Text.Trim()),
                new SqlParameter("@p", txtPhone.Text.Trim()),
                new SqlParameter("@a", txtAddress.Text.Trim()),
                new SqlParameter("@id", studentId)
            );
            MessageBox.Show("Profile updated.");
            LoadProfile();
        }

        private void LoadMarks()
        {
            DataTable dt = DbHelper.GetDataTable(@"
                SELECT m.MarkId, s.SubjectName, m.Mark, m.MaxMark, m.Grade, m.ExamDate, t.FullName as Teacher
                FROM Marks m
                INNER JOIN Subjects s ON m.SubjectId = s.SubjectId
                LEFT JOIN Teachers t ON m.TeacherId = t.TeacherId
                WHERE m.StudentId = @id
                ORDER BY m.ExamDate DESC",
                new SqlParameter("@id", studentId)
            );
            dgvMarks.DataSource = dt;
        }

        private void LoadAttendance()
        {
            DataTable dt = DbHelper.GetDataTable(@"
                SELECT a.AttendanceId, s.SubjectName, a.AttDate, a.Status, t.FullName as Teacher
                FROM Attendance a
                INNER JOIN Subjects s ON a.SubjectId = s.SubjectId
                LEFT JOIN Teachers t ON a.TeacherId = t.TeacherId
                WHERE a.StudentId = @id
                ORDER BY a.AttDate DESC",
                new SqlParameter("@id", studentId)
            );
            dgvAttendance.DataSource = dt;
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            LoadMarks();
            LoadAttendance();
            MessageBox.Show("Data refreshed.");
        }
    }
}

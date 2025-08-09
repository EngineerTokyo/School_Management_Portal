using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentRecordApp
{
    public partial class TeacherForm : Form
    {
        private int teacherId;
        public string LoggedInTeacherName { get; set; }

        public TeacherForm(int teacherId)
        {
            this.teacherId = teacherId;
            InitializeComponent();
            lblWelcome.Text = "Welcome, " + (LoggedInTeacherName ?? "Teacher");
            LoadSubjects();
        }

        private void LoadSubjects()
        {
            // get subjects assigned to this teacher
            DataTable dt = DbHelper.GetDataTable(
                "SELECT s.SubjectId, s.SubjectName FROM Subjects s INNER JOIN TeacherSubjects ts ON s.SubjectId = ts.SubjectId WHERE ts.TeacherId=@t",
                new SqlParameter("@t", teacherId)
            );
            cmbSubjects.DataSource = dt;
            cmbSubjects.DisplayMember = "SubjectName";
            cmbSubjects.ValueMember = "SubjectId";
        }

        private void btnLoadStudents_Click(object sender, EventArgs e)
        {
            if (cmbSubjects.SelectedValue == null) { MessageBox.Show("Select subject."); return; }
            int subjectId = Convert.ToInt32(cmbSubjects.SelectedValue);
            // show students (students who have any marks or attendance or all students — we will show all students for simplicity)
            DataTable dt = DbHelper.GetDataTable("SELECT StudentId, FullName, Email, Phone FROM Students ORDER BY FullName");
            dgvStudents.DataSource = dt;
        }

        // Mark selected students present (for simplicity mark the selected row)
        private void btnRecordAttendance_Click(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow == null) { MessageBox.Show("Select student row."); return; }
            if (cmbSubjects.SelectedValue == null) { MessageBox.Show("Select subject."); return; }

            int studentId = Convert.ToInt32(dgvStudents.CurrentRow.Cells["StudentId"].Value);
            int subjectId = Convert.ToInt32(cmbSubjects.SelectedValue);
            DateTime attDate = dtpDate.Value.Date;

            try
            {
                // call stored procedure sp_RecordAttendance
                DbHelper.ExecuteNonQuery("EXEC sp_RecordAttendance @StudentId, @SubjectId, @TeacherId, @AttDate, @Status",
                    new SqlParameter("@StudentId", studentId),
                    new SqlParameter("@SubjectId", subjectId),
                    new SqlParameter("@TeacherId", teacherId),
                    new SqlParameter("@AttDate", attDate),
                    new SqlParameter("@Status", "Present")
                );
                MessageBox.Show("Attendance recorded as Present for selected student.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Enter mark for selected student for selected subject
        private void btnEnterMark_Click(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow == null) { MessageBox.Show("Select student."); return; }
            if (cmbSubjects.SelectedValue == null) { MessageBox.Show("Select subject."); return; }
            if (!int.TryParse(txtMark.Text.Trim(), out int mark)) { MessageBox.Show("Enter valid mark."); return; }

            int studentId = Convert.ToInt32(dgvStudents.CurrentRow.Cells["StudentId"].Value);
            int subjectId = Convert.ToInt32(cmbSubjects.SelectedValue);
            DateTime examDate = dtpDate.Value.Date;

            // insert into Marks
            DbHelper.ExecuteNonQuery(
                "INSERT INTO Marks (StudentId, SubjectId, TeacherId, Mark, MaxMark, ExamDate) VALUES(@sid,@sub,@tid,@m,100,@d)",
                new SqlParameter("@sid", studentId),
                new SqlParameter("@sub", subjectId),
                new SqlParameter("@tid", teacherId),
                new SqlParameter("@m", mark),
                new SqlParameter("@d", examDate)
            );
            MessageBox.Show("Mark saved.");
        }
    }
}

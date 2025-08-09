using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentRecordApp
{
    public partial class AssignSubjectDialog : Form
    {
        private int teacherId;

        public AssignSubjectDialog(int teacherId)
        {
            this.teacherId = teacherId;
            InitializeComponent();
            LoadSubjects();
            LoadAssigned();
        }

        private void LoadSubjects()
        {
            DataTable dt = DbHelper.GetDataTable("SELECT SubjectId, SubjectName FROM Subjects ORDER BY SubjectName");
            clbSubjects.Items.Clear();
            foreach (DataRow r in dt.Rows)
            {
                clbSubjects.Items.Add(new ListItem { Id = Convert.ToInt32(r["SubjectId"]), Name = r["SubjectName"].ToString() });
            }
        }

        private void LoadAssigned()
        {
            DataTable dt = DbHelper.GetDataTable("SELECT SubjectId FROM TeacherSubjects WHERE TeacherId=@t", new SqlParameter("@t", teacherId));
            foreach (var item in clbSubjects.Items)
            {
                var li = item as ListItem;
                if (dt.Select("SubjectId=" + li.Id).Length > 0)
                    clbSubjects.SetItemChecked(clbSubjects.Items.IndexOf(item), true);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Delete existing assignments then insert checked items
            DbHelper.ExecuteNonQuery("DELETE FROM TeacherSubjects WHERE TeacherId=@t", new SqlParameter("@t", teacherId));

            foreach (var item in clbSubjects.CheckedItems)
            {
                var li = item as ListItem;
                DbHelper.ExecuteNonQuery("INSERT INTO TeacherSubjects(TeacherId, SubjectId) VALUES(@t,@s)",
                    new SqlParameter("@t", teacherId),
                    new SqlParameter("@s", li.Id));
            }

            MessageBox.Show("Assigned saved.");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();

        // Helper class for listbox items
        private class ListItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name;
        }
    }
}

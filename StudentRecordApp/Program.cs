using System;
using System.Windows.Forms;

namespace StudentRecordApp
{
    static class Program
    {
        /// <summary>
        /// App entry.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm()); // start with login
        }
    }
}

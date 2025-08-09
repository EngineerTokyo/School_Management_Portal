using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace StudentRecordApp
{
    public static class DbHelper
    {
        // Get connection string from App.config
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;
        }

        // Return DataTable for SELECT queries
        public static DataTable GetDataTable(string sql, params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                da.Fill(dt);
            }
            return dt;
        }

        // Execute non-query (INSERT/UPDATE/DELETE)
        public static int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // Execute scalar
        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                con.Open();
                return cmd.ExecuteScalar();
            }
        }

        // Execute stored procedure returning DataTable
        public static DataTable ExecuteProc(string procName, params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(procName, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                da.Fill(dt);
            }
            return dt;
        }
    }
}

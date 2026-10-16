using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyHoSoBenhAn.Data
{
    /// <summary>ADO.NET repository: only this layer opens database connections.</summary>
    public static class Db
    {
        private static string ConnectionString
        {
            get
            {
                var item = ConfigurationManager.ConnectionStrings["HospitalDb"];
                if (item == null) throw new ConfigurationErrorsException("Chưa có HospitalDb trong App.config.");
                return item.ConnectionString;
            }
        }

        private static SqlConnection OpenConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            try
            {
                // SQL Server requires these options for DML against filtered/computed indexes.
                using (var setup = new SqlCommand(
                    "SET ANSI_NULLS ON; SET ANSI_PADDING ON; SET ANSI_WARNINGS ON; " +
                    "SET ARITHABORT ON; SET CONCAT_NULL_YIELDS_NULL ON; " +
                    "SET QUOTED_IDENTIFIER ON; SET NUMERIC_ROUNDABORT OFF;", connection))
                    setup.ExecuteNonQuery();
                return connection;
            }
            catch { connection.Dispose(); throw; }
        }

        public static SqlParameter P(string name, object value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }

        public static SqlParameter Out(string name)
        {
            return new SqlParameter(name, SqlDbType.Int) { Direction = ParameterDirection.Output };
        }

        public static int Proc(string procedure, params SqlParameter[] parameters)
        {
            using (var connection = OpenConnection())
            using (var command = new SqlCommand(procedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 30;
                if (parameters != null) command.Parameters.AddRange(parameters);
                return command.ExecuteNonQuery();
            }
        }

        public static DataTable Query(string sql, params SqlParameter[] parameters)
        {
            var table = new DataTable();
            using (var connection = OpenConnection())
            using (var command = new SqlCommand(sql, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandTimeout = 30;
                if (parameters != null) command.Parameters.AddRange(parameters);
                adapter.Fill(table);
            }
            return table;
        }

        public static int Execute(string sql, params SqlParameter[] parameters)
        {
            using (var connection = OpenConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandTimeout = 30;
                if (parameters != null) command.Parameters.AddRange(parameters);
                return command.ExecuteNonQuery();
            }
        }

        public static void TestConnection()
        {
            using (var connection = OpenConnection()) { }
        }
    }
}

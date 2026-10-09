using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace QuanLyCongTyDuLich.Data
{
    public static class Db
    {
        public static SqlConnection Open()
        {
            var setting = ConfigurationManager.ConnectionStrings["QuanLyCongTyDuLich"];
            if (setting == null) throw new InvalidOperationException("Thieu connection string trong App.config.");
            var connection = new SqlConnection(setting.ConnectionString);
            connection.Open();
            return connection;
        }
        public static SqlParameter P(string name, object value)
        { return new SqlParameter(name, value ?? DBNull.Value); }
        public static SqlCommand Command(SqlConnection connection, SqlTransaction transaction, string sql, params SqlParameter[] args)
        {
            var c = new SqlCommand(sql, connection, transaction);
            c.CommandTimeout = 30;
            if (args != null) c.Parameters.AddRange(args);
            return c;
        }
        public static DataTable Query(string sql, params SqlParameter[] args)
        {
            using (var connection = Open())
            using (var command = Command(connection, null, sql, args))
            using (var adapter = new SqlDataAdapter(command))
            { var result = new DataTable(); adapter.Fill(result); return result; }
        }
        public static int Execute(string sql, params SqlParameter[] args)
        {
            using (var connection = Open())
            using (var command = Command(connection, null, sql, args)) return command.ExecuteNonQuery();
        }
        public static object Scalar(string sql, params SqlParameter[] args)
        {
            using (var connection = Open())
            using (var command = Command(connection, null, sql, args)) return command.ExecuteScalar();
        }
        public static string Required(string value, string label)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Khong duoc de trong: " + label);
            return value.Trim();
        }
    }
}

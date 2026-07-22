using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace App_QuanLy.Data
{
    public static class DatabaseHelper
    {
        private static string _connectionString => SQLServerDbContext.GetConnectionString();

        // 1. Hàm dùng cho lệnh SELECT (Trả về một DataTable để hiển thị lên GridView)
        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        adapter.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }

        // 2. Hàm dùng cho lệnh INSERT, UPDATE, DELETE (Trả về số dòng bị ảnh hưởng)
        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Thực thi câu lệnh truy vấn và trả về ô đầu tiên của dòng đầu tiên trong tập kết quả.
        // Thường dùng cho các câu lệnh COUNT(*), SUM(), MAX(),...
        public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Nếu có tham số truyền vào thì nạp vào Command
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        conn.Open();
                        // Thực thi ngầm và trả về giá trị đơn (dạng object)
                        return cmd.ExecuteScalar();
                    }
                    catch (SqlException ex)
                    {
                        // Ghi log hoặc ném tiếp lỗi ra ngoài hệ thống
                        throw new Exception("Lỗi khi thực thi ExecuteScalar: " + ex.Message);
                    }
                }
            }
        }
    }
}

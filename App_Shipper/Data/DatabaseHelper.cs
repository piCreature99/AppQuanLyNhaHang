using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace App_Shipper.Data
{
    public static class DatabaseHelper
    {
        private static string _connectionString => SQLServerDbContext.GetConnectionString();

        public static bool ExecuteTransaction(List<Tuple<string, SqlParameter[]>> commands)
        {
            if (commands == null || commands.Count == 0) return false;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var cmdInfo in commands)
                        {
                            string query = cmdInfo.Item1;
                            SqlParameter[] parameters = cmdInfo.Item2;

                            using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                            {
                                if (parameters != null)
                                {
                                    cmd.Parameters.AddRange(parameters);
                                }
                                cmd.ExecuteNonQuery();
                            }
                        }

                        // Nếu tất cả các lệnh chạy mượt mà không lỗi, tiến hành lưu vĩnh viễn
                        trans.Commit();
                        return true;
                    }
                    catch (SqlException ex)
                    {
                        // Chỉ cần 1 lệnh trong chuỗi bị lỗi, hủy bỏ toàn bộ ngay lập tức
                        trans.Rollback();
                        throw new Exception("Giao dịch thất bại (Đã Rollback hệ thống): " + ex.Message);
                    }
                }
            }
        }
        // 1. [HÀM MỚI] Dùng cho lệnh SELECT trả về một List<T> của bất kỳ Class DTO/Model nào
        // Áp dụng SqlDataReader để đọc dữ liệu thô trực tiếp từ RAM, tối ưu tốc độ tối đa
        public static List<T> ExecuteQuery<T>(string query, SqlParameter[] parameters = null) where T : new()
        {
            List<T> list = new List<T>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Lấy danh sách các thuộc tính { get; set; } của Class T
                        PropertyInfo[] properties = typeof(T).GetProperties();

                        while (reader.Read())
                        {
                            T obj = new T();

                            // Quét qua các thuộc tính để tự động map theo tên cột trùng khớp
                            foreach (PropertyInfo prop in properties)
                            {
                                if (HasColumn(reader, prop.Name) && reader[prop.Name] != DBNull.Value)
                                {
                                    prop.SetValue(obj, reader[prop.Name]);
                                }
                            }

                            list.Add(obj);
                        }
                    }
                }
            }
            return list;
        }

        // 2. [HÀM CŨ - GIỮ NGUYÊN] Dùng cho lệnh SELECT trả về một DataTable (khi cần đổ nhanh lên Grid thô)
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

        // 3. [GIỮ NGUYÊN] Hàm dùng cho lệnh INSERT, UPDATE, DELETE (Trả về số dòng bị ảnh hưởng)
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

        // 4. [GIỮ NGUYÊN] Thực thi câu lệnh truy vấn và trả về ô đầu tiên của dòng đầu tiên (COUNT, SUM, MAX...)
        public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        conn.Open();
                        return cmd.ExecuteScalar();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Lỗi khi thực thi ExecuteScalar: " + ex.Message);
                    }
                }
            }
        }

        // Hàm bổ trợ ngầm: Kiểm tra cột trong SqlDataReader có tồn tại hay không (Không phân biệt hoa thường)
        private static bool HasColumn(SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
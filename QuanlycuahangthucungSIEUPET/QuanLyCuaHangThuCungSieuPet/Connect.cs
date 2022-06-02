using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyCuaHangThuCungSieuPet
{
    class Connect
    {
        public static string strCon = @"Data Source=TRANHUNG;Initial Catalog=QuanLyCuaHangThuCungSieuPet;Integrated Security=True";
        public DataSet ds;
        public SqlCommand cmd;
        SqlConnection conn;
        SqlDataAdapter sda;

        public DataSet select(string sql)
        {
            conn = new SqlConnection(strCon);
            conn.Open();
            ds = new DataSet();
            cmd = new SqlCommand(sql, conn);
            sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            conn.Close();
            return ds;
        }

        public bool update(string sql)
        {
            conn = new SqlConnection(strCon);
            bool check = false;
            conn.Open();
            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.ExecuteNonQuery();
                check = true;
            }
            catch (SqlException e)
            {
                MessageBox.Show("failure");
            }
            conn.Close();
            return check;
        }
        public DataTable XemDL(string sql)
        {
            conn = new SqlConnection(strCon);
            conn.Open();
            SqlDataAdapter adap = new SqlDataAdapter(sql, conn);
            // tạo ra bảng chứa dữ liệu
            DataTable dt = new DataTable();
            adap.Fill(dt);
            return dt;
            conn.Close();
        }
        public SqlCommand ThucThiDl(string sql)
        {
            conn = new SqlConnection(strCon);
            conn.Open();
            // tạo ra đối tượng cho phép thực thi các câu lệnh sql
            SqlCommand cm = new SqlCommand(sql, conn);
            // thực thi câu lệnh sql không trả về dữ liệu
            cm.ExecuteNonQuery();
            return cm;
            conn.Close();
        }
        public bool CheckKey(string sql)
        {
            conn = new SqlConnection(strCon);
            conn.Open();
            SqlDataAdapter MyData = new SqlDataAdapter(sql, conn);
            DataTable table = new DataTable();
            MyData.Fill(table);
            if (table.Rows.Count > 0)
                return true;
            else return false;
        }
    }
}
